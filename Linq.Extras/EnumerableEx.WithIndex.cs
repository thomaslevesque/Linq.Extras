using System.Collections.Generic;
using System.Linq;

namespace Linq.Extras
{
    partial class EnumerableEx
    {
        public static IEnumerable<IndexedItem<TSource>> WithIndex<TSource>(this IEnumerable<TSource> source)
        {
            return source.Select((item, index) => new IndexedItem<TSource>(index, item));
        }

        public static IEnumerable<TSource> WithoutIndex<TSource>(this IEnumerable<IndexedItem<TSource>> source)
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
