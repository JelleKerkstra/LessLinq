using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LessLinq
{
    public static class WithExpression
    {
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
            var param = propertySelector.Parameters[0];
            var prop = propertySelector.Body;

            var body = values
                .Select(t => Expression.Equal(prop, Expression.Constant(t)))
                .Aggregate(Expression.Or);
            return Expression.Lambda<Func<T, bool>>(body, param);
        }
    }
}