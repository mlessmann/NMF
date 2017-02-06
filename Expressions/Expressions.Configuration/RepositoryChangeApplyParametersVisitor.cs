﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace NMF.Expressions
{
    internal class RepositoryChangeApplyParametersVisitor : ExpressionVisitorBase
    {
        private IDictionary<string, object> parameterMappings;

        public List<IChangeInfo> Recorders { get; private set; }

        public RepositoryChangeApplyParametersVisitor(IDictionary<string, object> parameterMappings)
        {
            this.parameterMappings = parameterMappings;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            object argument;
            if (parameterMappings.TryGetValue(node.Name, out argument))
            {
                if (node.Type.GetTypeInfo().IsAssignableFrom(argument.GetType().GetTypeInfo()))
                {
                    return Expression.Constant(argument);
                }
                else if (argument is Expression)
                {
                    return (Expression)argument;
                }
                else
                {
                    var notifyValueType = typeof(INotifyValue<>).MakeGenericType(node.Type);
                    if (notifyValueType.GetTypeInfo().IsAssignableFrom(argument.GetType().GetTypeInfo()))
                    {
                        if (Recorders == null)
                        {
                            Recorders = new List<IChangeInfo>();
                        }
                        var valueProperty = notifyValueType.GetRuntimeProperty("Value");
                        var recorderType = typeof(ChangeRecorder<>).MakeGenericType(node.Type);
                        var recorder = recorderType.GetTypeInfo().DeclaredConstructors.First().Invoke(new object[] { argument });

                        Recorders.Add((IChangeInfo)recorder);

                        return Expression.MakeMemberAccess(Expression.Constant(argument, notifyValueType), valueProperty);
                    }
                    throw new InvalidOperationException(string.Format("The provided value {0} for parameter {1} is not valid.", argument, node.Type));
                }
            }
            else
            {
                return node;
            }
        }

        private class ChangeRecorder<T> : IChangeInfo
        {
            public INotifyValue<T> Value { get; private set; }

            public ChangeRecorder(INotifyValue<T> value)
            {
                Value = value;
                Value.ValueChanged += Value_ValueChanged;
            }

            public event EventHandler ChangeCaptured;

            private void Value_ValueChanged(object sender, ValueChangedEventArgs e)
            {
                ChangeCaptured?.Invoke(this, e);
            }
        }
    }

    internal interface IChangeInfo
    {
        event EventHandler ChangeCaptured;
    }
}
