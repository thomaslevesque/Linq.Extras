using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    /// <summary>
    /// Provides extension methods for working with lists.
    /// </summary>
    [PublicAPI]
    public static class XList
    {
        /// <summary>
        /// Shuffles the elements of a list using the specified <see cref="Random"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of the elements in <c>list</c>.</typeparam>
        /// <param name="list">The list to shuffle.</param>
        /// <param name="random">A random number generator to use to shuffle the list. If null, a new random number generator will be used.</param>
        public static void Shuffle<T>(
            [NotNull] this IList<T> list,
            Random random = null)
        {
            list.CheckArgumentNull(nameof(list));
            random = random ?? new Random();

            for (int i = list.Count - 1; i > 0; i--)
            {
                int swapIndex = random.Next(i + 1);
                list.Swap(i, swapIndex);
            }
        }

        /// <summary>
        /// Swaps the elements at the specified positions in the list.
        /// </summary>
        /// <typeparam name="T">The type of the elements in <c>list</c>.</typeparam>
        /// <param name="list">The list in which to swap elements.</param>
        /// <param name="index1">The index of the first element to swap.</param>
        /// <param name="index2">The index of the second element to swap.</param>
        public static void Swap<T>(
            [NotNull] this IList<T> list,
            int index1,
            int index2)
        {
            list.CheckArgumentNull(nameof(list));
            index1.CheckArgumentOutOfRange(nameof(index1), 0, list.Count - 1);
            index2.CheckArgumentOutOfRange(nameof(index2), 0, list.Count - 1);

            T tmp = list[index1];
            list[index1] = list[index2];
            list[index2] = tmp;
        }

        /// <summary>
        /// Returns a read-only wrapper for the list.
        /// </summary>
        /// <typeparam name="T">The type of the elements in <c>list</c>.</typeparam>
        /// <param name="list">The list for which to return a read-only wrapper.</param>
        /// <returns>A read-only wrapper for <c>list</c>.</returns>
        [Pure]
        public static ReadOnlyCollection<T> AsReadOnly<T>(
            [NotNull] this IList<T> list)
        {
            return new ReadOnlyCollection<T>(list);
        }
    }
}
