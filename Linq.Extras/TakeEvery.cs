using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    partial class EnumerableEx
    {
        public static IEnumerable<TSource> TakeEvery<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            int frequency)
        {
            return source.TakeEvery(frequency, 0);
        }

        public static IEnumerable<TSource> TakeEvery<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            int frequency,
            int start)
        {
            source.CheckArgumentNull("source");
            frequency.CheckArgumentOutOfRange("frequency", 1, int.MaxValue);
            start.CheckArgumentOutOfRange("start", 0, int.MaxValue);

            return source
                .Skip(start)
                .Where((item, index) => index % frequency == 0);
        }
    }
}
