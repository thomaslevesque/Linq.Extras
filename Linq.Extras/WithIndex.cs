using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Linq.Extras
{
    partial class XEnumerable
    {
        [Pure]
        public static IEnumerable<IndexedItem<TSource>> WithIndex<TSource>(
            [NotNull] this IEnumerable<TSource> source)
        {
            return source.Select((item, index) => new IndexedItem<TSource>(index, item));
        }

        [Pure]
        public static IEnumerable<TSource> WithoutIndex<TSource>(
            [NotNull] this IEnumerable<IndexedItem<TSource>> source)
        {
            return source.Select(indexed => indexed.Value);
        }
    }

    public class IndexedItem<T>
    {
        public IndexedItem(int index, T value)
        {
            Index = index;
            Value = value;
        }

        public int Index { get; private set; }
        public T Value { get; private set; }
    }
}
