using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    partial class XEnumerable
    {
        #region Left

        /// <summary>
        /// Produces the left outer join of two sequences.
        /// </summary>
        /// <typeparam name="TLeft">The type of the elements of <c>left</c>.</typeparam>
        /// <typeparam name="TRight">The type of the elements of <c>right</c>.</typeparam>
        /// <typeparam name="TKey">The type of the key used to join elements.</typeparam>
        /// <typeparam name="TResult">The type of the elements of the output sequence</typeparam>
        /// <param name="left">The left sequence.</param>
        /// <param name="right">The right sequence.</param>
        /// <param name="leftKeySelector">The key selector for elements of the left sequence.</param>
        /// <param name="rightKeySelector">The key selector for elements of the right sequence.</param>
        /// <param name="resultSelector">A function to produce an output element from two matching elements from the left and right sequences.</param>
        /// <param name="defaultRight">The default value to use for missing elements in <c>right</c>.</param>
        /// <param name="keyComparer">A comparer to test for equality between the keys.</param>
        /// <returns>The left outer join of <c>left</c> and <c>right</c>.</returns>
        [Pure]
        public static IEnumerable<TResult> LeftOuterJoin<TLeft, TRight, TKey, TResult>(
            [NotNull] this IEnumerable<TLeft> left,
            [NotNull] IEnumerable<TRight> right,
            [NotNull] Func<TLeft, TKey> leftKeySelector,
            [NotNull] Func<TRight, TKey> rightKeySelector,
            [NotNull] Func<TLeft, TRight, TResult> resultSelector,
            TRight defaultRight = default(TRight),
            IEqualityComparer<TKey> keyComparer = null)
        {
            left.CheckArgumentNull(nameof(left));
            right.CheckArgumentNull(nameof(right));
            leftKeySelector.CheckArgumentNull(nameof(leftKeySelector));
            rightKeySelector.CheckArgumentNull(nameof(rightKeySelector));
            resultSelector.CheckArgumentNull(nameof(resultSelector));

            return
                left.GroupJoin(right, leftKeySelector, rightKeySelector, (o, tmp) => new { o, tmp }, keyComparer)
                     .SelectMany(j => j.tmp.DefaultIfEmpty(defaultRight), (t, i) => resultSelector(t.o, i));
        }

        #endregion

        #region Right

        /// <summary>
        /// Produces the right outer join of two sequences.
        /// </summary>
        /// <typeparam name="TLeft">The type of the elements of <c>left</c>.</typeparam>
        /// <typeparam name="TRight">The type of the elements of <c>right</c>.</typeparam>
        /// <typeparam name="TKey">The type of the key used to join elements.</typeparam>
        /// <typeparam name="TResult">The type of the elements of the output sequence</typeparam>
        /// <param name="left">The left sequence.</param>
        /// <param name="right">The right sequence.</param>
        /// <param name="leftKeySelector">The key selector for elements of the left sequence.</param>
        /// <param name="rightKeySelector">The key selector for elements of the right sequence.</param>
        /// <param name="resultSelector">A function to produce an output element from two matching elements from the left and right sequences.</param>
        /// <param name="defaultLeft">The default value to use for missing elements in <c>left</c>.</param>
        /// <param name="keyComparer">A comparer to test for equality between the keys.</param>
        /// <returns>The right outer join of <c>left</c> and <c>right</c>.</returns>
        [Pure]
        public static IEnumerable<TResult> RightOuterJoin<TLeft, TRight, TKey, TResult>(
            [NotNull] this IEnumerable<TLeft> left,
            [NotNull] IEnumerable<TRight> right,
            [NotNull] Func<TLeft, TKey> leftKeySelector,
            [NotNull] Func<TRight, TKey> rightKeySelector,
            [NotNull] Func<TLeft, TRight, TResult> resultSelector,
            TLeft defaultLeft = default(TLeft),
            IEqualityComparer<TKey> keyComparer = null)
        {
            left.CheckArgumentNull(nameof(left));
            right.CheckArgumentNull(nameof(right));
            leftKeySelector.CheckArgumentNull(nameof(leftKeySelector));
            rightKeySelector.CheckArgumentNull(nameof(rightKeySelector));
            resultSelector.CheckArgumentNull(nameof(resultSelector));

            // This is actually the same as a left outer join, with the left and right sequences swapped
            return right.LeftOuterJoin(left, rightKeySelector, leftKeySelector, (i, o) => resultSelector(o, i), defaultLeft, keyComparer);
        }

        #endregion

        #region Full

        /// <summary>
        /// Produces the full outer join of two sequences.
        /// </summary>
        /// <typeparam name="TLeft">The type of the elements of <c>left</c>.</typeparam>
        /// <typeparam name="TRight">The type of the elements of <c>right</c>.</typeparam>
        /// <typeparam name="TKey">The type of the key used to join elements.</typeparam>
        /// <typeparam name="TResult">The type of the elements of the output sequence</typeparam>
        /// <param name="left">The left sequence.</param>
        /// <param name="right">The right sequence.</param>
        /// <param name="leftKeySelector">The key selector for elements of the left sequence.</param>
        /// <param name="rightKeySelector">The key selector for elements of the right sequence.</param>
        /// <param name="resultSelector">A function to produce an output element from two matching elements from the left and right sequences.</param>
        /// <param name="defaultLeft">The default value to use for missing elements in <c>left</c>.</param>
        /// <param name="defaultRight">The default value to use for missing elements in <c>right</c>.</param>
        /// <param name="keyComparer">A comparer to test for equality between the keys.</param>
        /// <returns>The full outer join of <c>left</c> and <c>right</c>.</returns>
        [Pure]
        public static IEnumerable<TResult> FullOuterJoin<TLeft, TRight, TKey, TResult>(
            [NotNull] this IEnumerable<TLeft> left,
            [NotNull] IEnumerable<TRight> right,
            [NotNull] Func<TLeft, TKey> leftKeySelector,
            [NotNull] Func<TRight, TKey> rightKeySelector,
            [NotNull] Func<TKey, TLeft, TRight, TResult> resultSelector,
            TLeft defaultLeft = default(TLeft),
            TRight defaultRight = default(TRight),
            IEqualityComparer<TKey> keyComparer = null)
        {
            left.CheckArgumentNull(nameof(left));
            right.CheckArgumentNull(nameof(right));
            leftKeySelector.CheckArgumentNull(nameof(leftKeySelector));
            rightKeySelector.CheckArgumentNull(nameof(rightKeySelector));
            resultSelector.CheckArgumentNull(nameof(resultSelector));

            return left.FullOuterJoinIterator(right, leftKeySelector, rightKeySelector, resultSelector, defaultLeft, defaultRight, keyComparer);
        }

        private static IEnumerable<TResult> FullOuterJoinIterator<TLeft, TRight, TKey, TResult>(
            this IEnumerable<TLeft> left,
            IEnumerable<TRight> right,
            Func<TLeft, TKey> leftKeySelector,
            Func<TRight, TKey> rightKeySelector,
            Func<TKey, TLeft, TRight, TResult> resultSelector,
            TLeft defaultLeft,
            TRight defaultRight,
            IEqualityComparer<TKey> keyComparer)
        {
            // First gather items from right in a lookup by key to access them quickly
            var rightLookup = right.ToLookup(rightKeySelector, keyComparer);

            // To keep track of which keys have already been used
            var usedKeys = new HashSet<TKey>(keyComparer);

            // Pair each item from left with each matching item from right
            // (or with the default value if there is no matching item in right)
            foreach (var leftItem in left)
            {
                var key = leftKeySelector(leftItem);
                usedKeys.Add(key);
                foreach (var rightItem in rightLookup[key].DefaultIfEmpty(defaultRight))
                {
                    yield return resultSelector(key, leftItem, rightItem);
                }
            }

            // Unused items from right don't have a matching item in left
            // Pair them with the default value
            foreach (var g in rightLookup)
            {
                if (usedKeys.Contains(g.Key))
                    continue;

                foreach (var rightItem in g)
                {
                    yield return resultSelector(g.Key, defaultLeft, rightItem);
                }
            }
        }

        #endregion
    }
}
