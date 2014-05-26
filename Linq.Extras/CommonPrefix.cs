using System.Collections.Generic;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    partial class EnumerableExtensions
    {
        [Pure]
        public static IEnumerable<T> CommonPrefix<T>(
            [NotNull] this IEnumerable<T> source,
            [NotNull] IEnumerable<T> other)
        {
            source.CheckArgumentNull("source");
            other.CheckArgumentNull("other");

            return source.CommonPrefixImpl(other, null);
        }

        [Pure]
        public static IEnumerable<T> CommonPrefix<T>(
            [NotNull] this IEnumerable<T> source,
            [NotNull] IEnumerable<T> other,
            IEqualityComparer<T> comparer)
        {
            source.CheckArgumentNull("source");
            other.CheckArgumentNull("other");

            return source.CommonPrefixImpl(other, comparer);
        }

        private static IEnumerable<T> CommonPrefixImpl<T>(this IEnumerable<T> source, IEnumerable<T> other, IEqualityComparer<T> comparer)
        {
            comparer = comparer ?? EqualityComparer<T>.Default;

            using (IEnumerator<T> en1 = source.GetEnumerator(),
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
