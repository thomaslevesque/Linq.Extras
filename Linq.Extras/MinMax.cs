using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Linq.Extras.Internal;
using Linq.Extras.Properties;

namespace Linq.Extras
{
    partial class XEnumerable
    {
        [Pure]
        public static TSource Max<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            IComparer<TSource> comparer)
        {
            source.CheckArgumentNull("source");
            return source.Extreme(comparer, 1);
        }

        [Pure]
        public static TSource Min<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            IComparer<TSource> comparer)
        {
            source.CheckArgumentNull("source");
            return source.Extreme(comparer, -1);
        }

        [Pure]
        private static TSource Extreme<TSource>(this IEnumerable<TSource> source, IComparer<TSource> comparer, int sign)
        {
            comparer = comparer ?? Comparer<TSource>.Default;
            TSource extreme = default(TSource);
            bool first = true;
            foreach (var item in source)
            {
                int compare = 0;
                if (!first)
                    compare = comparer.Compare(item, extreme);

                if (Math.Sign(compare) == sign || first)
                {
                    extreme = item;
                }
                first = false;
            }

            if (first)
                throw EmptySequenceException();

            return extreme;
        }

        private static InvalidOperationException EmptySequenceException()
        {
            return new InvalidOperationException(Resources.SequenceContainsNoElements);
        }

        [Pure]
        public static TSource MaxBy<TSource, TKey>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TKey> keySelector)
        {
            source.CheckArgumentNull("source");
            keySelector.CheckArgumentNull("keySelector");
            var comparer = XComparer.By(keySelector);
            return source.Max(comparer);
        }

        [Pure]
        public static TSource MaxBy<TSource, TKey>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TKey> keySelector,
            IComparer<TKey> keyComparer)
        {
            source.CheckArgumentNull("source");
            keySelector.CheckArgumentNull("keySelector");
            var comparer = XComparer.By(keySelector, keyComparer);
            return source.Max(comparer);
        }

        [Pure]
        public static TSource MinBy<TSource, TKey>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TKey> keySelector)
        {
            source.CheckArgumentNull("source");
            keySelector.CheckArgumentNull("keySelector");
            var comparer = XComparer.By(keySelector);
            return source.Min(comparer);
        }

        [Pure]
        public static TSource MinBy<TSource, TKey>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TKey> keySelector,
            IComparer<TKey> keyComparer)
        {
            source.CheckArgumentNull("source");
            keySelector.CheckArgumentNull("keySelector");
            var comparer = XComparer.By(keySelector, keyComparer);
            return source.Min(comparer);
        }
    }
}
