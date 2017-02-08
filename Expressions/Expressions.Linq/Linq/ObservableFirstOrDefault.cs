﻿using System;
using System.Collections.Generic;
using SL = System.Linq.Enumerable;
using System.Text;
using System.Collections.Specialized;
using System.Linq.Expressions;
using System.Reflection;

namespace NMF.Expressions.Linq
{

    internal class ObservableFirstOrDefault<TSource> : INotifyValue<TSource>, INotifyReversableValue<TSource>
    {
        private TSource value;
        private bool isAttached;
        private INotifyEnumerable<TSource> source;

        public static ObservableFirstOrDefault<TSource> Create(INotifyEnumerable<TSource> source)
        {
            return new ObservableFirstOrDefault<TSource>(source);
        }

        public static ObservableFirstOrDefault<TSource> CreateForPredicate(INotifyEnumerable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            return new ObservableFirstOrDefault<TSource>(source.Where(predicate));
        }

        public static ObservableFirstOrDefault<TSource> CreateExpression(IEnumerableExpression<TSource> source)
        {
            return new ObservableFirstOrDefault<TSource>(source.AsNotifiable());
        }

        public static ObservableFirstOrDefault<TSource> CreateExpressionForPredicate(IEnumerableExpression<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            return new ObservableFirstOrDefault<TSource>(source.AsNotifiable().Where(predicate));
        }

        public static Expression CreateSetExpression(MethodCallExpression node, SetExpressionRewriter rewriter)
        {
            if (node == null || rewriter == null) return null;
            var collectionType = typeof(ICollection<TSource>);
            if (!collectionType.GetTypeInfo().IsAssignableFrom(node.Arguments[0].Type)) return null;
            
            var variable = Expression.Variable(typeof(TSource));
            var collection = Expression.Variable(typeof(ICollection<TSource>));

            var perform = Expression.IfThen(Expression.Not(Expression.Call(collection, collectionType.GetRuntimeMethod("Contains", new[] { typeof(TSource) }), variable)),
                Expression.Call(collection, collectionType.GetRuntimeMethod("Add", new[] { typeof(TSource) }), variable));

            var trueBlock = Expression.Block(
                new ParameterExpression[] {
                    variable
                },
                Expression.Assign(variable, rewriter.Value),
                perform);

            return
                Expression.Block(
                    new ParameterExpression[] { collection },
                    Expression.Assign(collection, Expression.Convert(node.Arguments[0], collectionType)),
                    Expression.IfThenElse(
                        Expression.NotEqual(rewriter.Value, Expression.Constant(null, typeof(object))),
                        trueBlock,
                        Expression.Call(collection, collectionType.GetRuntimeMethod("Clear", new Type[0])))
                );
        }

        public static Expression CreateSetExpressionWithPredicate(MethodCallExpression node, SetExpressionRewriter rewriter)
        {
            if (node == null || rewriter == null) return null;
            if (node.Arguments[1] == null || 
                (node.Arguments[1].NodeType != ExpressionType.Constant
                && node.Arguments[1].NodeType != ExpressionType.Quote
                && node.Arguments[1].NodeType != ExpressionType.Lambda)) return null;
            var collectionType = typeof(ICollection<TSource>);
            if (!collectionType.GetTypeInfo().IsAssignableFrom(node.Arguments[0].Type)) return null;
            
            var variable = Expression.Variable(typeof(TSource));
            var collection = Expression.Variable(typeof(ICollection<TSource>));

            Expression perform = Expression.IfThen(Expression.Not(Expression.Call(collection, collectionType.GetRuntimeMethod("Contains", new[] { typeof(TSource) }), variable)),
                Expression.Call(collection, collectionType.GetRuntimeMethod("Add", new[] { typeof(TSource) }), variable));


            var predicate = node.Arguments[1];
            while (predicate.NodeType == ExpressionType.Quote)
            {
                predicate = (predicate as UnaryExpression).Operand;
            }
            if (predicate.NodeType == ExpressionType.Constant)
            {
                predicate = (predicate as ConstantExpression).Value as Expression;
            }
            var predicateRewriter = new SetExpressionRewriter(Expression.Constant(true));
            var predicateCasted = predicate as Expression<Func<TSource, bool>>;
            if (predicateCasted != null)
            {
                var fulfillPredicate = predicateRewriter.Visit(predicateCasted.Body);

                if (fulfillPredicate != null)
                {
                    perform = Expression.Block(perform, fulfillPredicate);
                }
            }

            var trueBlock = Expression.Block(
                new ParameterExpression[]
                {
                    variable
                },
                Expression.Assign(variable, rewriter.Value),
                perform);

            var removeMethod = ((Action<ICollection<TSource>, Func<TSource, bool>>)RemoveWhere).GetMethodInfo();
            return
                Expression.Block(
                    new ParameterExpression[] { collection },
                    Expression.Assign(collection, Expression.Convert(node.Arguments[0], collectionType)),
                    Expression.IfThenElse(
                        Expression.NotEqual(rewriter.Value, Expression.Constant(null, typeof(object))),
                        trueBlock,
                        Expression.Call(removeMethod, collection, 
                            Expression.Constant((predicate as Expression<Func<TSource, bool>>).Compile())))
                );
        }

