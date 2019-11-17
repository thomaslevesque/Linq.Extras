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
        /// Produces the set difference of two sequences by using the specified key and key comparer to test for equality between elements.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
        /// <typeparam name="TKey">The type of the key used for testing equality between elements.</typeparam>
        /// <param name="source">A sequence whose elements that are not also in second will be returned.</param>
        /// <param name="other">A sequence whose elements that also occur in the first sequence will cause those elements to be removed from the returned sequence.</param>
        /// <param name="keySelector">A delegate that returns the key used to test equality between elements.</param>
        /// <param name="keyComparer">A comparer used to test equality between keys (can be null).</param>
        /// <returns>A sequence that contains the set difference of the elements of two sequences.</returns>
        [Pure]
        public static IEnumerable<TSource> ExceptBy<TSource, TKey>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] IEnumerable<TSource> other,
            [NotNull] Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey>? keyComparer = null)
        {
            source.CheckArgumentNull(nameof(source));
            other.CheckArgumentNull(nameof(other));
            keySelector.CheckArgumentNull(nameof(keySelector));
            var comparer = XEqualityComparer.By(keySelector, keyComparer);
            return source.Except(other, comparer);
        }
    }
}
