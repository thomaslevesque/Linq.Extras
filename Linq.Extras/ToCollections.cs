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
        /// Creates a queue from the elements in the source sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The sequence containing the elements to put in the queue.</param>
        /// <returns>A queue containing the same elements as the <c>source</c> sequence.</returns>
        [Pure]
        public static Queue<TSource> ToQueue<TSource>([NotNull] this IEnumerable<TSource> source)
        {
            source.CheckArgumentNull("source");
            return new Queue<TSource>(source);
        }

        /// <summary>
        /// Creates a stack from the elements in the source sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The sequence containing the elements to put in the stack.</param>
        /// <returns>A stack containing the same elements as the <c>source</c> sequence.</returns>
        [Pure]
        public static Stack<TSource> ToStack<TSource>([NotNull] this IEnumerable<TSource> source)
        {
            source.CheckArgumentNull("source");
            return new Stack<TSource>(source);
        }

        /// <summary>
        /// Creates a hash set from the elements in the source sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The sequence containing the elements to put in the hash set.</param>
        /// <param name="comparer">A comparer to test for equality between elements.</param>
        /// <returns>A hash set containing the same elements as the <c>source</c> sequence.</returns>
        /// <remarks>Since a hash set cannot contain duplicates, duplicate elements from the <c>source</c> sequence will not be included in the hash set.</remarks>
        [Pure]
        public static HashSet<TSource> ToHashSet<TSource>([NotNull] this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer = null)
        {
            source.CheckArgumentNull("source");
            return new HashSet<TSource>(source, comparer);
        }

        /// <summary>
        /// Creates a linked list from the elements in the source sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The sequence containing the elements to put in the linked list.</param>
        /// <returns>A linked list containing the same elements as the <c>source</c> sequence.</returns>
        [Pure]
        public static LinkedList<TSource> ToLinkedList<TSource>([NotNull] this IEnumerable<TSource> source)
        {
            source.CheckArgumentNull("source");
            return new LinkedList<TSource>(source);
        }

        /// <summary>
        /// Creates an array from the elements in the source sequence. Unlike <see cref="Enumerable.ToArray{TSource}"/>,
        /// this method takes the number of elements as a parameter, so that it can allocate an array of the right size
        /// from the start, hence suppressing the need for subsequent allocations and improving performance.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The sequence containing the elements to put in the array.</param>
        /// <param name="count">The number of elements in <c>source</c>.</param>
        /// <returns>An array containing the same elements as the <c>source</c> sequence.</returns>
        /// <exception cref="IndexOutOfRangeException">The <c>source</c> sequence contains more than <c>count</c> elements.</exception>
        /// <remarks><see cref="Enumerable.ToArray{TSource}"/> doesn't know the number of elements in the source sequence (unless it
        /// implements <see cref="ICollection{TSource}"/>), so it starts by allocating a small array, copies elements into it until it's full,
        /// then allocates a new array with twice the initial size, copies the data from the previous array, and continues until all
        /// elements have been copied; then it needs to trim the array, by allocating yet another array with the correct size and copying
        /// the data into it. This is very inefficient because of the many allocations and copies. This method allows you to supply the
        /// number of elements, if you know it, so that it can make a single array allocation of exactly the right size.</remarks>
        [Pure]
        public static TSource[] ToArray<TSource>([NotNull] this IEnumerable<TSource> source, int count)
        {
            source.CheckArgumentNull("source");
            count.CheckArgumentOutOfRange("count", 0, int.MaxValue);
            var array = new TSource[count];
            int i = 0;
            foreach (var item in source)
            {
                array[i++] = item;
            }
            return array;
        }

        /// <summary>
        /// Creates a <see cref="List{TSource}"/> from the elements in the source sequence. Unlike <see cref="Enumerable.ToList{TSource}"/>,
        /// this method takes the number of elements as a parameter, so that it can allocate a list with sufficient capacity, hence suppressing
        /// the need for subsequent allocations, and improving performance.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The sequence containing the elements to put in the list.</param>
        /// <param name="count">The number of elements in <c>source</c>.</param>
        /// <returns>A <see cref="List{TSource}"/> containing the same elements as the <c>source</c> sequence.</returns>
        /// <remarks><see cref="Enumerable.ToList{TSource}"/> doesn't know the number of elements in the source sequence (unless it
        /// implements <see cref="ICollection{T}"/>), so it starts with an empty collection and adds the items to it; every time the list's
        /// capacity is exceeded, it needs to resize itself, which involves allocating a new array and copying the existing data into it
        /// (all of this is actually done in the <see cref="List{TSource}"/> class). This is very inefficient, because of the many allocations
        /// and copies. This method allows you to supply the number of elements, if you know it, so that it can allocate a list with sufficient
        /// capacity and avoid subsequent allocations.</remarks>
        [Pure]
        public static List<TSource> ToList<TSource>([NotNull] this IEnumerable<TSource> source, int count)
        {
            source.CheckArgumentNull("source");
            count.CheckArgumentOutOfRange("count", 0, int.MaxValue);
            var list = new List<TSource>(count);
            foreach (var item in source)
            {
                list.Add(item);
            }
            return list;
        }
    }
}
