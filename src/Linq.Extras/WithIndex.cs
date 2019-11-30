using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Linq.Extras
{
    partial class XEnumerable
    {
        /// <summary>
        /// Associates an index to each element of the source sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <returns>A sequence of elements paired with their index in the sequence.</returns>
        [Pure]
        public static WithIndexEnumerable<TSource> WithIndex<TSource>(
            [NotNull] this IEnumerable<TSource> source)
        {
            return new WithIndexEnumerable<TSource>(source);
        }

        /// <summary>
        /// Removes the indexes from a sequence of indexed elements, returning only the elements themselves.
        /// </summary>
        /// <typeparam name="TSource">The type of the indexed elements.</typeparam>
        /// <param name="source">The sequence to remove the indexes from.</param>
        /// <returns>A sequence of elements without their associated indexes.</returns>
        [Pure]
        public static IEnumerable<TSource> WithoutIndex<TSource>(
            [NotNull] this IEnumerable<ItemWithIndex<TSource>> source)
        {
            return source.Select(indexed => indexed.Item);
        }

        /// <summary>
        /// A sequence of elements associated with their index.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the source sequence.</typeparam>
        public struct WithIndexEnumerable<TSource> : IEnumerable<ItemWithIndex<TSource>>
        {
            private readonly IEnumerable<TSource> _source;

            internal WithIndexEnumerable(IEnumerable<TSource> source)
            {
                _source = source;
            }

            /// <summary>
            /// Returns an enumerator that iterates through the <see cref="WithIndexEnumerable{TSource}"/>.
            /// </summary>
            /// <returns>An enumerator that iterates through the <see cref="WithIndexEnumerable{TSource}"/>.</returns>
            public Enumerator GetEnumerator() => new Enumerator(_source.GetEnumerator());

            IEnumerator<ItemWithIndex<TSource>> IEnumerable<ItemWithIndex<TSource>>.GetEnumerator() => GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            /// <summary>
            /// Enumerates the elements of a <see cref="WithIndexEnumerable{TSource}"/>.
            /// </summary>
            public struct Enumerator : IEnumerator<ItemWithIndex<TSource>>
            {
                private readonly IEnumerator<TSource> _innerEnumerator;
                private int _index;

                internal Enumerator(IEnumerator<TSource> inner)
                {
                    _innerEnumerator = inner;
                    _index = -1;
                }

                /// <summary>
                /// Advances the enumerator to the next element of the <see cref="WithIndexEnumerable{TSource}"/>.
                /// </summary>
                /// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
                public bool MoveNext()
                {
                    if (_innerEnumerator.MoveNext())
                    {
                        _index++;
                        return true;
                    }

                    return false;
                }

                void IEnumerator.Reset() => _innerEnumerator.Reset();

                /// <summary>
                /// Releases all resources used by the <see cref="Enumerator"/>.
                /// </summary>
                public void Dispose() => _innerEnumerator.Dispose();

                /// <summary>
                /// Gets the element at the current position of the enumerator.
                /// </summary>
                public ItemWithIndex<TSource> Current => new ItemWithIndex<TSource>(_innerEnumerator.Current, _index);

                object IEnumerator.Current => Current;
            }
        }
    }
}
