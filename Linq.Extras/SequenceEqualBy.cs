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
        /// Determines whether two sequences are equal by comparing their elements using the specified key and key comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c> and <c>other</c>.</typeparam>
        /// <typeparam name="TKey">The type of the key used to test for equality between elements.</typeparam>
        /// <param name="source">The first sequence to compare.</param>
        /// <param name="other">The second sequence to compare.</param>
        /// <param name="keySelector">A function that returns the key to use to test for equality between elements.</param>
        /// <param name="keyComparer">A comparer used to compare the keys.</param>
        /// <returns>true if the two source sequences are of equal length and their corresponding elements compare equal according to the key and key comparer; otherwise, false.</returns>
        [Pure]
        public static bool SequenceEqualBy<TSource, TKey>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] IEnumerable<TSource> other,
            [NotNull] Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> keyComparer = null)
        {
            source.CheckArgumentNull("source");
            other.CheckArgumentNull("other");
            keySelector.CheckArgumentNull("keySelector");
            var comparer = XEqualityComparer.By(keySelector, keyComparer);
            return source.SequenceEqual(other, comparer);
        }
    }
}
