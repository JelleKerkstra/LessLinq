using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ReflEx;

namespace LessLinq
{
    public static class WithExpression
    {
        private static readonly MethodInfo containsMethod = Extract.GenericMethodDefinition(() => default(List<object>).Contains<object>(default));

        /// <summary>
        /// Creates an expression filter on the given property with the given values
        /// </summary>
        /// <typeparam name="T">Type to filter</typeparam>
        /// <typeparam name="TProperty">Type of the filtered property.</typeparam>
        /// <param name="propertySelector">Expression to select the property on the type.</param>
        /// <param name="values">The values to filter the type by</param>
        /// <returns>An expression filter for the given values on the given property.</returns>
        public static Expression<Func<T, bool>> With<T, TProperty>(Expression<Func<T, TProperty>> propertySelector, IEnumerable<TProperty> values)
        {
            var list = values.ToList();
            var typedMethod = containsMethod.MakeGenericMethod(typeof(TProperty));
            var body = Expression.Call(typedMethod, Expression.Constant(list), propertySelector.Body);

            return Expression.Lambda<Func<T, bool>>(body, propertySelector.Parameters[0]);
        }
    }
}