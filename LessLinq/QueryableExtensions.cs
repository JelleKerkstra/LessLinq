using System;
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
    }
}