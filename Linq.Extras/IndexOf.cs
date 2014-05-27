using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    partial class XEnumerable
    {
        [Pure]
        public static int IndexOf<TSource>([NotNull] this IEnumerable<TSource> source, TSource item)
        {
            return source.IndexOf(item, null);
        }

        [Pure]
        public static int IndexOf<TSource>([NotNull] this IEnumerable<TSource> source, TSource item, IEqualityComparer<TSource> comparer)
        {
            comparer = comparer ?? EqualityComparer<TSource>.Default;
            return source.IndexOf(i => comparer.Equals(i, item));
        }

        [Pure]
        public static int IndexOf<TSource>([NotNull] this IEnumerable<TSource> source, [NotNull] Func<TSource, bool> predicate)
        {
            source.CheckArgumentNull("source");
            predicate.CheckArgumentNull("source");
            return source.WithIndex()
                .Where(i => predicate(i.Value))
                .Select(i => i.Index)
                .FirstOrDefault(-1);
        }

        [Pure]
        public static int LastIndexOf<TSource>([NotNull] this IEnumerable<TSource> source, TSource item)
        {
            return source.LastIndexOf(item, null);
        }

        [Pure]
        public static int LastIndexOf<TSource>([NotNull] this IEnumerable<TSource> source, TSource item, IEqualityComparer<TSource> comparer)
        {
            comparer = comparer ?? EqualityComparer<TSource>.Default;
            return source.LastIndexOf(i => comparer.Equals(i, item));
        }

        [Pure]
        public static int LastIndexOf<TSource>([NotNull] this IEnumerable<TSource> source, [NotNull] Func<TSource, bool> predicate)
        {
            source.CheckArgumentNull("source");
            predicate.CheckArgumentNull("source");
            return source.WithIndex()
                .Where(i => predicate(i.Value))
                .Select(i => i.Index)
                .LastOrDefault(-1);
        }
    }
}
