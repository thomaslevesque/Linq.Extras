using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    partial class EnumerableExtensions
    {
        [Pure]
        public static bool None<TSource>([NotNull] this IEnumerable<TSource> source)
        {
            source.CheckArgumentNull("source");
            return !source.Any();
        }

        [Pure]
        public static bool None<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, bool> predicate)
        {
            source.CheckArgumentNull("source");
            predicate.CheckArgumentNull("predicate");
            return !source.Any(predicate);
        }
    }
}
