using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    partial class XEnumerable
    {
        /// <summary>
        /// Groups equal adjacent elements from the input sequence based on the specified comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The sequence of elements to group.</param>
        /// <param name="comparer">A comparer used to test equality between elements (can be null).</param>
        /// <returns>A sequence of groupings of adjacent elements.</returns>
        [Pure]
        public static IEnumerable<IGrouping<TSource, TSource>> GroupUntilChanged<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            IEqualityComparer<TSource>? comparer = null)
        {
            source.CheckArgumentNull(nameof(source));
            return source.GroupUntilChangedByImpl(Identity, comparer);
        }

        /// <summary>
        /// Groups adjacent elements from the input sequence that have the same value for the specified key, according to the specified key comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <typeparam name="TKey">The type of the key used for testing equality between elements.</typeparam>
        /// <param name="source">The sequence of elements to group.</param>
        /// <param name="keySelector">A delegate that returns the key used to test equality between elements.</param>
        /// <param name="keyComparer">A comparer used to test equality between keys (can be null).</param>
        /// <returns>A sequence of groupings of adjacent elements that have the same value for the specified key.</returns>
        [Pure]
        public static IEnumerable<IGrouping<TKey, TSource>> GroupUntilChangedBy<TSource, TKey>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey>? keyComparer = null)
        {
            source.CheckArgumentNull(nameof(source));
            keySelector.CheckArgumentNull(nameof(keySelector));
            return source.GroupUntilChangedByImpl(keySelector, keyComparer);
        }

        private static IEnumerable<IGrouping<TKey, TSource>> GroupUntilChangedByImpl<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey>? keyComparer = null)
        {
            keyComparer = keyComparer ?? EqualityComparer<TKey>.Default;
            using (var en = source.GetEnumerator())
            {
                if (!en.MoveNext())
                    yield break;

                var currentItems = new List<TSource> {en.Current};
                var prevKey = keySelector(en.Current);

                while (en.MoveNext())
                {
                    TKey key = keySelector(en.Current);
                    if (keyComparer.Equals(prevKey, key))
                    {
                        currentItems.Add(en.Current);
                    }
                    else
                    {
                        yield return new Grouping<TKey, TSource>(prevKey, currentItems);
                        currentItems = new List<TSource>{en.Current};
                        prevKey = key;
                    }
                }

                yield return new Grouping<TKey, TSource>(prevKey, currentItems);
            }
        }

        class Grouping<TKey, TElement> : IGrouping<TKey, TElement>
        {
            private readonly IEnumerable<TElement> _elements;

            public Grouping(TKey key, IEnumerable<TElement> elements)
            {
                Key = key;
                _elements = elements;
            }

            public IEnumerator<TElement> GetEnumerator()
            {
                return _elements.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public TKey Key { get; }
        }
        
    }
}
