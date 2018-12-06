using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Linq.Extras.Internal;
using Linq.Extras.Properties;

namespace Linq.Extras
{
    partial class XEnumerable
    {
        /// <summary>
        /// Returns the first element of a sequence or the specified default value if the sequence contains no elements.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The sequence to return the first element of.</param>
        /// <param name="defaultValue">The default value to return if the sequence is empty.</param>
        /// <returns><c>defaultValue</c> if <c>source</c> is empty; otherwise, the first element in <c>source</c>.</returns>
        [Pure]
        public static TSource FirstOrDefault<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            TSource defaultValue)
        {
            source.CheckArgumentNull(nameof(source));
            return source.DefaultIfEmpty(defaultValue).First();
        }

        /// <summary>
        /// Returns the first element of the sequence that satisfies a condition or the specied default value if no such element is found.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The sequence to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="defaultValue">The default value to return if no matching element is found.</param>
        /// <returns><c>defaultValue</c> if source is empty or if no element passes the test specified by <c>predicate</c>; otherwise, the first element in <c>source</c> that passes the test specified by <c>predicate</c>.</returns>
        [Pure]
        public static TSource FirstOrDefault<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, bool> predicate,
            TSource defaultValue)
        {
            source.CheckArgumentNull(nameof(source));
            predicate.CheckArgumentNull(nameof(predicate));
            return source.Where(predicate).FirstOrDefault(defaultValue);
        }

        /// <summary>
        /// Returns the last element of a sequence or the specified default value if the sequence contains no elements.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The sequence to return the last element of.</param>
        /// <param name="defaultValue">The default value to return if the sequence is empty.</param>
        /// <returns><c>defaultValue</c> if <c>source</c> is empty; otherwise, the last element in <c>source</c>.</returns>
        [Pure]
        public static TSource LastOrDefault<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] TSource defaultValue)
        {
            source.CheckArgumentNull(nameof(source));
            return source.DefaultIfEmpty(defaultValue).Last();
        }

        /// <summary>
        /// Returns the last element of the sequence that satisfies a condition or the specied default value if no such element is found.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The sequence to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="defaultValue">The default value to return if no matching element is found.</param>
        /// <returns><c>defaultValue</c> if source is empty or if no element passes the test specified by <c>predicate</c>; otherwise, the last element in <c>source</c> that passes the test specified by <c>predicate</c>.</returns>
        [Pure]
        public static TSource LastOrDefault<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, bool> predicate,
            TSource defaultValue)
        {
            source.CheckArgumentNull(nameof(source));
            predicate.CheckArgumentNull(nameof(predicate));
            return source.Where(predicate).LastOrDefault(defaultValue);
        }

        /// <summary>
        /// Returns the element at a specified index in a sequence, or the specified default value if the index is out of range.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The sequence to return an element from.</param>
        /// <param name="index">The zero-based index of the element to retrieve.</param>
        /// <param name="defaultValue">The default value to return if there is no element at the specified index.</param>
        /// <returns><c>defaultValue</c> if the index is outside the bounds of the source sequence; otherwise, the element at the specified position in the source sequence.</returns>
        [Pure]
        public static TSource ElementAtOrDefault<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            int index,
            TSource defaultValue)
        {
            source.CheckArgumentNull(nameof(source));
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            return source.Skip(index).DefaultIfEmpty(defaultValue).First();
        }

        /// <summary>
        /// Returns the only element of a sequence, or the specified default value if the sequence is empty; this method throws an exception if there is more than one element in the sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The sequence to return the single element of.</param>
        /// <param name="defaultValue">The default value to return if the sequence is empty.</param>
        /// <returns>The single element of the input sequence, or <c>defaultValue</c> if the sequence contains no elements.</returns>
        [Pure]
        public static TSource SingleOrDefault<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            TSource defaultValue)
        {
            source.CheckArgumentNull(nameof(source));
            int i = 0;
            TSource value = defaultValue;
            foreach (var item in source)
            {
                if (i == 0)
                {
                    value = item;
                }
                else
                {
                    throw new InvalidOperationException(Resources.SequenceHasMoreThanOneElement);
                }
                i++;
            }
            return value;
        }

        /// <summary>
        /// Returns the only element of a sequence that satisfies a specified condition, or the specified default value if no such element exists; this method throws an exception if more than one element satisfies the condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The sequence to return a single element from.</param>
        /// <param name="predicate">A function to test an element for a condition.</param>
        /// <param name="defaultValue">The default value to return if the no element matches the predicate.</param>
        /// <returns>The single element of the input sequence that satisfies the condition, or <c>defaultValue</c> if no such element is found.</returns>
        [Pure]
        public static TSource SingleOrDefault<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, bool> predicate,
            TSource defaultValue)
        {
            source.CheckArgumentNull(nameof(source));
            predicate.CheckArgumentNull(nameof(predicate));
            return source.Where(predicate).SingleOrDefault(defaultValue);
        }

    }
}
