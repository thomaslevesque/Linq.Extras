using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    partial class EnumerableEx
    {
        /// <summary>
        /// Appends the specified element to the specified sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The sequence to append an element to.</param>
        /// <param name="item">The element to append.</param>
        /// <returns>The source sequence followed by the appended element.</returns>
        public static IEnumerable<TSource> Append<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            TSource item)
        {
            source.CheckArgumentNull("source");
            return source.Concat(new[] { item });
        }

        /// <summary>
        /// Prepends the specified element to the specified sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The sequence to prepend an element to.</param>
        /// <param name="item">The element to prepend.</param>
        /// <returns>The source sequence preceded by the prepended element.</returns>
        public static IEnumerable<TSource> Prepend<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            TSource item)
        {
            source.CheckArgumentNull("source");
            return new[] { item }.Concat(source);
        }
    }
}
