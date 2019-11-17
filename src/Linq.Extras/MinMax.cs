using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Linq.Extras.Internal;
using Linq.Extras.Properties;

namespace Linq.Extras
{
    partial class XEnumerable
    {
        /// <summary>
        /// Returns the maximum element of the sequence according to the specified comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The sequence to return the maximum element from.</param>
        /// <param name="comparer">The comparer used to compare elements.</param>
        /// <returns>The maximum element according to the specified comparer.</returns>
        [Pure]
        public static TSource Max<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] IComparer<TSource> comparer)
        {
            source.CheckArgumentNull(nameof(source));
            comparer.CheckArgumentNull(nameof(comparer));
            return source.Extreme(comparer, 1);
        }

        /// <summary>
        /// Returns the minimum element of the sequence according to the specified comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The sequence to return the minimum element from.</param>
        /// <param name="comparer">The comparer used to compare elements.</param>
        /// <returns>The minimum element according to the specified comparer.</returns>
        [Pure]
        public static TSource Min<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] IComparer<TSource> comparer)
        {
            source.CheckArgumentNull(nameof(source));
            comparer.CheckArgumentNull(nameof(comparer));
            return source.Extreme(comparer, -1);
        }

        [Pure]
        private static TSource Extreme<TSource>(this IEnumerable<TSource> source, IComparer<TSource> comparer, int sign)
        {
            comparer = comparer ?? Comparer<TSource>.Default;
            TSource extreme = default!;
            bool first = true;
            foreach (var item in source)
            {
                int compare = 0;
                if (!first)
                    compare = comparer.Compare(item, extreme);

                if (Math.Sign(compare) == sign || first)
                {
                    extreme = item;
                }
                first = false;
            }

            if (first)
                throw EmptySequenceException();

            return extreme;
        }

        private static InvalidOperationException EmptySequenceException()
        {
            return new InvalidOperationException(Resources.SequenceContainsNoElements);
        }

        /// <summary>
        /// Returns the element of the sequence that has the maximum value for the specified key.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <typeparam name="TKey">The type of the key used to compare elements.</typeparam>
        /// <param name="source">The sequence to return an element from.</param>
        /// <param name="keySelector">A delegate that returns the key used to compare elements.</param>
        /// <param name="keyComparer">A comparer to compare the keys.</param>
        /// <returns>The element of <c>source</c> that has the maximum value for the specified key.</returns>
        [Pure]
        public static TSource MaxBy<TSource, TKey>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TKey> keySelector,
            IComparer<TKey>? keyComparer = null)
        {
            source.CheckArgumentNull(nameof(source));
            keySelector.CheckArgumentNull(nameof(keySelector));
            var comparer = XComparer.By(keySelector, keyComparer);
            return source.Max(comparer);
        }

        /// <summary>
        /// Returns the element of the sequence that has the minimum value for the specified key.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <typeparam name="TKey">The type of the key used to compare elements.</typeparam>
        /// <param name="source">The sequence to return an element from.</param>
        /// <param name="keySelector">A delegate that returns the key used to compare elements.</param>
        /// <param name="keyComparer">A comparer to compare the keys.</param>
        /// <returns>The element of <c>source</c> that has the minimum value for the specified key.</returns>
        [Pure]
        public static TSource MinBy<TSource, TKey>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TKey> keySelector,
            IComparer<TKey>? keyComparer = null)
        {
            source.CheckArgumentNull(nameof(source));
            keySelector.CheckArgumentNull(nameof(keySelector));
            var comparer = XComparer.By(keySelector, keyComparer);
            return source.Min(comparer);
        }
    }
}
