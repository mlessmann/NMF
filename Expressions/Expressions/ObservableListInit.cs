using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using NMF.Utilities;

namespace NMF.Expressions
{
    internal class ObservableListInit<T> : ObservableMemberInit<T>
    {
        public ObservableListInit(ListInitExpression expression, ObservableExpressionBinder binder)
            : this(expression, binder, binder.VisitObservable<T>(expression.NewExpression)) { }

        private ObservableListInit(ListInitExpression expression, ObservableExpressionBinder binder, INotifyExpression<T> inner)
            : base(inner, expression.Initializers.Select(e => binder.VisitElementInit<T>(e, inner))) { }
    }

	internal class ObservableListInitializer<T, TElement> : ObservableMemberBinding<T>
	{
		public INotifyExpression<T> Target { get; private set; }

		public INotifyExpression<TElement> Value { get; private set; }

        public Action<T, TElement> AddAction { get; private set; }
        public Action<T, TElement> RemoveAction { get; private set; }

        public ObservableListInitializer(ElementInit expression, ObservableExpressionBinder binder, INotifyExpression<T> target)
            : this(target, binder.VisitObservable<TElement>(expression.Arguments[0]), expression.AddMethod) { }

		public ObservableListInitializer(INotifyExpression<T> target, INotifyExpression<TElement> value, MethodInfo addMethod)
		{
            if (target == null) throw new ArgumentNullException("target");
            if (value == null) throw new ArgumentNullException("value");
            if (addMethod == null) throw new ArgumentNullException("addMethod");

			Target = target;
			Value = value;

            AddAction = addMethod.CreateDelegate<Action<T, TElement>>();
            var removeMethod = FindRemoveMethod(typeof(T), typeof(TElement));
            if (removeMethod == null)
            {
                throw new InvalidOperationException("Could not find appropriate Remove method for " + addMethod.Name);
            }
            if (removeMethod.ReturnType == typeof(void))
            {
                RemoveAction = removeMethod.CreateDelegate<Action<T, TElement>>();
            }
            else if (removeMethod.ReturnType == typeof(bool))
            {
                var tempAction = removeMethod.CreateDelegate<Func<T, TElement, bool>>();
                RemoveAction = (o, i) => tempAction(o, i);
            }
            else
            {
                throw new NotSupportedException();
            }
		}

        private ObservableListInitializer(INotifyExpression<T> target, INotifyExpression<TElement> value, Action<T, TElement> addMethod, Action<T, TElement> removeMethod)
        {
            Target = target;
            Value = value;

            AddAction = addMethod;
            RemoveAction = removeMethod;
        }

        private static MethodInfo FindRemoveMethod(Type collectionType, Type elementType)
        {
            if (typeof(IList).GetTypeInfo().IsAssignableFrom(collectionType.GetTypeInfo()))
            {
                var map = collectionType.GetTypeInfo().GetRuntimeInterfaceMap(typeof(IList));
                return map.TargetMethods[9];
            }
            var methods = collectionType.GetRuntimeMethods();
            foreach (var method in methods)
            {
                if (method.Name == "Remove")
                {
                    var parameters = method.GetParameters();
                    if (parameters != null && parameters.Length == 1)
                    {
                        if (!parameters[0].IsOut && parameters[0].ParameterType.GetTypeInfo().IsAssignableFrom(elementType.GetTypeInfo()))
                        {
                            return method;
                        }
                    }
                }
            }
            throw new InvalidOperationException("No suitable Remove method found");
        }

		private void AddToList(bool removeOld, object old)
		{
            if (removeOld)
            {
                RemoveAction.Invoke(Target.Value, (TElement)old);
            }
            AddAction.Invoke(Target.Value, Value.Value);
		}

		private void TargetChanged(object sender, ValueChangedEventArgs e)
        {
            if (!Target.IsAttached) return;
            RemoveAction.Invoke((T)e.OldValue, Value.Value);
			AddToList(false, null);
		}

		private void ValueChanged(object sender, ValueChangedEventArgs e)
		{
            if (!Target.IsAttached) return;
			AddToList(true, e.OldValue);
		}

		public override void Attach()
		{
			Target.Attach();
            Value.Attach();

            Target.ValueChanged += TargetChanged;
            Value.ValueChanged += ValueChanged;

            AddToList(false, null);
		}

		public override void Detach()
		{
			Target.Detach();
            Value.Detach();

            Target.ValueChanged -= TargetChanged;
            Value.ValueChanged -= ValueChanged;

            RemoveAction.Invoke(Target.Value, Value.Value);
		}

        public override bool IsParameterFree
        {
            get { return Value.IsParameterFree; }
        }

        public override ObservableMemberBinding<T> ApplyParameters(INotifyExpression<T> newTarget, IDictionary<string, object> parameters)
        {
            return new ObservableListInitializer<T, TElement>(newTarget, Value.ApplyParameters(parameters), AddAction, RemoveAction);
        }
    }

}
