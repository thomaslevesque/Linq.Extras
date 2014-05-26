using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    partial class EnumerableExtensions
    {
        public static Queue<TSource> ToQueue<TSource>([NotNull] this IEnumerable<TSource> source)
        {
            source.CheckArgumentNull("source");
            return new Queue<TSource>(source);
        }

        public static Stack<TSource> ToStack<TSource>([NotNull] this IEnumerable<TSource> source)
        {
            source.CheckArgumentNull("source");
            return new Stack<TSource>(source);
        }

        public static HashSet<TSource> ToHashSet<TSource>([NotNull] this IEnumerable<TSource> source)
        {
            return source.ToHashSet(null);
        }

        public static HashSet<TSource> ToHashSet<TSource>([NotNull] this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            source.CheckArgumentNull("source");
            return new HashSet<TSource>(source, comparer);
        }

        public static LinkedList<TSource> ToLinkedList<TSource>([NotNull] this IEnumerable<TSource> source)
        {
            source.CheckArgumentNull("source");
            return new LinkedList<TSource>(source);
        }
    }
}
