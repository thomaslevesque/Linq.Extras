using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    partial class EnumerableEx
    {
        public static IEnumerable<IEnumerable<TSource>> Batch<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            int count)
        {
            source.CheckArgumentNull("source");
            count.CheckArgumentOutOfRange("count", 1, int.MaxValue);

            return source
                .WithIndex()
                .GroupBy(x => x.Index / count)
                .Select(g => g.WithoutIndex());
        }
    }
}
