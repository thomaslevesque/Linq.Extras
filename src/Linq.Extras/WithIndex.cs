using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    partial class XEnumerable
    {
        /// <summary>
        /// Associates an index to each element of the source sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <returns>A sequence of elements paired with their index in the sequence.</returns>
        [Pure]
        public static IEnumerable<ItemWithIndex<TSource>> WithIndex<TSource>(
            [NotNull] this IEnumerable<TSource> source)
        {
            return source.Select((item, index) => new ItemWithIndex<TSource>(item, index));
        }

        /// <summary>
        /// Removes the indexes from a sequence of indexed elements, returning only the elements themselves.
        /// </summary>
        /// <typeparam name="TSource">The type of the indexed elements.</typeparam>
        /// <param name="source">The sequence to remove the indexes from.</param>
        /// <returns>A sequence of elements without their associated indexes.</returns>
        [Pure]
        public static IEnumerable<TSource> WithoutIndex<TSource>(
            [NotNull] this IEnumerable<ItemWithIndex<TSource>> source)
        {
            return source.Select(indexed => indexed.Item);
        }
    }
}
