using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    partial class XEnumerable
    {
        /// <summary>
        /// Splits the input sequence into a sequence of batches of the specified size.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The sequence to split into batches.</param>
        /// <param name="size">The size of the batches</param>
        /// <returns>A sequence of batches of the specified size; the last batch can be shorter if there isn't enough elements remaining in the input sequence.</returns>
        [Pure]
        public static IEnumerable<IEnumerable<TSource>> Batch<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            int size)
        {
            source.CheckArgumentNull(nameof(source));
            size.CheckArgumentOutOfRange(nameof(size), 1, int.MaxValue);

            return source
                .WithIndex()
                .GroupBy(x => x.Index / size)
                .Select(g => g.WithoutIndex());
        }
    }
}
