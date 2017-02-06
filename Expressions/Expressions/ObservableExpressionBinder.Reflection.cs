using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NMF.Expressions
{
    internal partial class ObservableExpressionBinder
    {
        private Expression VisitImplementedOperator(BinaryExpression node, string reverseOperator)
        {
            var leftSubtract = node.Method.DeclaringType.GetRuntimeMethod(reverseOperator, new Type[] { node.Type, node.Left.Type });
            MethodInfo rightSubtract;
            if (node.Left.Type == node.Right.Type)
            {
                rightSubtract = leftSubtract;
            }
            else
            {
                rightSubtract = node.Method.DeclaringType.GetRuntimeMethod(reverseOperator, new Type[] { node.Type, node.Right.Type });
            }
            if (leftSubtract != null || rightSubtract != null)
            {
                return Activator.CreateInstance(typeof(ObservableReversableBinaryExpression<,,>).MakeGenericType(node.Left.Type, node.Right.Type, node.Type),
                    node, this, leftSubtract, rightSubtract) as Expression;
            }
            else
            {
                return VisitImplementedBinary(node);
            }
        }

        private Type GetLeastGeneralCommonType(Type type1, Type type2)
        {
            if (type1.GetTypeInfo().IsInterface)
            {
                if (type2.GetTypeInfo().ImplementedInterfaces.Contains(type1)) return type1;
                if (type2.GetTypeInfo().IsInterface)
                {
                    if (type1.GetTypeInfo().ImplementedInterfaces.Contains(type2)) return type2;
                }
                return typeof(object);
            }
            else
            {
                if (type2.GetTypeInfo().IsInterface)
                {
                    if (type1.GetTypeInfo().ImplementedInterfaces.Contains(type2)) return type2;
                    return typeof(object);
                }
                Type current = type1;
                List<Type> types = new List<Type>();
                while (current != null)
                {
                    types.Add(current);
                    current = current.GetTypeInfo().BaseType;
                }
                current = type2;
                while (!types.Contains(current))
                {
                    current = current.GetTypeInfo().BaseType;
                }
                return current;
            }
        }

        protected override Expression VisitInvocation(InvocationExpression node)
        {
            if (typeof(Delegate).GetTypeInfo().IsAssignableFrom(node.Expression.Type.GetTypeInfo()) || IsObservableFuncType(node.Expression.Type, node.Arguments.Count))
            {
                return VisitMethodCall(Expression.Call(node.Expression, node.Expression.Type.GetRuntimeMethod("Invoke", node.Arguments.Select(a => a.Type).ToArray()), node.Arguments));
            }
            throw new InvalidOperationException("Unclear what to invoke.");
        }

        private bool IsObservableFuncType(Type type, int arguments)
        {
            if (!type.IsConstructedGenericType) return false;
            return type.GetGenericTypeDefinition() == funcTypes[arguments];
        }
    }
}
