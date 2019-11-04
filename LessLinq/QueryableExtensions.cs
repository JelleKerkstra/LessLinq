using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NeinLinq;

namespace LessLinq
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// Applies the given predicate on the path relative to the queryable.
        /// </summary>
        /// <typeparam name="T">Type of sequence.</typeparam>
        /// <typeparam name="TPredicateSource">Type source type of the predicate.</typeparam>
        /// <param name="set">The queryable on which the predicate is to be applied.</param>
        /// <param name="path">The path on the sequence type on which to apply the predicate.</param>
        /// <param name="predicate">The predicate to apply on the queryable.</param>
        /// <returns>A sequence filtered by the given predicate on the given path.</returns>
        public static IQueryable<T> Having<T, TPredicateSource>(this IQueryable<T> set,
            Expression<Func<T, TPredicateSource>> path, Expression<Func<TPredicateSource, bool>> predicate)
        {
            return set.Where(predicate.Translate().To(path));
        }

        /// <summary>
        /// Filters the queryable by the given values on the selected property
        /// </summary>
        /// <typeparam name="T">Type of sequence.</typeparam>
        /// <typeparam name="TProperty">Type of the filtered property.</typeparam>
        /// <param name="set">The queryable on which the filter is to be applied.</param>
        /// <param name="propertySelector">Expression to select the property on the sequence type.</param>
        /// <param name="values">The values to filter the sequence by.</param>
        /// <returns>A sequence filtered by the given values on the given property.</returns>
        public static IQueryable<T> With<T, TProperty>(this IQueryable<T> set,
            Expression<Func<T, TProperty>> propertySelector, IEnumerable<TProperty> values)
        {
            if (!values.Any()) return set;

            var param = propertySelector.Parameters[0];
            var prop = propertySelector.Body;

            var body = values
                .Select(t => Expression.Equal(prop, Expression.Constant(t)))
                .Aggregate(Expression.Or);
            var predicate = Expression.Lambda<Func<T, bool>>(body, param);

            return set.Where(predicate);
        }
    }
}