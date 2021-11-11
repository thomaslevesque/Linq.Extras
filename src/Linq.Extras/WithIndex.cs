using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using NotNullAttribute = JetBrains.Annotations.NotNullAttribute;
using Linq.Extras.Internal;

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
            source.CheckArgumentNull(nameof(source));
            return new WithIndexEnumerable<TSource>(source);
        }

        /// <summary>
        /// Associates an index to each element of the source array.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The source array.</param>
        /// <returns>A sequence of elements paired with their index in the array.</returns>
        [Pure]
        public static WithIndexArrayEnumerable<TSource> WithIndex<TSource>(
            [NotNull] this TSource[] source)
        {
            source.CheckArgumentNull(nameof(source));
            return new WithIndexArrayEnumerable<TSource>(source);
        }

        /// <summary>
        /// Associates an index to each element of the source <see cref="List{T}"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The source list.</param>
        /// <returns>A sequence of elements paired with their index in the list.</returns>
        [Pure]
        public static WithIndexListEnumerable<TSource> WithIndex<TSource>(
            [NotNull] this List<TSource> source)
        {
            source.CheckArgumentNull(nameof(source));
            return new WithIndexListEnumerable<TSource>(source);
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

            [ExcludeFromCodeCoverage]
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

                [ExcludeFromCodeCoverage]
                void IEnumerator.Reset()
                {
                    _index = -1;
                    _innerEnumerator.Reset();
                }

                /// <summary>
                /// Releases all resources used by the <see cref="Enumerator"/>.
                /// </summary>
                public void Dispose() => _innerEnumerator.Dispose();

                /// <summary>
                /// Gets the element at the current position of the enumerator.
                /// </summary>
                public ItemWithIndex<TSource> Current => new ItemWithIndex<TSource>(_innerEnumerator.Current, _index);

                [ExcludeFromCodeCoverage]
                object IEnumerator.Current => Current;
            }
        }

        /// <summary>
        /// A sequence of elements associated with their index, optimized for the case when the underlying collection is an array.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the source array.</typeparam>
        public struct WithIndexArrayEnumerable<TSource> : IEnumerable<ItemWithIndex<TSource>>
        {
            private readonly TSource[] _array;

            internal WithIndexArrayEnumerable(TSource[] array)
            {
                _array = array;
            }

            /// <summary>
            /// Returns an enumerator that iterates through the <see cref="WithIndexArrayEnumerable{TSource}"/>.
            /// </summary>
            /// <returns>An enumerator that iterates through the <see cref="WithIndexArrayEnumerable{TSource}"/>.</returns>
            public Enumerator GetEnumerator() => new Enumerator(_array);

            IEnumerator<ItemWithIndex<TSource>> IEnumerable<ItemWithIndex<TSource>>.GetEnumerator() => GetEnumerator();

            [ExcludeFromCodeCoverage]
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            /// <summary>
            /// Enumerates the elements of a <see cref="WithIndexArrayEnumerable{TSource}"/>.
            /// </summary>
            public struct Enumerator : IEnumerator<ItemWithIndex<TSource>>
            {
                private readonly TSource[] _array;
                private int _index;

                internal Enumerator(TSource[] array)
                {
                    _array = array;
                    _index = -1;
                }

                /// <summary>
                /// Advances the enumerator to the next element of the <see cref="WithIndexArrayEnumerable{TSource}"/>.
                /// </summary>
                /// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
                public bool MoveNext()
                {
                    if (_index + 1 < _array.Length)
                    {
                        _index++;
                        return true;
                    }

                    return false;
                }

                void IEnumerator.Reset()
                {
                    _index = -1;
                }

                /// <summary>
                /// Releases all resources used by the <see cref="Enumerator"/>.
                /// </summary>
                public void Dispose()
                {
                }

                /// <summary>
                /// Gets the element at the current position of the enumerator.
                /// </summary>
                public ItemWithIndex<TSource> Current => new ItemWithIndex<TSource>(_array[_index], _index);

                [ExcludeFromCodeCoverage]
                object IEnumerator.Current => Current;
            }
        }

        /// <summary>
        /// A sequence of elements associated with their index, optimized for the case when the underlying collection is a <see cref="List{T}"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the source list.</typeparam>
        public struct WithIndexListEnumerable<TSource> : IEnumerable<ItemWithIndex<TSource>>
        {
            private readonly List<TSource> _list;

            internal WithIndexListEnumerable(List<TSource> list)
            {
                _list = list;
            }

            /// <summary>
            /// Returns an enumerator that iterates through the <see cref="WithIndexListEnumerable{TSource}"/>.
            /// </summary>
            /// <returns>An enumerator that iterates through the <see cref="WithIndexListEnumerable{TSource}"/>.</returns>
            public Enumerator GetEnumerator() => new Enumerator(_list.GetEnumerator());

            IEnumerator<ItemWithIndex<TSource>> IEnumerable<ItemWithIndex<TSource>>.GetEnumerator() => GetEnumerator();

            [ExcludeFromCodeCoverage]
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            /// <summary>
            /// Enumerates the elements of a <see cref="WithIndexListEnumerable{TSource}"/>.
            /// </summary>
            public struct Enumerator : IEnumerator<ItemWithIndex<TSource>>
            {
                private List<TSource>.Enumerator _inner;
                private int _index;

                internal Enumerator(List<TSource>.Enumerator inner)
                {
                    _inner = inner;
                    _index = -1;
                }

                /// <summary>
                /// Advances the enumerator to the next element of the <see cref="WithIndexListEnumerable{TSource}"/>.
                /// </summary>
                /// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
                public bool MoveNext()
                {
                    if (_inner.MoveNext())
                    {
                        _index++;
                        return true;
                    }

                    return false;
                }

                void IEnumerator.Reset()
                {
                    _index = -1;
                    Reset(ref _inner);
                }
                
                private static void Reset<TEnumerator>(ref TEnumerator enumerator)
                    where TEnumerator : struct, IEnumerator
                {
                    enumerator.Reset();
                }

                /// <summary>
                /// Releases all resources used by the <see cref="Enumerator"/>.
                /// </summary>
                public void Dispose() => _inner.Dispose();

                /// <summary>
                /// Gets the element at the current position of the enumerator.
                /// </summary>
                public ItemWithIndex<TSource> Current => new ItemWithIndex<TSource>(_inner.Current, _index);

                [ExcludeFromCodeCoverage]
                object IEnumerator.Current => Current;
            }
        }
    }
}
