using System.Collections.Generic;

namespace LessLinq
{
    public static class GenericExtensions
    {
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