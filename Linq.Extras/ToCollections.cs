using System.Collections.Generic;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    partial class XEnumerable
    {
        /// <summary>
        /// Creates a queue from the elements in the source sequence.
        /// </summary>
        /// <typeparam name="TSource">The types of the elements of <c>source</c>.</typeparam>
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
        /// <typeparam name="TSource">The types of the elements of <c>source</c>.</typeparam>
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
        /// <typeparam name="TSource">The types of the elements of <c>source</c>.</typeparam>
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
        /// <typeparam name="TSource">The types of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The sequence containing the elements to put in the linked list.</param>
        /// <returns>A linked list containing the same elements as the <c>source</c> sequence.</returns>
        [Pure]
        public static LinkedList<TSource> ToLinkedList<TSource>([NotNull] this IEnumerable<TSource> source)
        {
            source.CheckArgumentNull("source");
            return new LinkedList<TSource>(source);
        }
    }
}
