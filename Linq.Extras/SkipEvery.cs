using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    partial class EnumerableExtensions
    {
        [Pure]
        public static IEnumerable<TSource> SkipEvery<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            int frequency)
        {
            return source.SkipEvery(frequency, 0);
        }

        [Pure]
        public static IEnumerable<TSource> SkipEvery<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            int frequency,
            int start)
        {
            source.CheckArgumentNull("source");
            frequency.CheckArgumentOutOfRange("frequency", 1, int.MaxValue);
            start.CheckArgumentOutOfRange("start", 0, int.MaxValue);

            return source
                .Where((item, index) => index < start || (index - start) % frequency != 0);
        }
    }
}
