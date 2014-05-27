using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    partial class XEnumerable
    {
        [Pure]
        public static bool SequenceEqualBy<TSource, TKey>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] IEnumerable<TSource> other,
            [NotNull] Func<TSource, TKey> keySelector)
        {
            return source.SequenceEqualBy(other, keySelector, null);
        }

        [Pure]
        public static bool SequenceEqualBy<TSource, TKey>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] IEnumerable<TSource> other,
            [NotNull] Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> keyComparer)
        {
            source.CheckArgumentNull("source");
            other.CheckArgumentNull("other");
            keySelector.CheckArgumentNull("keySelector");
            var comparer = ByKeyEqualityComparer<TSource>.Create(keySelector, keyComparer);
            return source.SequenceEqual(other, comparer);
        }
    }
}
