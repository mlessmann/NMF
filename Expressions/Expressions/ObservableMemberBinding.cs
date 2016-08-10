﻿using NMF.Expressions.Execution;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace NMF.Expressions
{
    internal abstract class ObservableMemberBinding<T> : INotifiable
    {
        private readonly ShortList<INotifiable> successors = new ShortList<INotifiable>();

        public ObservableMemberBinding()
        {
            successors.CollectionChanged += (obj, e) =>
            {
                if (successors.Count == 0)
                    Detach();
                else if (e.Action == NotifyCollectionChangedAction.Add && successors.Count == 1)
                    Attach();
            };
        }

        public IList<INotifiable> Successors { get { return successors; } }

        public abstract bool IsParameterFree { get; }
        
        public abstract IEnumerable<INotifiable> Dependencies { get; }

        public ExecutionMetaData ExecutionMetaData { get; } = new ExecutionMetaData();

        private void Attach()
        {
            OnAttach();
            foreach (var dep in Dependencies)
                dep.Successors.Add(this);
        }

        private void Detach()
        {
            OnDetach();
            foreach (var dep in Dependencies)
                dep.Successors.Remove(this);
        }

        protected virtual void OnAttach() { }

        protected virtual void OnDetach() { }

        public abstract ObservableMemberBinding<T> ApplyParameters(INotifyExpression<T> newTarget, IDictionary<string, object> parameters);

        public abstract INotificationResult Notify(IList<INotificationResult> sources);
    }

    internal class ObservablePropertyMemberBinding<T, TMember> : ObservableMemberBinding<T>
    {
        public ObservablePropertyMemberBinding(MemberAssignment node, ObservableExpressionBinder binder, INotifyExpression<T> target, FieldInfo field)
            : this(target, ReflectionHelper.CreateDynamicFieldSetter<T, TMember>(field), binder.VisitObservable<TMember>(node.Expression)) { }

        public ObservablePropertyMemberBinding(MemberAssignment node, ObservableExpressionBinder binder, INotifyExpression<T> target, Action<T, TMember> member)
            : this(target, member, binder.VisitObservable<TMember>(node.Expression)) { }


        public ObservablePropertyMemberBinding(INotifyExpression<T> target, Action<T, TMember> member, INotifyExpression<TMember> value)
        {
            if (value == null) throw new ArgumentNullException("value");
            if (member == null) throw new ArgumentNullException("member");
            if (target == null) throw new ArgumentNullException("target");

            Value = value;
            Member = member;
            Target = target;
        }

        public INotifyExpression<TMember> Value { get; private set; }

        public Action<T, TMember> Member { get; private set; }

        public INotifyExpression<T> Target { get; private set; }

        public override bool IsParameterFree
        {
            get { return Value.IsParameterFree; }
        }

        public override IEnumerable<INotifiable> Dependencies
        {
            get
            {
                yield return Value;
                yield return Target;
            }
        }

        public void Apply()
        {
            Member(Target.Value, Value.Value);
        }

        public override ObservableMemberBinding<T> ApplyParameters(INotifyExpression<T> newTarget, IDictionary<string, object> parameters)
        {
            return new ObservablePropertyMemberBinding<T, TMember>(newTarget, Member, Value.ApplyParameters(parameters));
        }

        public override INotificationResult Notify(IList<INotificationResult> sources)
        {
            T oldValue = Target.Value;
            Apply();
            return new ValueChangedNotificationResult<T>(this, oldValue, Target.Value);
        }

        protected override void OnAttach()
        {
            Apply();
        }
    }

    internal class ObservableReversablePropertyMemberBinding<T, TMember> : ObservableMemberBinding<T>
    {
        private readonly IExecutionContext context;

        public ObservableReversablePropertyMemberBinding(INotifyExpression<T> target, string memberName, Func<T, TMember> memberGet, Action<T, TMember> memberSet, INotifyReversableExpression<TMember> value, IExecutionContext context)
        {
            if (value == null) throw new ArgumentNullException("value");
            if (memberName == null) throw new ArgumentNullException("memberName");
            if (memberGet == null) throw new ArgumentNullException("memberGet");
            if (memberSet == null) throw new ArgumentNullException("memberSet");
            if (target == null) throw new ArgumentNullException("target");

            Value = value;
            MemberName = memberName;
            MemberGet = memberGet;
            MemberSet = memberSet;
            Target = target;
            this.context = context;
        }

        public INotifyReversableExpression<TMember> Value { get; private set; }

        public Action<T, TMember> MemberSet { get; private set; }

        public Func<T, TMember> MemberGet { get; private set; }

        public string MemberName { get; private set; }

        public INotifyExpression<T> Target { get; private set; }

        public override bool IsParameterFree
        {
            get { return Value.IsParameterFree; }
        }

        public override IEnumerable<INotifiable> Dependencies
        {
            get
            {
                yield return Value;
                yield return Target;
            }
        }

        public void Apply()
        {
            MemberSet(Target.Value, Value.Value);
        }

        public override ObservableMemberBinding<T> ApplyParameters(INotifyExpression<T> newTarget, IDictionary<string, object> parameters)
        {
            return new ObservablePropertyMemberBinding<T, TMember>(newTarget, MemberSet, Value.ApplyParameters(parameters));
        }

        public override INotificationResult Notify(IList<INotificationResult> sources)
        {
            var targetChange = sources.FirstOrDefault(c => c.Source == Target) as ValueChangedNotificationResult<T>;
            if (targetChange != null)
            {
                DetachPropertyChangeListener(targetChange.OldValue);
                AttachPropertyChangeListener(targetChange.NewValue);
            }

            Apply();
            Value.Value = MemberGet(Target.Value);
            return new ValueChangedNotificationResult<T>(this, targetChange.OldValue, targetChange.NewValue);
        }

        protected override void OnAttach()
        {
            AttachPropertyChangeListener(Target.Value);
        }

        protected override void OnDetach()
        {
            DetachPropertyChangeListener(Target.Value);
        }

        private void AttachPropertyChangeListener(object target)
        {
            var newTarget = target as INotifyPropertyChanged;
            if (newTarget != null)
                context.AddChangeListener(this, newTarget, MemberName);
        }

        private void DetachPropertyChangeListener(object target)
        {
            var oldTarget = target as INotifyPropertyChanged;
            if (oldTarget != null)
                context.RemoveChangeListener(this, oldTarget, MemberName);
        }
    }
}
