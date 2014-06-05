using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    [PublicAPI]
    public static class XEqualityComparer
    {
        [Pure]
        public static IEqualityComparer<T> By<T, TKey>(
            [NotNull] Func<T, TKey> keySelector,
            IEqualityComparer<TKey> keyComparer = null)
        {
            keySelector.CheckArgumentNull("keySelector");
            return new ByKeyEqualityComparer<T, TKey>(keySelector, keyComparer);
        }

        #region Comparers

        sealed class ByKeyEqualityComparer<TSource, TKey> : IEqualityComparer<TSource>
        {
            private readonly Func<TSource, TKey> _keySelector;
            private readonly IEqualityComparer<TKey> _keyComparer;

            public ByKeyEqualityComparer([NotNull] Func<TSource, TKey> keySelector, IEqualityComparer<TKey> keyComparer)
            {
                keySelector.CheckArgumentNull("keySelector");
                _keySelector = keySelector;
                _keyComparer = keyComparer ?? EqualityComparer<TKey>.Default;
            }

            public bool Equals(TSource x, TSource y)
            {
                return _keyComparer.Equals(_keySelector(x), _keySelector(y));
            }

            public int GetHashCode(TSource obj)
            {
                return _keyComparer.GetHashCode(_keySelector(obj));
            }
        }

        #endregion
    }

    [PublicAPI]
    [ExcludeFromCodeCoverage] // Nothing to test here, these are just shorcuts for convenience
    public static class XEqualityComparer<T>
    {
        [Pure]
        public static IEqualityComparer<T> By<TKey>([NotNull] Func<T, TKey> keySelector, IEqualityComparer<TKey> keyComparer = null)
        {
            return XEqualityComparer.By(keySelector, keyComparer);
        }
    }
}
