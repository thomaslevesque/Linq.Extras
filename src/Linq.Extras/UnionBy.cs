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
        /// Produces the set union of two sequences, based on the specified key and key comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <typeparam name="TKey">The type of the key used to test for equality between elements.</typeparam>
        /// <param name="source">The first sequence.</param>
        /// <param name="other">The second sequence.</param>
        /// <param name="keySelector">A delegate that returns the key used to test for equality between elements.</param>
        /// <param name="keyComparer">A comparer used to test for equality between keys.</param>
        /// <returns>The set union of <c>source</c> and <c>other</c>, based on the specified key and key comparer.</returns>
        [Pure]
        public static IEnumerable<TSource> UnionBy<TSource, TKey>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] IEnumerable<TSource> other,
            [NotNull] Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> keyComparer = null)
        {
            source.CheckArgumentNull(nameof(source));
            other.CheckArgumentNull(nameof(other));
            keySelector.CheckArgumentNull(nameof(keySelector));
            var comparer = XEqualityComparer.By(keySelector, keyComparer);
            return source.Union(other, comparer);
        }
    }
}
