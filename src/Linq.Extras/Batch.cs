using System;
using System.Collections.Generic;
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
#if FEATURE_CHUNK
        [Obsolete("This feature is now implemented in System.Linq. Please use Enumerable.Chunk instead")]
#endif
        [Pure]
        public static IEnumerable<IEnumerable<TSource>> Batch<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            int size)
        {
            source.CheckArgumentNull(nameof(source));
            size.CheckArgumentOutOfRange(nameof(size), 1, int.MaxValue);

            return BatchImpl(source, size);

            static IEnumerable<IEnumerable<TSource>> BatchImpl(IEnumerable<TSource> source, int size)
            {
                var batch = new List<TSource>();
                foreach (var item in source)
                {
                    batch.Add(item);
                    if (batch.Count == size)
                    {
                        yield return batch;
                        batch = new List<TSource>();
                    }
                }

                if (batch.Count > 0)
                {
                    yield return batch;
                }
            }
        }
    }
}
