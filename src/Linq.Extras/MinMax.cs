using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Linq.Extras.Internal;
using Linq.Extras.Properties;
using NotNullAttribute = JetBrains.Annotations.NotNullAttribute;

namespace Linq.Extras
{
    partial class XEnumerable
    {
        /// <summary>
        /// Returns the maximum element of the sequence according to the specified comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The sequence to return the maximum element from.</param>
        /// <param name="comparer">The comparer used to compare elements.</param>
        /// <returns>The maximum element according to the specified comparer.</returns>
        /// <remarks>
        /// If <c>TSource</c> is a reference type or nullable value type, null values are ignored, unless the sequence consists
        /// entirely of null values (in which case the method will return null).
        /// If <c>TSource</c> is a reference type or nullable value type, and the sequence is empty, the method will return <c>null</c>.
        /// If <c>TSource</c> is a value type, and the sequence is empty, the method will throw an <see cref="InvalidOperationException"/>.
        /// </remarks>
#if FEATURE_MINMAX_COMPARER
        [Obsolete("This feature is now implemented directly in System.Linq. Please use Enumerable.Max instead")]
#endif
        [Pure]
        [return: MaybeNull]
        public static TSource Max<TSource>(
#if FEATURE_MINMAX_COMPARER
            [NotNull] IEnumerable<TSource> source,
#else
            [NotNull] this IEnumerable<TSource> source,
#endif
            [NotNull] IComparer<TSource> comparer)
        {
            source.CheckArgumentNull(nameof(source));
            comparer.CheckArgumentNull(nameof(comparer));
            return source.Extreme(comparer, 1);
        }

        /// <summary>
        /// Returns the minimum element of the sequence according to the specified comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The sequence to return the minimum element from.</param>
        /// <param name="comparer">The comparer used to compare elements.</param>
        /// <returns>The minimum element according to the specified comparer.</returns>
        /// <remarks>
        /// If <c>TSource</c> is a reference type or nullable value type, null values are ignored, unless the sequence consists
        /// entirely of null values (in which case the method will return null).
        /// If <c>TSource</c> is a reference type or nullable value type, and the sequence is empty, the method will return <c>null</c>.
        /// If <c>TSource</c> is a value type, and the sequence is empty, the method will throw an <see cref="InvalidOperationException"/>.
        /// </remarks>
#if FEATURE_MINMAX_COMPARER
        [Obsolete("This feature is now implemented directly in System.Linq. Please use Enumerable.Min instead")]
#endif
        [Pure]
        [return: MaybeNull]
        public static TSource Min<TSource>(
#if FEATURE_MINMAX_COMPARER
            [NotNull] IEnumerable<TSource> source,
#else
            [NotNull] this IEnumerable<TSource> source,
#endif
            [NotNull] IComparer<TSource> comparer)
        {
            source.CheckArgumentNull(nameof(source));
            comparer.CheckArgumentNull(nameof(comparer));
            return source.Extreme(comparer, -1);
        }

        [Pure]
        private static TSource Extreme<TSource>(this IEnumerable<TSource> source, IComparer<TSource> comparer, int sign)
        {
            comparer = comparer ?? Comparer<TSource>.Default;
            TSource extreme = default!;

            using var e = source.GetEnumerator();
            if (extreme is null)
            {
                // For nullable types, return null if the sequence is empty
                // or contains only null values.

                // First, skip until the first non-null value, if any
                do
                {
                    if (!e.MoveNext())
                    {
                        return extreme;
                    }

                    extreme = e.Current;
                } while (extreme is null);

                while (e.MoveNext())
                {
                    if (e.Current is null)
                    {
                        continue;
                    }

                    if (Math.Sign(comparer.Compare(e.Current, extreme)) == sign)
                    {
                        extreme = e.Current;
                    }
                }
            }
            else
            {
                // For non-nullable types, throw an exception if the sequence is empty

                if (!e.MoveNext())
                {
                    throw EmptySequenceException();
                }

                extreme = e.Current;

                while (e.MoveNext())
                {
                    if (Math.Sign(comparer.Compare(e.Current, extreme)) == sign)
                    {
                        extreme = e.Current;
                    }
                }
            }

            return extreme;
        }

        private static InvalidOperationException EmptySequenceException()
        {
            return new InvalidOperationException(Resources.SequenceContainsNoElements);
        }

