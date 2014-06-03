using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Linq.Extras
{
    partial class XEnumerable
    {
        [Pure]
        public static IEnumerable<IIndexedItem<TSource>> WithIndex<TSource>(
            [NotNull] this IEnumerable<TSource> source)
        {
            return source.Select((item, index) => new IndexedItem<TSource>(index, item));
        }

        [Pure]
        public static IEnumerable<TSource> WithoutIndex<TSource>(
            [NotNull] this IEnumerable<IIndexedItem<TSource>> source)
        {
            return source.Select(indexed => indexed.Value);
        }

        sealed class IndexedItem<T> : IIndexedItem<T>
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

    public interface IIndexedItem<T>
    {
        int Index { get; }
        T Value { get; }
    }
}
