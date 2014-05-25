using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Linq.Extras.Internal;
using Linq.Extras.Properties;

namespace Linq.Extras
{
    partial class EnumerableEx
    {
        public static TSource Max<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            IComparer<TSource> comparer)
        {
            source.CheckArgumentNull("source");
            return source.Extreme(comparer, 1);
        }

        public static TSource Min<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            IComparer<TSource> comparer)
        {
            source.CheckArgumentNull("source");
            return source.Extreme(comparer, -1);
        }

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
                ThrowEmptySequence();

            return extreme;
        }

        private static void ThrowEmptySequence()
        {
            throw new InvalidOperationException(Resources.SequenceHasMoreThanOneElement);
        }

        public static TSource MaxBy<TSource, TKey>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TKey> keySelector)
        {
            source.CheckArgumentNull("source");
            keySelector.CheckArgumentNull("keySelector");
            var comparer = ByKeyComparer.Create(keySelector);
            return source.Max(comparer);
        }

        public static TSource MaxBy<TSource, TKey>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TKey> keySelector,
            IComparer<TKey> keyComparer)
        {
            source.CheckArgumentNull("source");
            keySelector.CheckArgumentNull("keySelector");
            var comparer = ByKeyComparer.Create(keySelector, keyComparer);
            return source.Max(comparer);
        }

        public static TSource MinBy<TSource, TKey>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TKey> keySelector)
        {
            source.CheckArgumentNull("source");
            keySelector.CheckArgumentNull("keySelector");
            var comparer = ByKeyComparer.Create(keySelector);
            return source.Min(comparer);
        }

        public static TSource MinBy<TSource, TKey>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TKey> keySelector,
            IComparer<TKey> keyComparer)
        {
            source.CheckArgumentNull("source");
            keySelector.CheckArgumentNull("keySelector");
            var comparer = ByKeyComparer.Create(keySelector, keyComparer);
            return source.Min(comparer);
        }
    }
}
