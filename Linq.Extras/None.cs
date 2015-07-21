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
        /// Checks if the sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The sequence to check for emptyness.</param>
        /// <returns>true if the sequence is empty, false if it contains at least one element.</returns>
        [Pure]
        public static bool None<TSource>([NotNull] this IEnumerable<TSource> source)
        {
            source.CheckArgumentNull(nameof(source));
            return !source.Any();
        }

        /// <summary>
        /// Checks if the sequence contains no element that matches the predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The sequence to check.</param>
        /// <param name="predicate">The predicate to verify against each element in the sequence.</param>
        /// <returns>true if no element matches the predicate, false if at least one element matches the predicate.</returns>
        [Pure]
        public static bool None<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, bool> predicate)
        {
            source.CheckArgumentNull(nameof(source));
            predicate.CheckArgumentNull(nameof(predicate));
            return !source.Any(predicate);
        }
    }
}
