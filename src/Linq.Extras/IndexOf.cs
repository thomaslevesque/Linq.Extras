using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    partial class XEnumerable
    {
        /// <summary>
        /// Returns the position of the first occurrence of <c>item</c> in the <c>source</c> sequence, or -1 if it is not found.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The sequence to search for <c>item</c>.</param>
        /// <param name="item">The element to find the index of.</param>
        /// <param name="comparer">The comparer to use to test for equality between elements.</param>
        /// <returns>The zero-based index of the first occurrence of <c>item</c> if it is found; otherwise, -1.</returns>
        [Pure]
        public static int IndexOf<TSource>([NotNull] this IEnumerable<TSource> source, TSource item, IEqualityComparer<TSource> comparer = null)
        {
            comparer = comparer ?? EqualityComparer<TSource>.Default;
            return source.IndexOf(i => comparer.Equals(i, item));
        }

        /// <summary>
        /// Returns the position of the first element of <c>source</c> that verifies the specified predicate, or -1 if it is not found.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The sequence to search for a matching element.</param>
        /// <param name="predicate">The predicate that is tested against each element.</param>
        /// <returns>The zero-based index of the first element that verifies the predicate, or -1 if no element verifies the predicate.</returns>
        [Pure]
        public static int IndexOf<TSource>([NotNull] this IEnumerable<TSource> source, [NotNull] Func<TSource, bool> predicate)
        {
            source.CheckArgumentNull(nameof(source));
            predicate.CheckArgumentNull(nameof(predicate));
            return source.WithIndex()
                .Where(i => predicate(i.Item))
                .Select(i => i.Index)
                .FirstOrDefault(-1);
        }

        /// <summary>
        /// Returns the position of the last occurrence of <c>item</c> in the <c>source</c> sequence, or -1 if it is not found.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The sequence to search for <c>item</c>.</param>
        /// <param name="item">The element to find the index of.</param>
        /// <param name="comparer">The comparer to use to test for equality between elements.</param>
        /// <returns>The zero-based index of the last occurrence of <c>item</c> if it is found; otherwise, -1.</returns>
        [Pure]
        public static int LastIndexOf<TSource>([NotNull] this IEnumerable<TSource> source, TSource item, IEqualityComparer<TSource> comparer = null)
        {
            comparer = comparer ?? EqualityComparer<TSource>.Default;
            return source.LastIndexOf(i => comparer.Equals(i, item));
        }

        /// <summary>
        /// Returns the position of the last element of <c>source</c> that verifies the specified predicate, or -1 if it is not found.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The sequence to search for a matching element.</param>
        /// <param name="predicate">The predicate that is tested against each element.</param>
        /// <returns>The zero-based index of the last element that verifies the predicate, or -1 if no element verifies the predicate.</returns>
        [Pure]
        public static int LastIndexOf<TSource>([NotNull] this IEnumerable<TSource> source, [NotNull] Func<TSource, bool> predicate)
        {
            source.CheckArgumentNull(nameof(source));
            predicate.CheckArgumentNull(nameof(predicate));
            return source.WithIndex()
                .Where(i => predicate(i.Item))
                .Select(i => i.Index)
                .LastOrDefault(-1);
        }
    }
}
