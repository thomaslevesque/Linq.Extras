using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    partial class XEnumerable
    {
        /// <summary>
        /// Generates a sequence from a seed and a generator function.
        /// </summary>
        /// <typeparam name="TElement">The type of the elements in the sequence.</typeparam>
        /// <param name="seed">The first element of the sequence.</param>
        /// <param name="generator">A generator function that returns the next element from the current element.</param>
        /// <returns>An infinite sequence of generated elements.</returns>
        [Pure]
        public static IEnumerable<TElement> Generate<TElement>(TElement seed, [NotNull] Func<TElement, TElement> generator)
        {
            generator.CheckArgumentNull("generator");
            return GenerateIterator(seed, generator);
        }

        private static IEnumerable<TElement> GenerateIterator<TElement>(TElement seed, Func<TElement, TElement> generator)
        {
            TElement current = seed;
            while (true)
            {
                yield return current;
                current = generator(current);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        /// <summary>
        /// Generates a sequence from an index-based generator function.
        /// </summary>
        /// <typeparam name="TElement">The type of the elements in the sequence.</typeparam>
        /// <param name="generator">A generator function that returns an element based on its index.</param>
        /// <returns>An sequence of elements generated from their index.</returns>
        /// <remarks>The index varies from 0 (inclusive) to <c>int.MaxValue</c> (exclusive), so the output sequence contains <c>int.MaxValue</c> elements.</remarks>
        [Pure]
        public static IEnumerable<TElement> Generate<TElement>([NotNull] Func<int, TElement> generator)
        {
            generator.CheckArgumentNull("generator");
            return Enumerable.Range(0, int.MaxValue).Select(generator);
        }
    }
}
