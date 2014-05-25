using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    partial class EnumerableEx
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TKey> keySelector)
        {
            return source.DistinctBy(keySelector, null);
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> keyComparer)
        {
            source.CheckArgumentNull("source");
            keySelector.CheckArgumentNull("keySelector");
            var comparer = ByKeyEqualityComparer<TSource>.Create(keySelector, keyComparer);
            return source.Distinct(comparer);
        }
    }
}
