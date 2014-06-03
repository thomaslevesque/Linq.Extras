using System;
using System.Collections.Generic;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    sealed class ByKeyComparer<T, TKey> : IComparer<T>
    {
        private readonly Func<T, TKey> _keySelector;
        private readonly IComparer<TKey> _keyComparer;

        public ByKeyComparer(Func<T, TKey> keySelector, IComparer<TKey> keyComparer)
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

    public static class ByKeyComparer<T>
    {
        public static IComparer<T> Create<TKey>(Func<T, TKey> keySelector)
        {
            return Create(keySelector, null);
        }

        public static IComparer<T> Create<TKey>(Func<T, TKey> keySelector, IComparer<TKey> keyComparer)
        {
            return new ByKeyComparer<T, TKey>(keySelector, keyComparer);
        }
    }

    public static class ByKeyComparer
    {
        public static IComparer<T> Create<T, TKey>(Func<T, TKey> keySelector)
        {
            return new ByKeyComparer<T, TKey>(keySelector, null);
        }

        public static IComparer<T> Create<T, TKey>(Func<T, TKey> keySelector, IComparer<TKey> keyComparer)
        {
            return new ByKeyComparer<T, TKey>(keySelector, keyComparer);
        }

        // ReSharper disable once UnusedParameter.Global
        public static IComparer<T> Create<T, TKey>(T dummy, Func<T, TKey> keySelector)
        {
            return Create(keySelector);
        }

        // ReSharper disable once UnusedParameter.Global
        public static IComparer<T> Create<T, TKey>(T dummy, Func<T, TKey> keySelector, IComparer<TKey> keyComparer)
        {
            return Create(keySelector, keyComparer);
        }
    }
}
