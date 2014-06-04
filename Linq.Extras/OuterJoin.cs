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
            [NotNull] Func<TOuter, TInner, TResult> resultSelector,
            TInner defaultInner = default(TInner),
            IEqualityComparer<TKey> keyComparer = null)
        {
            outer.CheckArgumentNull("outer");
            inner.CheckArgumentNull("inner");
            outerKeySelector.CheckArgumentNull("outerKeySelector");
            innerKeySelector.CheckArgumentNull("innerKeySelector");
            resultSelector.CheckArgumentNull("resultSelector");

            return from o in outer
                   join i in inner on outerKeySelector(o) equals innerKeySelector(i) into tmp
                   from i in tmp.DefaultIfEmpty()
                   select resultSelector(o, i);
        }

        #endregion

        #region Right

        [Pure]
        public static IEnumerable<TResult> RightOuterJoin<TOuter, TInner, TKey, TResult>(
            [NotNull] this IEnumerable<TOuter> outer,
            [NotNull] IEnumerable<TInner> inner,
            [NotNull] Func<TOuter, TKey> outerKeySelector,
            [NotNull] Func<TInner, TKey> innerKeySelector,
            [NotNull] Func<TOuter, TInner, TResult> resultSelector,
            TOuter defaultOuter = default(TOuter),
            IEqualityComparer<TKey> keyComparer = null)
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
