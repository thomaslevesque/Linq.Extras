using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    partial class EnumerableExtensions
    {
        public static IEnumerable<T> Apply<T>(
            [NotNull] this IEnumerable<T> source,
            [NotNull] Action<T> action)
            where T : class
        {
            source.CheckArgumentNull("source");
            action.CheckArgumentNull("action");
            return source.ApplyIterator(action);
        }

        private static IEnumerable<T> ApplyIterator<T>(this IEnumerable<T> source, Action<T> action)
            where T : class
        {
            foreach (var item in source)
            {
                action(item);
                yield return item;
            }
        }

        public static IEnumerable<T> Apply<T>(
            [NotNull] this IEnumerable<T> source,
            [NotNull] ByRefAction<T> action)
            where T : struct
        {
            source.CheckArgumentNull("source");
            action.CheckArgumentNull("action");
            return source.ApplyIterator(action);
        }

        private static IEnumerable<T> ApplyIterator<T>(this IEnumerable<T> source, ByRefAction<T> action)
            where T : struct
        {
            foreach (var item in source)
            {
                T it = item;
                action(ref it);
                yield return it;
            }
        }
    }

    public delegate void ByRefAction<T>(ref T param);
}
