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
            [NotNull] Func<TKey, TLeft, TRight, TResult> resultSelector)
        {
            return left.FullOuterJoin(right, leftKeySelector, rightKeySelector, resultSelector, default(TLeft), default(TRight), null);
        }

        [Pure]
        public static IEnumerable<TResult> FullOuterJoin<TLeft, TRight, TKey, TResult>(
            [NotNull] this IEnumerable<TLeft> left,
            [NotNull] IEnumerable<TRight> right,
            [NotNull] Func<TLeft, TKey> leftKeySelector,
            [NotNull] Func<TRight, TKey> rightKeySelector,
            [NotNull] Func<TKey, TLeft, TRight, TResult> resultSelector,
            TLeft defaultLeft,
            TRight defaultRight)
        {
            return left.FullOuterJoin(right, leftKeySelector, rightKeySelector, resultSelector, defaultLeft, defaultRight, null);
        }

        [Pure]
        public static IEnumerable<TResult> FullOuterJoin<TLeft, TRight, TKey, TResult>(
            [NotNull] this IEnumerable<TLeft> left,
            [NotNull] IEnumerable<TRight> right,
            [NotNull] Func<TLeft, TKey> leftKeySelector,
            [NotNull] Func<TRight, TKey> rightKeySelector,
            [NotNull] Func<TKey, TLeft, TRight, TResult> resultSelector,
            IEqualityComparer<TKey> keyComparer)
        {
            return left.FullOuterJoin(right, leftKeySelector, rightKeySelector, resultSelector, default(TLeft), default(TRight), keyComparer);
        }

        [Pure]
        public static IEnumerable<TResult> FullOuterJoin<TLeft, TRight, TKey, TResult>(
            [NotNull] this IEnumerable<TLeft> left,
            [NotNull] IEnumerable<TRight> right,
            [NotNull] Func<TLeft, TKey> leftKeySelector,
            [NotNull] Func<TRight, TKey> rightKeySelector,
            [NotNull] Func<TKey, TLeft, TRight, TResult> resultSelector,
            TLeft defaultLeft,
            TRight defaultRight,
            IEqualityComparer<TKey> keyComparer)
        {
            left.CheckArgumentNull("left");
            right.CheckArgumentNull("right");
            leftKeySelector.CheckArgumentNull("leftKeySelector");
            rightKeySelector.CheckArgumentNull("rightKeySelector");
            resultSelector.CheckArgumentNull("resultSelector");

            // ReSharper disable PossibleMultipleEnumeration
            var leftJoin = left.LeftOuterJoin(right, leftKeySelector, rightKeySelector, (l, r) => new { key = leftKeySelector(l), l, r }, defaultRight, keyComparer).Select(x => resultSelector(x.key, x.l, x.r));
            var rightJoin = left.RightOuterJoin(right, leftKeySelector, rightKeySelector, (l, r) => new { key = rightKeySelector(r), l, r }, defaultLeft, keyComparer).Select(x => resultSelector(x.key, x.l, x.r));
            // ReSharper restore PossibleMultipleEnumeration
            return leftJoin.Union(rightJoin);
        }

        #endregion
    }
}
