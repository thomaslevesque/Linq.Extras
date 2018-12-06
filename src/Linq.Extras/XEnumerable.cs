using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Linq.Extras
{
    /// <summary>
    /// Provides extension and helper methods to work with sequences.
    /// </summary>
    [PublicAPI]
    public static partial class XEnumerable
    {
        /// <summary>
        /// Produces a sequence containing a single element.
        /// </summary>
        /// <typeparam name="TSource">The type of the element in the sequence.</typeparam>
        /// <param name="item">The element to include in the sequence.</param>
        /// <returns>A sequence containing the specified element.</returns>
        /// <remarks>The output sequence is lazily evaluated.</remarks>
        [Pure]
        public static IEnumerable<TSource> Unit<TSource>(TSource item)
        {
            yield return item;
        }

        /// <summary>
        /// Produces an empty sequence.
        /// </summary>
        /// <typeparam name="TSource">The element typeof the sequence.</typeparam>
        /// <returns>An empty sequence.</returns>
        /// <remarks>
        /// Unlike <see cref="Enumerable.Empty{TResult}"/>, this method returns a lazily evaluated sequence; <see cref="Enumerable.Empty{TResult}"/> returns an empty array.</remarks>
        [Pure]
        public static IEnumerable<TSource> Empty<TSource>()
        {
            yield break;
        }
    }
}
