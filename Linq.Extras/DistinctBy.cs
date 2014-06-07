using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    partial class XEnumerable
    {
        /// <summary>
        /// Return distinct items from the input sequence based on the specified key and the specified key comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <typeparam name="TKey">The type of the key used for testing equality between items.</typeparam>
        /// <param name="source">The sequence from which to return distinct items.</param>
        /// <param name="keySelector">A delegate that returns the key used to test equality between items.</param>
        /// <param name="keyComparer">A comparer used to test equality between keys (can be null).</param>
        /// <returns>A sequence whose elements have distinct values for the specified key.</returns>
        [Pure]
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> keyComparer = null)
        {
            source.CheckArgumentNull("source");
            keySelector.CheckArgumentNull("keySelector");
            var comparer = XEqualityComparer.By(keySelector, keyComparer);
            return source.Distinct(comparer);
        }
    }
}