        private static void RemoveWhere(ICollection<TSource> collection, Func<TSource, bool> predicate)
        {
            if (collection != null)
            {
                var list = collection as IList<TSource>;
                if (list != null)
                {
                    for (int i = list.Count - 1; i >= 0; i--)
                    {
                        if (predicate(list[i]))
                        {
                            list.RemoveAt(i);
                        }
                    }
                }
                else
                {
                    var toRemove = SL.ToList(SL.Where(collection, predicate));
                    foreach (var item in toRemove)
                    {
                        collection.Remove(item);
                    }
                }
            }
        }

        public ObservableFirstOrDefault(INotifyEnumerable<TSource> source)
        {
            if (source == null) throw new ArgumentNullException("source");

            this.source = source;

            Attach();
        }

        private void SourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset
                || (e.OldItems != null && e.OldStartingIndex <= 0)
                || (e.NewItems != null && e.NewStartingIndex <= 0))
            {
                SetValue(SL.FirstOrDefault(source));
            }
        }

        private void SetValue(TSource item)
        {
            if (!EqualityComparer<TSource>.Default.Equals(value, item))
            {
                var old = value;
                value = item;
                OnValueChanged(new ValueChangedEventArgs(old, item));
            }
        }

        public TSource Value
        {
            get { return value; }
        }

        protected void OnValueChanged(ValueChangedEventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, e);
            }
        }

        public event EventHandler<ValueChangedEventArgs> ValueChanged;

        public void Detach()
        {
            if (isAttached)
            {
                source.CollectionChanged -= SourceCollectionChanged;
                isAttached = false;
            }
        }

        public void Attach()
        {
            if (!isAttached)
            {
                value = SL.FirstOrDefault(source);
                source.CollectionChanged += SourceCollectionChanged;
                isAttached = true;
            }
        }


        public bool IsAttached
        {
            get { return isAttached; }
        }

        TSource INotifyReversableValue<TSource>.Value
        {
            get
            {
                return Value;
            }
            set
            {
                if (!EqualityComparer<TSource>.Default.Equals(Value, value))
                {
                    var coll = source as ICollection<TSource>;
                    if (coll == null || coll.IsReadOnly) throw new InvalidOperationException("The underlying source is not a writable collection!");
                    if (value != null)
                    {
                        if (!coll.Contains(value))
                        {
                            coll.Add(value);
                            if (EqualityComparer<TSource>.Default.Equals(this.value, value)) return;
                        }
                        var old = this.value;
                        this.value = value;
                        OnValueChanged(new ValueChangedEventArgs(old, value));
                    }
                    else
                    {
                        coll.Clear();
                    }
                }
            }
        }

        public bool IsReversable
        {
            get
            {
                var coll = source as ICollection<TSource>;
                return coll != null && !coll.IsReadOnly;
            }
        }
    }
}
