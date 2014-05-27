using System.Collections.Generic;
using JetBrains.Annotations;

namespace Linq.Extras
{
    /// <summary>
    /// Provides extension and helper methods that work on sequences and collections.
    /// </summary>
    public static partial class XEnumerable
    {
        [Pure]
        public static IEnumerable<TSource> Unit<TSource>(TSource item)
        {
            yield return item;
        }
    }
}
