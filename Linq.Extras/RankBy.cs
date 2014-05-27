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
        public static IEnumerable<TResult> RankBy<TSource, TKey, TResult>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TKey> keySelector,
            [NotNull] Func<TSource, int, TResult> resultSelector)
        {
            source.CheckArgumentNull("source");
            keySelector.CheckArgumentNull("keySelector");
            resultSelector.CheckArgumentNull("resultSelector");
            return source.RankByImpl(keySelector, null, false, resultSelector);
        }

        [Pure]
        public static IEnumerable<TResult> RankBy<TSource, TKey, TResult>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TKey> keySelector,
            IComparer<TKey> keyComparer,
            [NotNull] Func<TSource, int, TResult> resultSelector)
        {
            source.CheckArgumentNull("source");
            keySelector.CheckArgumentNull("keySelector");
            resultSelector.CheckArgumentNull("resultSelector");
            return source.RankByImpl(keySelector, keyComparer, false, resultSelector);
        }

        [Pure]
        public static IEnumerable<TResult> RankByDescending<TSource, TKey, TResult>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TKey> keySelector,
            IComparer<TKey> keyComparer,
            [NotNull] Func<TSource, int, TResult> resultSelector)
        {
            source.CheckArgumentNull("source");
            keySelector.CheckArgumentNull("keySelector");
            resultSelector.CheckArgumentNull("resultSelector");
            return source.RankByImpl(keySelector, keyComparer, true, resultSelector);
        }

        [Pure]
        public static IEnumerable<TResult> RankByDescending<TSource, TKey, TResult>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TKey> keySelector,
            [NotNull] Func<TSource, int, TResult> resultSelector)
        {
            source.CheckArgumentNull("source");
            keySelector.CheckArgumentNull("keySelector");
            resultSelector.CheckArgumentNull("resultSelector");
            return source.RankByImpl(keySelector, null, true, resultSelector);
        }

        [Pure]
        private static IEnumerable<TResult> RankByImpl<TSource, TKey, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> keyComparer,
            bool descending,
            Func<TSource, int, TResult> resultSelector)
        {
            keyComparer = keyComparer ?? Comparer<TKey>.Default;

            var grouped = source.GroupBy(keySelector);
            var ordered =
                descending
                    ? grouped.OrderByDescending(g => g.Key, keyComparer)
                    : grouped.OrderBy(g => g.Key, keyComparer);

            int totalRank = 1;
            foreach (var group in ordered)
            {
                int rank = totalRank;
                foreach (var item in group)
                {
                    yield return resultSelector(item, rank);
                    totalRank++;
                }
            }
        }

        [Pure]
        public static IEnumerable<TResult> DenseRankBy<TSource, TKey, TResult>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TKey> keySelector,
            [NotNull] Func<TSource, int, TResult> resultSelector)
        {
            source.CheckArgumentNull("source");
            keySelector.CheckArgumentNull("keySelector");
            resultSelector.CheckArgumentNull("resultSelector");
            return source.DenseRankByImpl(keySelector, null, false, resultSelector);
        }

        [Pure]
        public static IEnumerable<TResult> DenseRankBy<TSource, TKey, TResult>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TKey> keySelector,
            IComparer<TKey> keyComparer,
            [NotNull] Func<TSource, int, TResult> resultSelector)
        {
            source.CheckArgumentNull("source");
            keySelector.CheckArgumentNull("keySelector");
            resultSelector.CheckArgumentNull("resultSelector");
            return source.DenseRankByImpl(keySelector, keyComparer, false, resultSelector);
        }

        [Pure]
        public static IEnumerable<TResult> DenseRankByDescending<TSource, TKey, TResult>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TKey> keySelector,
            IComparer<TKey> keyComparer,
            [NotNull] Func<TSource, int, TResult> resultSelector)
        {
            source.CheckArgumentNull("source");
            keySelector.CheckArgumentNull("keySelector");
            resultSelector.CheckArgumentNull("resultSelector");
            return source.DenseRankByImpl(keySelector, keyComparer, true, resultSelector);
        }

        [Pure]
        public static IEnumerable<TResult> DenseRankByDescending<TSource, TKey, TResult>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TKey> keySelector,
            [NotNull] Func<TSource, int, TResult> resultSelector)
        {
            source.CheckArgumentNull("source");
            keySelector.CheckArgumentNull("keySelector");
            resultSelector.CheckArgumentNull("resultSelector");
            return source.DenseRankByImpl(keySelector, null, true, resultSelector);
        }

        [Pure]
        private static IEnumerable<TResult> DenseRankByImpl<TSource, TKey, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> keyComparer,
            bool descending,
            Func<TSource, int, TResult> resultSelector)
        {
            keyComparer = keyComparer ?? Comparer<TKey>.Default;

            var grouped = source.GroupBy(keySelector);
            var ordered =
                descending
                    ? grouped.OrderByDescending(g => g.Key, keyComparer)
                    : grouped.OrderBy(g => g.Key, keyComparer);

            int rank = 1;
            foreach (var group in ordered)
            {
                foreach (var item in group)
                {
                    yield return resultSelector(item, rank);
                }
                rank++;
            }
        }
    }
}
