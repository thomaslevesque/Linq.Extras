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
        /// Associates an ascending rank to each element of the input sequence, based on the specified key and key comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <typeparam name="TKey">The type of the key used to compare elements.</typeparam>
        /// <typeparam name="TResult">The type of the elements in the output sequence.</typeparam>
        /// <param name="source">The input sequence.</param>
        /// <param name="keySelector">A function that returns the key for each element.</param>
        /// <param name="resultSelector">A projection that takes an element and its rank and returns an output element.</param>
        /// <param name="keyComparer">The comparer used to compare the keys.</param>
        /// <returns>A sequence of elements associated with their rank using the result selector.</returns>
        /// <remarks>
        /// This method produces a sparse ranking, i.e. elements with equal rank produce "holes" in the ranks. In the following example,
        /// there are two elements with rank 2, so there is no element with rank 3; the rank of the next element is 4.
        /// <list type="table">
        ///     <listheader>
        ///         <term>Rank</term>
        ///         <description>Value</description>
        ///     </listheader>
        ///     <item>
        ///         <term>1</term>
        ///         <description>A</description>
        ///     </item>
        ///     <item>
        ///         <term>2</term>
        ///         <description>B</description>
        ///     </item>
        ///     <item>
        ///         <term>2</term>
        ///         <description>B</description>
        ///     </item>
        ///     <item>
        ///         <term>4</term>
        ///         <description>C</description>
        ///     </item>
        /// </list>
        /// </remarks>
        [Pure]
        public static IEnumerable<TResult> RankBy<TSource, TKey, TResult>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TKey> keySelector,
            [NotNull] Func<TSource, int, TResult> resultSelector,
            IComparer<TKey> keyComparer = null)
        {
            source.CheckArgumentNull(nameof(source));
            keySelector.CheckArgumentNull(nameof(keySelector));
            resultSelector.CheckArgumentNull(nameof(resultSelector));
            return source.RankByImpl(keySelector, resultSelector, keyComparer, false);
        }

        /// <summary>
        /// Associates a descending rank to each element of the input sequence, based on the specified key and key comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <typeparam name="TKey">The type of the key used to compare elements.</typeparam>
        /// <typeparam name="TResult">The type of the elements in the output sequence.</typeparam>
        /// <param name="source">The input sequence.</param>
        /// <param name="keySelector">A function that returns the key for each element.</param>
        /// <param name="resultSelector">A projection that takes an element and its rank and returns an output element.</param>
        /// <param name="keyComparer">The comparer used to compare the keys.</param>
        /// <returns>A sequence of elements associated with their rank using the result selector.</returns>
        /// <remarks>
        /// This method produces a sparse ranking, i.e. elements with equal rank produce "holes" in the ranks. See <see cref="RankBy{TSource,TKey,TResult}"/> for an example.
        /// </remarks>
        [Pure]
        public static IEnumerable<TResult> RankByDescending<TSource, TKey, TResult>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TKey> keySelector,
            [NotNull] Func<TSource, int, TResult> resultSelector,
            IComparer<TKey> keyComparer = null)
        {
            source.CheckArgumentNull(nameof(source));
            keySelector.CheckArgumentNull(nameof(keySelector));
            resultSelector.CheckArgumentNull(nameof(resultSelector));
            return source.RankByImpl(keySelector, resultSelector, keyComparer, true);
        }

        private static IEnumerable<TResult> RankByImpl<TSource, TKey, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, int, TResult> resultSelector,
            IComparer<TKey> keyComparer,
            bool descending)
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

        /// <summary>
        /// Associates an ascending dense rank to each element of the input sequence, based on the specified key and key comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <typeparam name="TKey">The type of the key used to compare elements.</typeparam>
        /// <typeparam name="TResult">The type of the elements in the output sequence.</typeparam>
        /// <param name="source">The input sequence.</param>
        /// <param name="keySelector">A function that returns the key for each element.</param>
        /// <param name="resultSelector">A projection that takes an element and its rank and returns an output element.</param>
        /// <param name="keyComparer">The comparer used to compare the keys.</param>
        /// <returns>A sequence of elements associated with their rank using the result selector.</returns>
        /// <remarks>
        /// This method produces a dense ranking, i.e. elements with equal rank produce no "holes" in the ranks. In the following example,
        /// there are two elements with rank 2, and the rank of the next element is 3.
        /// <list type="table">
        ///     <listheader>
        ///         <term>Dense rank</term>
        ///         <description>Value</description>
        ///     </listheader>
        ///     <item>
        ///         <term>1</term>
        ///         <description>A</description>
        ///     </item>
        ///     <item>
        ///         <term>2</term>
        ///         <description>B</description>
        ///     </item>
        ///     <item>
        ///         <term>2</term>
        ///         <description>B</description>
        ///     </item>
        ///     <item>
        ///         <term>3</term>
        ///         <description>C</description>
        ///     </item>
        /// </list>
        /// </remarks>
        [Pure]
        public static IEnumerable<TResult> DenseRankBy<TSource, TKey, TResult>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TKey> keySelector,
            [NotNull] Func<TSource, int, TResult> resultSelector,
            IComparer<TKey> keyComparer = null)
        {
            source.CheckArgumentNull(nameof(source));
            keySelector.CheckArgumentNull(nameof(keySelector));
            resultSelector.CheckArgumentNull(nameof(resultSelector));
            return source.DenseRankByImpl(keySelector, resultSelector, keyComparer, false);
        }

        /// <summary>
        /// Associates a descending dense rank to each element of the input sequence, based on the specified key and key comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <typeparam name="TKey">The type of the key used to compare elements.</typeparam>
        /// <typeparam name="TResult">The type of the elements in the output sequence.</typeparam>
        /// <param name="source">The input sequence.</param>
        /// <param name="keySelector">A function that returns the key for each element.</param>
        /// <param name="resultSelector">A projection that takes an element and its rank and returns an output element.</param>
        /// <param name="keyComparer">The comparer used to compare the keys.</param>
        /// <returns>A sequence of elements associated with their rank using the result selector.</returns>
        /// <remarks>
        /// This method produces a dense ranking, i.e. elements with equal rank produce no "holes" in the ranks. See <see cref="DenseRankBy{TSource,TKey,TResult}"/> for an example.
        /// </remarks>
        [Pure]
        public static IEnumerable<TResult> DenseRankByDescending<TSource, TKey, TResult>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TKey> keySelector,
            [NotNull] Func<TSource, int, TResult> resultSelector,
            IComparer<TKey> keyComparer = null)
        {
            source.CheckArgumentNull(nameof(source));
            keySelector.CheckArgumentNull(nameof(keySelector));
            resultSelector.CheckArgumentNull(nameof(resultSelector));
            return source.DenseRankByImpl(keySelector, resultSelector, keyComparer, true);
        }

        [Pure]
        private static IEnumerable<TResult> DenseRankByImpl<TSource, TKey, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, int, TResult> resultSelector,
            IComparer<TKey> keyComparer,
            bool descending)
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
