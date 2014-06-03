using System;
using System.Collections.Generic;

namespace Linq.Extras
{
    sealed class ByKeyEqualityComparer<TSource, TKey> : IEqualityComparer<TSource>
    {
        private readonly Func<TSource, TKey> _keySelector;
        private readonly IEqualityComparer<TKey> _keyComparer;

        public ByKeyEqualityComparer(Func<TSource, TKey> keySelector, IEqualityComparer<TKey> keyComparer)
        {
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

    public static class ByKeyEqualityComparer<T>
    {
        public static IEqualityComparer<T> Create<TKey>(Func<T, TKey> keySelector)
        {
            return Create(keySelector, null);
        }

        public static IEqualityComparer<T> Create<TKey>(Func<T, TKey> keySelector, IEqualityComparer<TKey> keyComparer)
        {
            return new ByKeyEqualityComparer<T, TKey>(keySelector, keyComparer);
        }
    }

    public static class ByKeyEqualityComparer
    {
        public static IEqualityComparer<T> Create<T, TKey>(Func<T, TKey> keySelector)
        {
            return Create(keySelector, null);
        }

        public static IEqualityComparer<T> Create<T, TKey>(Func<T, TKey> keySelector, IEqualityComparer<TKey> keyComparer)
        {
            return new ByKeyEqualityComparer<T, TKey>(keySelector, keyComparer);
        }

        // ReSharper disable once UnusedParameter.Global
        public static IEqualityComparer<T> Create<T, TKey>(T dummy, Func<T, TKey> keySelector)
        {
            return Create(dummy, keySelector, null);
        }

        // ReSharper disable once UnusedParameter.Global
        public static IEqualityComparer<T> Create<T, TKey>(T dummy, Func<T, TKey> keySelector, IEqualityComparer<TKey> keyComparer)
        {
            return new ByKeyEqualityComparer<T, TKey>(keySelector, keyComparer);
        }
    }

}
