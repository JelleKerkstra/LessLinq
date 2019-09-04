using System.Collections.Generic;

namespace LessLinq
{
    public static class GenericExtensions
    {
        /// <summary>
        /// Returns a sequence consisting of both source elements.
        /// </summary>
        /// <typeparam name="T">Type of sequence.</typeparam>
        /// <param name="param1">First element of the new sequence.</param>
        /// <param name="param2">Second element of the new sequence.</param>
        /// <returns>A sequence consisting of both source elements.</returns>
        public static IEnumerable<T> Concat<T>(this T param1, T param2)
        {
            yield return param1;
            yield return param2;
        }

        /// <summary>
        /// Returns a sequence consisting of the source element.
        /// </summary>
        /// <typeparam name="T">Type of sequence.</typeparam>
        /// <param name="param">The element in the new sequence.</param>
        /// <returns>A sequence consisting of the source element.</returns>
        public static IEnumerable<T> Yield<T>(this T param)
        {
            yield return param;
        }
    }
}