        /// <summary>
        /// Returns the element of the sequence that has the maximum value for the specified key.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <typeparam name="TKey">The type of the key used to compare elements.</typeparam>
        /// <param name="source">The sequence to return an element from.</param>
        /// <param name="keySelector">A delegate that returns the key used to compare elements.</param>
        /// <param name="keyComparer">A comparer to compare the keys.</param>
        /// <returns>The element of <c>source</c> that has the maximum value for the specified key.</returns>
        /// <remarks>
        /// If <c>TKey</c> is a reference type or nullable value type, null keys are ignored, unless the sequence consists
        /// entirely of items with null keys (in which case the method will return null).
        /// If <c>TKey</c> is a reference type or nullable value type, and the sequence is empty, the method will return <c>null</c>.
        /// If <c>TKey</c> is a value type, and the sequence is empty, the method will throw an <see cref="InvalidOperationException"/>.
        /// </remarks>
#if FEATURE_BY_OPERATORS
        [Obsolete("This feature is now implemented directly in System.Linq. Please use Enumerable.MaxBy instead")]
#endif
        [Pure]
        [return: MaybeNull]
        public static TSource MaxBy<TSource, TKey>(
#if FEATURE_BY_OPERATORS
            [NotNull] IEnumerable<TSource> source,
#else
            [NotNull] this IEnumerable<TSource> source,
#endif
            [NotNull] Func<TSource, TKey> keySelector,
            IComparer<TKey>? keyComparer = null)
        {
            source.CheckArgumentNull(nameof(source));
            keySelector.CheckArgumentNull(nameof(keySelector));
            return source.ExtremeBy(keySelector, keyComparer, 1);
        }

        /// <summary>
        /// Returns the element of the sequence that has the minimum value for the specified key.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <typeparam name="TKey">The type of the key used to compare elements.</typeparam>
        /// <param name="source">The sequence to return an element from.</param>
        /// <param name="keySelector">A delegate that returns the key used to compare elements.</param>
        /// <param name="keyComparer">A comparer to compare the keys.</param>
        /// <returns>The element of <c>source</c> that has the minimum value for the specified key.</returns>
        /// <remarks>
        /// If <c>TKey</c> is a reference type or nullable value type, null keys are ignored, unless the sequence consists
        /// entirely of items with null keys (in which case the method will return null).
        /// If <c>TKey</c> is a reference type or nullable value type, and the sequence is empty, the method will return <c>null</c>.
        /// If <c>TKey</c> is a value type, and the sequence is empty, the method will throw an <see cref="InvalidOperationException"/>.
        /// </remarks>
#if FEATURE_BY_OPERATORS
        [Obsolete("This feature is now implemented directly in System.Linq. Please use Enumerable.MinBy instead")]
#endif
        [Pure]
        [return: MaybeNull]
        public static TSource MinBy<TSource, TKey>(
#if FEATURE_BY_OPERATORS
            [NotNull] IEnumerable<TSource> source,
#else
            [NotNull] this IEnumerable<TSource> source,
#endif
            [NotNull] Func<TSource, TKey> keySelector,
            IComparer<TKey>? keyComparer = null)
        {
            source.CheckArgumentNull(nameof(source));
            keySelector.CheckArgumentNull(nameof(keySelector));
            return source.ExtremeBy(keySelector, keyComparer, -1);
        }

        [Pure]
        private static TSource ExtremeBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey>? keyComparer,
            int sign)
        {
            keyComparer = keyComparer ?? Comparer<TKey>.Default;
            TSource extreme = default!;
            TKey extremeKey = default!;

            using var e = source.GetEnumerator();

            if (extremeKey is null)
            {
                // For nullable types, return null if the sequence is empty
                // or contains only values with null keys.

                // First, skip until the first non-null key value, if any
                do
                {
                    if (!e.MoveNext())
                    {
                        return extreme;
                    }

                    extreme = e.Current;
                    extremeKey = keySelector(extreme);
                } while (extremeKey is null);

                while (e.MoveNext())
                {
                    var currentKey = keySelector(e.Current);
                    if (currentKey is null)
                    {
                        continue;
                    }

                    if (Math.Sign(keyComparer.Compare(currentKey, extremeKey)) == sign)
                    {
                        extreme = e.Current;
                        extremeKey = currentKey;
                    }
                }
            }
            else
            {
                // For non-nullable types, throw an exception if the sequence is empty

                if (!e.MoveNext())
                {
                    throw EmptySequenceException();
                }

                extreme = e.Current;
                extremeKey = keySelector(e.Current);

                while (e.MoveNext())
                {
                    var currentKey = keySelector(e.Current);
                    if (Math.Sign(keyComparer.Compare(currentKey, extremeKey)) == sign)
                    {
                        extreme = e.Current;
                        extremeKey = currentKey;
                    }
                }
            }

            return extreme;
        }
    }
}
