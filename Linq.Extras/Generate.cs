using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    partial class XEnumerable
    {
        [Pure]
        public static IEnumerable<TSource> Generate<TSource>(TSource seed, [NotNull] Func<TSource, TSource> nextSelector)
        {
            nextSelector.CheckArgumentNull("nextSelector");
            return GenerateIterator(seed, nextSelector);
        }

        private static IEnumerable<TSource> GenerateIterator<TSource>(TSource seed, Func<TSource, TSource> nextSelector)
        {
            TSource current = seed;
            while (true)
            {
                yield return current;
                current = nextSelector(current);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        [Pure]
        public static IEnumerable<TSource> Generate<TSource>([NotNull] Func<int, TSource> generator)
        {
            generator.CheckArgumentNull("generator");
            return GenerateIterator(generator);
        }

        private static IEnumerable<TSource> GenerateIterator<TSource>(Func<int, TSource> generator)
        {
            for (int i = 0; i < int.MaxValue; i++)
            {
                yield return generator(i);
            }
            yield return generator(int.MaxValue);
        }
    }
}
