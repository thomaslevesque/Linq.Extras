using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    partial class XEnumerable
    {
        [Pure]
        public static IEnumerable<IEnumerable<TSource>> Batch<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            int size)
        {
            source.CheckArgumentNull("source");
            size.CheckArgumentOutOfRange("size", 1, int.MaxValue);

            return source
                .WithIndex()
                .GroupBy(x => x.Index / size)
                .Select(g => g.WithoutIndex());
        }
    }
}
