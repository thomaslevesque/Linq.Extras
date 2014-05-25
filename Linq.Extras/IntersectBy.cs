using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    partial class EnumerableEx
    {
        public static IEnumerable<TSource> IntersectBy<TSource, TKey>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] IEnumerable<TSource> other,
            [NotNull] Func<TSource, TKey> keySelector)
        {
            return source.IntersectBy(other, keySelector, null);
        }

        public static IEnumerable<TSource> IntersectBy<TSource, TKey>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] IEnumerable<TSource> other,
            [NotNull] Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> keyComparer)
        {
            source.CheckArgumentNull("source");
            other.CheckArgumentNull("other");
            keySelector.CheckArgumentNull("keySelector");
            var comparer = ByKeyEqualityComparer<TSource>.Create(keySelector, keyComparer);
            return source.Intersect(other, comparer);
        }
    }
}
