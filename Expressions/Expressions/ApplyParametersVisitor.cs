﻿using NMF.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NMF.Expressions
{
    public class ApplyParametersVisitor : ExpressionVisitorBase
    {
        private IDictionary<string, object> parameterMappings;

        public ApplyParametersVisitor(IDictionary<string, object> parameterMappings)
        {
            this.parameterMappings = parameterMappings;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            object argument;
            if (parameterMappings.TryGetValue(node.Name, out argument))
            {
                if (node.Type.IsInstanceOf(argument))
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
                    if (notifyValueType.IsInstanceOf(argument))
                    {
                        throw new NotImplementedException();
                    }
                    throw new InvalidOperationException(string.Format("The provided value {0} for parameter {1} is not valid.", argument, node.Type));
                }
            }
            else
            {
                return node;
            }
        }
    }

    internal class ReplaceParametersVisitor : ExpressionVisitor
    {
        private IDictionary<ParameterExpression, Expression> parameterValues;

        public ReplaceParametersVisitor(IDictionary<ParameterExpression, Expression> parameterValues)
        {
            if (parameterValues == null) throw new ArgumentNullException("parameterValues");
            this.parameterValues = parameterValues;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            Expression exp;
            if (parameterValues.TryGetValue(node, out exp))
            {
                return exp;
            }
            else
            {
                return node;
            }
        }
    }
}
