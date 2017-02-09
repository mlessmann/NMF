﻿using NMF.Transformations.Core;
using NMF.Transformations;
using NMF.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace NMF.Synchronizations
{
    public abstract class Synchronization : Transformation
    {
        public abstract IEnumerable<SynchronizationRuleBase> SynchronizationRules { get; }

        public override void RegisterRules()
        {
            if (!IsRulesRegistered)
            {
                base.RegisterRules();

                if (SynchronizationRules != null)
                {
                    foreach (var rule in SynchronizationRules)
                    {
                        rule.Synchronization = this;
                        rule.DeclareSynchronization();
                    }
                }
            }
        }

        public virtual SynchronizationRuleBase GetSynchronizationRuleForType(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            return SynchronizationRules.FirstOrDefault(type.IsInstanceOf);
        }

        public virtual IEnumerable<SynchronizationRuleBase> GetSynchronizationRulesForType(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            return SynchronizationRules.Where(type.IsInstanceOf);
        }

        public virtual SynchronizationRuleBase GetSynchronizationRuleForSignature(Type left, Type right)
        {
            var exactMatch = SynchronizationRules.Where(s => s.LeftType == left && s.RightType == right).FirstOrDefault();
            if (exactMatch != null) return exactMatch;
            return SynchronizationRules.FirstOrDefault(s => left.GetTypeInfo().IsAssignableFrom(s.LeftType.GetTypeInfo()) && right.GetTypeInfo().IsAssignableFrom(s.RightType.GetTypeInfo()));
        }

        public virtual IEnumerable<SynchronizationRuleBase> GetSynchronizationRulesForSignature(Type left, Type right)
        {
            return SynchronizationRules.Where(s => left.GetTypeInfo().IsAssignableFrom(s.LeftType.GetTypeInfo()) && right.GetTypeInfo().IsAssignableFrom(s.RightType.GetTypeInfo()));
        }

        public ISynchronizationContext Synchronize<TLeft, TRight>(ref TLeft left, ref TRight right, SynchronizationDirection direction, ChangePropagationMode changePropagation)
            where TLeft : class
            where TRight : class
        {
            return Synchronize<TLeft, TRight>(GetSynchronizationRuleForSignature(typeof(TLeft), typeof(TRight)) as SynchronizationRule<TLeft, TRight>, ref left, ref right, direction, changePropagation);
        }

        public ISynchronizationContext Synchronize<TLeft, TRight>(SynchronizationRule<TLeft, TRight> startRule, ref TLeft left, ref TRight right, SynchronizationDirection direction, ChangePropagationMode changePropagation)
            where TLeft : class
            where TRight : class
        {
            if (startRule == null) throw new ArgumentNullException("startRule");

            Initialize();

            var context = new SynchronizationContext(this, direction, changePropagation);
            switch (direction)
            {
                case SynchronizationDirection.LeftToRight:
                case SynchronizationDirection.LeftToRightForced:
                case SynchronizationDirection.LeftWins:
                    var c1 = TransformationRunner.Transform(new object[] { left }, new object[] { right }, startRule.LeftToRight, context);
                    right = c1.Output as TRight;
                    break;
                case SynchronizationDirection.RightToLeft:
                case SynchronizationDirection.RightToLeftForced:
                case SynchronizationDirection.RightWins:
                    var c2 = TransformationRunner.Transform(new object[] { right }, new object[] { left }, startRule.RightToLeft, context);
                    left = c2.Output as TLeft;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("direction");
            }
            return context;
        }

        public ISynchronizationContext SynchronizeMany<TLeft, TRight>(SynchronizationRule<TLeft, TRight> startRule, ICollection<TLeft> lefts, ICollection<TRight> rights, SynchronizationDirection direction, ChangePropagationMode changePropagation)
            where TLeft : class
            where TRight : class
        {
            if (startRule == null) throw new ArgumentNullException("startRule");

            var context = new SynchronizationContext(this, direction, changePropagation);
            switch (direction)
            {
                case SynchronizationDirection.LeftToRight:
                    var c1 = TransformationRunner.TransformMany(lefts.Select(l => new object[] { l }), rights, startRule.LeftToRight, context);
                    rights.Clear();
                    rights.AddRange(c1.Select(c => c.Output as TRight));
                    break;
                case SynchronizationDirection.RightToLeft:
                    var c2 = TransformationRunner.TransformMany(rights.Select(r => new object[] { r }), lefts, startRule.RightToLeft, context);
                    lefts.Clear();
                    lefts.AddRange(c2.Select(c => c.Output as TLeft));
                    break;
                default:
                    throw new ArgumentOutOfRangeException("direction");
            }
            return context;
        }
    }
}
