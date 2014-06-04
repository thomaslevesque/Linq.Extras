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

        [Pure]
        public static IEnumerable<TResult> LeftOuterJoin<TOuter, TInner, TKey, TResult>(
            [NotNull] this IEnumerable<TOuter> outer,
            [NotNull] IEnumerable<TInner> inner,
            [NotNull] Func<TOuter, TKey> outerKeySelector,
            [NotNull] Func<TInner, TKey> innerKeySelector,
            [NotNull] Func<TOuter, TInner, TResult> resultSelector)
        {
            return outer.LeftOuterJoin(inner, outerKeySelector, innerKeySelector, resultSelector, default(TInner), null);
        }

        [Pure]
        public static IEnumerable<TResult> LeftOuterJoin<TOuter, TInner, TKey, TResult>(
            [NotNull] this IEnumerable<TOuter> outer,
            [NotNull] IEnumerable<TInner> inner,
            [NotNull] Func<TOuter, TKey> outerKeySelector,
            [NotNull] Func<TInner, TKey> innerKeySelector,
            [NotNull] Func<TOuter, TInner, TResult> resultSelector,
            TInner defaultInner)
        {
            return outer.LeftOuterJoin(inner, outerKeySelector, innerKeySelector, resultSelector, defaultInner, null);
        }

        [Pure]
        public static IEnumerable<TResult> LeftOuterJoin<TOuter, TInner, TKey, TResult>(
            [NotNull] this IEnumerable<TOuter> outer,
            [NotNull] IEnumerable<TInner> inner,
            [NotNull] Func<TOuter, TKey> outerKeySelector,
            [NotNull] Func<TInner, TKey> innerKeySelector,
            [NotNull] Func<TOuter, TInner, TResult> resultSelector,
            IEqualityComparer<TKey> keyComparer)
        {
            return outer.LeftOuterJoin(inner, outerKeySelector, innerKeySelector, resultSelector, default(TInner), keyComparer);
        }

        [Pure]
        public static IEnumerable<TResult> LeftOuterJoin<TOuter, TInner, TKey, TResult>(
            [NotNull] this IEnumerable<TOuter> outer,
            [NotNull] IEnumerable<TInner> inner,
            [NotNull] Func<TOuter, TKey> outerKeySelector,
            [NotNull] Func<TInner, TKey> innerKeySelector,
            [NotNull] Func<TOuter, TInner, TResult> resultSelector,
            TInner defaultInner,
            IEqualityComparer<TKey> keyComparer)
        {
            outer.CheckArgumentNull("outer");
            inner.CheckArgumentNull("inner");
            outerKeySelector.CheckArgumentNull("outerKeySelector");
            innerKeySelector.CheckArgumentNull("innerKeySelector");
            resultSelector.CheckArgumentNull("resultSelector");

            return outer.GroupJoin(
                            inner,
                            outerKeySelector,
                            innerKeySelector,
                            (left, tmp) => new { left, tmp },
                            keyComparer)
                        .SelectMany(
                            g => g.tmp.DefaultIfEmpty(defaultInner),
                            (g, right) => resultSelector(g.left, right));
        }

        #endregion

        #region Right

        [Pure]
        public static IEnumerable<TResult> RightOuterJoin<TOuter, TInner, TKey, TResult>(
            [NotNull] this IEnumerable<TOuter> outer,
            [NotNull] IEnumerable<TInner> inner,
            [NotNull] Func<TOuter, TKey> outerKeySelector,
            [NotNull] Func<TInner, TKey> innerKeySelector,
            [NotNull] Func<TOuter, TInner, TResult> resultSelector)
        {
            return outer.RightOuterJoin(inner, outerKeySelector, innerKeySelector, resultSelector, default(TOuter), null);
        }

        [Pure]
        public static IEnumerable<TResult> RightOuterJoin<TOuter, TInner, TKey, TResult>(
            [NotNull] this IEnumerable<TOuter> outer,
            [NotNull] IEnumerable<TInner> inner,
            [NotNull] Func<TOuter, TKey> outerKeySelector,
            [NotNull] Func<TInner, TKey> innerKeySelector,
            [NotNull] Func<TOuter, TInner, TResult> resultSelector,
            TOuter defaultOuter)
        {
            return outer.RightOuterJoin(inner, outerKeySelector, innerKeySelector, resultSelector, defaultOuter, null);
        }

        [Pure]
        public static IEnumerable<TResult> RightOuterJoin<TOuter, TInner, TKey, TResult>(
            [NotNull] this IEnumerable<TOuter> outer,
            [NotNull] IEnumerable<TInner> inner,
            [NotNull] Func<TOuter, TKey> outerKeySelector,
            [NotNull] Func<TInner, TKey> innerKeySelector,
            [NotNull] Func<TOuter, TInner, TResult> resultSelector,
            IEqualityComparer<TKey> keyComparer)
        {
            return outer.RightOuterJoin(inner, outerKeySelector, innerKeySelector, resultSelector, default(TOuter), keyComparer);
        }

        [Pure]
        public static IEnumerable<TResult> RightOuterJoin<TOuter, TInner, TKey, TResult>(
            [NotNull] this IEnumerable<TOuter> outer,
            [NotNull] IEnumerable<TInner> inner,
            [NotNull] Func<TOuter, TKey> outerKeySelector,
            [NotNull] Func<TInner, TKey> innerKeySelector,
            [NotNull] Func<TOuter, TInner, TResult> resultSelector,
            TOuter defaultOuter,
            IEqualityComparer<TKey> keyComparer)
        {
            outer.CheckArgumentNull("outer");
            inner.CheckArgumentNull("inner");
            outerKeySelector.CheckArgumentNull("outerKeySelector");
            innerKeySelector.CheckArgumentNull("innerKeySelector");
            resultSelector.CheckArgumentNull("resultSelector");

            // This is actually the same as a left outer, with the inner and out sequences swapped
            return inner.LeftOuterJoin(outer, innerKeySelector, outerKeySelector, (i, o) => resultSelector(o, i), defaultOuter, keyComparer);
        }

        #endregion

        #region Full

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
            left.CheckArgumentNull("left");
            right.CheckArgumentNull("right");
            leftKeySelector.CheckArgumentNull("leftKeySelector");
            rightKeySelector.CheckArgumentNull("rightKeySelector");
            resultSelector.CheckArgumentNull("resultSelector");

            var rightLookup = right.ToLookup(rightKeySelector, keyComparer);
            var usedKeys = new HashSet<TKey>(keyComparer);
            foreach (var leftItem in left)
            {
                var key = leftKeySelector(leftItem);
                usedKeys.Add(key);
                foreach (var rightItem in rightLookup[key].DefaultIfEmpty(defaultRight))
                {
                    yield return resultSelector(key, leftItem, rightItem);
                }
            }
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
