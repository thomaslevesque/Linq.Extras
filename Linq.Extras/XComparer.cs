using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    [PublicAPI]
    public static class XComparer
    {
        [Pure]
        public static IComparer<T> Reverse<T>([NotNull] this IComparer<T> comparer)
        {
            comparer.CheckArgumentNull("comparer");
            return new ReverseComparer<T>(comparer);
        }

        [Pure]
        public static IComparer<T> ChainWith<T>(
            [NotNull] this IComparer<T> comparer,
            [NotNull] IComparer<T> nextComparer)
        {
            comparer.CheckArgumentNull("comparer");
            nextComparer.CheckArgumentNull("nextComparer");

            // Optimized to avoid nested chained comparers
            var chained = comparer as ChainedComparer<T>;
            var nextChained = nextComparer as ChainedComparer<T>;
            if (chained != null && nextChained != null)
                return new ChainedComparer<T>(chained, nextChained);
            if (chained != null)
                return new ChainedComparer<T>(chained, nextComparer);
            if (nextChained != null)
                return new ChainedComparer<T>(comparer, nextChained);
            return new ChainedComparer<T>(new[] { comparer, nextComparer });
        }

        [Pure]
        public static IComparer<T> By<T, TKey>(
            [NotNull] Func<T, TKey> keySelector,
            IComparer<TKey> keyComparer = null)
        {
            keySelector.CheckArgumentNull("keySelector");
            return new ByKeyComparer<T, TKey>(keySelector, keyComparer);
        }

        [Pure]
        public static IComparer<T> By<T, TKey>(
            T dummy,
            [NotNull] Func<T, TKey> keySelector,
            IComparer<TKey> keyComparer = null)
        {
            return By(keySelector, keyComparer);
        }

        [Pure]
        public static IComparer<T> ByDescending<T, TKey>(
            T dummy,
            [NotNull] Func<T, TKey> keySelector,
            IComparer<TKey> keyComparer = null)
        {
            return ByDescending(keySelector, keyComparer);
        }

        [Pure]
        public static IComparer<T> ByDescending<T, TKey>(
            [NotNull] Func<T, TKey> keySelector,
            IComparer<TKey> keyComparer = null)
        {
            return By(keySelector, keyComparer).Reverse();
        }

        [Pure]
        public static IComparer<T> ThenBy<T, TKey>(
            [NotNull] this IComparer<T> comparer,
            [NotNull] Func<T, TKey> keySelector,
            IComparer<TKey> keyComparer = null)
        {
            comparer.CheckArgumentNull("comparer");
            return comparer.ChainWith(By(keySelector, keyComparer));
        }

        [Pure]
        public static IComparer<T> ThenByDescending<T, TKey>(
            [NotNull] this IComparer<T> comparer,
            [NotNull] Func<T, TKey> keySelector,
            IComparer<TKey> keyComparer = null)
        {
            comparer.CheckArgumentNull("comparer");
            return comparer.ChainWith(ByDescending(keySelector, keyComparer));
        }

        #region Comparers

        sealed class ByKeyComparer<T, TKey> : IComparer<T>
        {
            private readonly Func<T, TKey> _keySelector;
            private readonly IComparer<TKey> _keyComparer;

            public ByKeyComparer([NotNull] Func<T, TKey> keySelector, IComparer<TKey> keyComparer)
            {
                keySelector.CheckArgumentNull("keySelector");
                _keySelector = keySelector;
                _keyComparer = keyComparer ?? Comparer<TKey>.Default;
            }

            public int Compare(T x, T y)
            {
                return _keyComparer.Compare(_keySelector(x), _keySelector(y));
            }
        }

        sealed class ReverseComparer<T> : IComparer<T>
        {
            private readonly IComparer<T> _baseComparer;

            public ReverseComparer([NotNull] IComparer<T> baseComparer)
            {
                baseComparer.CheckArgumentNull("baseComparer");
                _baseComparer = baseComparer;
            }

            public int Compare(T x, T y)
            {
                return _baseComparer.Compare(y, x);
            }
        }

        sealed class ChainedComparer<T> : IComparer<T>
        {
            private readonly IComparer<T>[] _comparers;

            public ChainedComparer([NotNull] IComparer<T>[] comparers)
            {
                comparers.CheckArgumentNull("comparers");
                _comparers = comparers;
            }

            public ChainedComparer([NotNull] ChainedComparer<T> first, [NotNull] IComparer<T> next)
            {
                first.CheckArgumentNull("first");
                next.CheckArgumentNull("next");
                _comparers = first._comparers.Append(next).ToArray();
            }

            public ChainedComparer([NotNull] IComparer<T> first, [NotNull] ChainedComparer<T> next)
            {
                first.CheckArgumentNull("first");
                next.CheckArgumentNull("next");
                _comparers = next._comparers.Prepend(first).ToArray();
            }

            public ChainedComparer([NotNull] ChainedComparer<T> first, [NotNull] ChainedComparer<T> next)
            {
                first.CheckArgumentNull("first");
                next.CheckArgumentNull("next");
                _comparers = first._comparers.Concat(next._comparers).ToArray();
            }

            public int Compare(T x, T y)
            {
                foreach (var comparer in _comparers)
                {
                    int cmp = comparer.Compare(x, y);
                    if (cmp != 0)
                        return cmp;
                }
                return 0;
            }
        }

        #endregion

    }

    [PublicAPI]
    public static class XComparer<T>
    {
        [Pure]
        public static IComparer<T> By<TKey>(
            [NotNull] Func<T, TKey> keySelector,
            IComparer<TKey> keyComparer = null)
        {
            return XComparer.By(keySelector, keyComparer);
        }

        [Pure]
        public static IComparer<T> ByDescending<TKey>(
            [NotNull] Func<T, TKey> keySelector,
            IComparer<TKey> keyComparer = null)
        {
            return XComparer.ByDescending(keySelector, keyComparer);
        }
    }
}
