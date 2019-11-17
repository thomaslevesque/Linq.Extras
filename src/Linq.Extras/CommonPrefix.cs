using System.Collections.Generic;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    partial class XEnumerable
    {
        /// <summary>
        /// Returns the common prefix of two sequences, according to the specified comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The first sequence.</param>
        /// <param name="other">The second sequence.</param>
        /// <param name="comparer">The comparer used to test items for equality (can be null).</param>
        /// <returns>A sequence consisting of the first elements of <c>source</c> that match the first elements of <c>other</c>.
        /// The resulting sequence ends when the two input sequence start to differ.</returns>
        [Pure]
        public static IEnumerable<TSource> CommonPrefix<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] IEnumerable<TSource> other,
            IEqualityComparer<TSource>? comparer = null)
        {
            source.CheckArgumentNull(nameof(source));
            other.CheckArgumentNull(nameof(other));

            return source.CommonPrefixImpl(other, comparer);
        }

        private static IEnumerable<TSource> CommonPrefixImpl<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> other, IEqualityComparer<TSource>? comparer)
        {
            comparer = comparer ?? EqualityComparer<TSource>.Default;

            using (IEnumerator<TSource> en1 = source.GetEnumerator(),
                                  en2 = other.GetEnumerator())
            {
                while (en1.MoveNext() && en2.MoveNext())
                {
                    if (comparer.Equals(en1.Current, en2.Current))
                        yield return en1.Current;
                    else
                        yield break;
                }
            }
        }
    }
}
