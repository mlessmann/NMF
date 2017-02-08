using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace NMF.Utilities
{
    public static class ReflectionExtensions
    {
        #region Types

        public static bool IsInstanceOf(this Type type, object instance)
        {
            return instance != null && (type == instance.GetType() || type.GetTypeInfo().IsInstanceOfType(instance.GetType()));
        }

        #endregion

        #region Properties

        public static Delegate CreateGetDelegate(this PropertyInfo info, bool nonPublic = false)
        {
            return CreateGetDelegateInternal(info, nonPublic, null);
        }

        public static Func<TTarget, TProperty> CreateGetDelegate<TTarget, TProperty>(this PropertyInfo info, bool nonPublic = false)
        {
            return (Func<TTarget, TProperty>)CreateGetDelegateInternal(info, nonPublic, typeof(Func<TTarget, TProperty>));
        }

        private static Delegate CreateGetDelegateInternal(PropertyInfo info, bool nonPublic, Type delegateType)
        {
            if (!info.CanRead)
                return null;

            var getMethod = info.GetGetMethod(nonPublic);
            if (getMethod == null)
                return null;

            //We only handle value types differently because of a bug in the BCL
            if (info.DeclaringType.GetTypeInfo().IsValueType)
            {
                var param = Expression.Parameter(info.DeclaringType);
                var expression = Expression.Lambda(Expression.Property(param, info), param);
                return expression.Compile();
            }
            else
            {
                if (delegateType == null)
                    delegateType = typeof(Func<,>).MakeGenericType(info.DeclaringType, info.PropertyType);
                return getMethod.CreateDelegate(delegateType);
            }
        }

        public static Delegate CreateSetDelegate(this PropertyInfo info, bool nonPublic = false)
        {
            return CreateSetDelegateInternal(info, nonPublic, null);
        }

        public static Action<TTarget, TProperty> CreateSetDelegate<TTarget, TProperty>(this PropertyInfo info, bool nonPublic = false)
        {
            return (Action<TTarget, TProperty>)CreateSetDelegateInternal(info, nonPublic, typeof(Action<TTarget, TProperty>));
        }

        private static Delegate CreateSetDelegateInternal(PropertyInfo info, bool nonPublic, Type delegateType)
        {
            if (!info.CanWrite)
                return null;

            var setMethod = info.GetSetMethod(nonPublic);
            if (setMethod == null)
                return null;

            //We only handle value types differently because of a bug in the BCL
            if (info.DeclaringType.GetTypeInfo().IsValueType)
            {
                var setParam1 = Expression.Parameter(info.DeclaringType);
                var setParam2 = Expression.Parameter(info.PropertyType);
                var expression = Expression.Lambda(Expression.Assign(Expression.Property(setParam1, info), setParam2), setParam1, setParam2);
                return expression.Compile();
            }
            else
            {
                if (delegateType == null)
                    delegateType = typeof(Action<,>).MakeGenericType(info.DeclaringType, info.PropertyType);
                return setMethod.CreateDelegate(delegateType);
            }
        }

        #endregion

        #region Fields

        public static Func<T, TField> CreateGetDelegate<T, TField>(this FieldInfo field)
        {
            var target = Expression.Parameter(field.DeclaringType, "target");
            var lambda = Expression.Lambda<Func<T, TField>>(Expression.Field(target, field), target);
            return lambda.Compile();
        }

        public static Action<T, TField> CreateSetDelegate<T, TField>(this FieldInfo field)
        {
            var target = Expression.Parameter(field.DeclaringType, "target");
            var value = Expression.Parameter(field.FieldType, "value");
            var lambda = Expression.Lambda<Action<T, TField>>(Expression.Assign(Expression.Field(target, field), value), target, value);
            return lambda.Compile();
        }

        #endregion

        #region Methods

        public static TDelegate CreateDelegate<TDelegate>(this MethodInfo info) where TDelegate : class
        {
            return info.CreateDelegate(typeof(TDelegate)) as TDelegate;
        }

        public static TDelegate CreateDelegate<TDelegate>(this MethodInfo info, object target) where TDelegate : class
        {
            return info.CreateDelegate(typeof(TDelegate), target) as TDelegate;
        }

        #endregion
    }
}
