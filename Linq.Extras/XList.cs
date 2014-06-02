using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    public static class XList
    {
        public static void Shuffle<T>([NotNull] this IList<T> list)
        {
            list.Shuffle(new Random());
        }

        public static void Shuffle<T>(
            [NotNull] this IList<T> list,
            [NotNull] Random rnd)
        {
            list.CheckArgumentNull("list");
            rnd.CheckArgumentNull("rnd");
            for (int i = list.Count - 1; i > 0; i--)
            {
                int swapIndex = rnd.Next(i + 1);
                list.Swap(i, swapIndex);
            }
        }

        public static void Swap<T>(
            [NotNull] this IList<T> list,
            int index1,
            int index2)
        {
            list.CheckArgumentNull("list");
            index1.CheckArgumentOutOfRange("index1", 0, list.Count - 1);
            index2.CheckArgumentOutOfRange("index2", 0, list.Count - 1);

            T tmp = list[index1];
            list[index1] = list[index2];
            list[index2] = tmp;
        }

        public static ReadOnlyCollection<TSource> AsReadOnly<TSource>(
            [NotNull] this IList<TSource> source)
        {
            return new ReadOnlyCollection<TSource>(source);
        }
    }
}
