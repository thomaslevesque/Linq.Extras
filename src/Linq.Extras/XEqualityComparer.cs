using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Linq.Extras.Internal;
using NotNullAttribute = JetBrains.Annotations.NotNullAttribute;

namespace Linq.Extras
{
    /// <summary>
    /// Provides extension and helper methods to create and work with equality comparers.
    /// </summary>
    [PublicAPI]
    public static class XEqualityComparer
    {
        /// <summary>
        /// Creates an equality comparer based on the specified comparison key and key comparer.
        /// </summary>
        /// <typeparam name="T">The type of the objects to test for equality.</typeparam>
        /// <typeparam name="TKey">The type of the comparison key.</typeparam>
        /// <param name="keySelector">A function that returns the comparison key.</param>
        /// <param name="keyComparer">An optional comparer used to test the keys for equality.</param>
        /// <returns>An equality comparer based on the specified comparison key and key comparer.</returns>
        [Pure]
        public static IEqualityComparer<T> By<T, TKey>(
            [NotNull] Func<T, TKey> keySelector,
            IEqualityComparer<TKey>? keyComparer = null)
        {
            keySelector.CheckArgumentNull(nameof(keySelector));
            return new ByKeyEqualityComparer<T, TKey>(keySelector, keyComparer);
        }

        #region Comparers

        sealed class ByKeyEqualityComparer<TSource, TKey> : IEqualityComparer<TSource>
        {
            private readonly Func<TSource, TKey> _keySelector;
            private readonly IEqualityComparer<TKey> _keyComparer;

            public ByKeyEqualityComparer(Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? keyComparer)
            {
                _keySelector = keySelector;
                _keyComparer = keyComparer ?? EqualityComparer<TKey>.Default;
            }

#if !LACKS_COMPARER_NULLABILITY
            public bool Equals(TSource? x, TSource? y)
#else
            public bool Equals(TSource x, TSource y)
#endif
            {
                return _keyComparer.Equals(
                    x is null ? default! : _keySelector(x),
                    y is null ? default! : _keySelector(y));
            }

            public int GetHashCode(TSource obj)
            {
                var key = _keySelector(obj);
                return key is null ? 0 : _keyComparer.GetHashCode(key);
            }
        }

#endregion
    }

    /// <summary>
    /// Provides a helper method to create equality comparers by taking advantage of generic type inference
    /// </summary>
    /// <typeparam name="T">The type of the objects to compare.</typeparam>
    [PublicAPI]
    [ExcludeFromCodeCoverage] // Nothing to test here, these are just shorcuts for convenience
    public static class XEqualityComparer<T>
    {
        /// <summary>
        /// Creates an equality comparer based on the specified comparison key and key comparer.
        /// </summary>
        /// <typeparam name="TKey">The type of the comparison key.</typeparam>
        /// <param name="keySelector">A function that returns the comparison key.</param>
        /// <param name="keyComparer">An optional comparer used to test the keys for equality.</param>
        /// <returns>An equality comparer based on the specified comparison key and key comparer.</returns>
        [Pure]
        public static IEqualityComparer<T> By<TKey>([NotNull] Func<T, TKey> keySelector, IEqualityComparer<TKey>? keyComparer = null)
        {
            return XEqualityComparer.By(keySelector, keyComparer);
        }
    }
}
