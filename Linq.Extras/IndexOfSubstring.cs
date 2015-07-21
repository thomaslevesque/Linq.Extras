using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    partial class XEnumerable
    {
        /// <summary>
        /// Returns the position of the first occurrence of the specified <c>substring</c> in the <c>source</c> sequence, or -1 if it is not found.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The sequence to search for a substring.</param>
        /// <param name="substring">The substring to search.</param>
        /// <param name="comparer">The comparer to use to test equality between elements.</param>
        /// <returns>The zero-based index of the start of the first occurrence of <c>substring</c> if it's found; otherwise, -1.</returns>
        /// <remarks>Here the word <c>substring</c> is to be understood in the mathematical sense; it's not related to character strings. See the <see href="http://en.wikipedia.org/wiki/Substring">definition</see> on Wikipedia.</remarks>
        [Pure]
        public static int IndexOfSubstring<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] IEnumerable<TSource> substring,
            IEqualityComparer<TSource> comparer = null)
        {
            source.CheckArgumentNull(nameof(source));
            substring.CheckArgumentNull(nameof(substring));

            comparer = comparer ?? EqualityComparer<TSource>.Default;

            var sub = substring.ToArray();
            if (sub.Length == 0)
                return 0;

            int p = 0; // current position in source
            int i = 0; // current position in substring
            var prospects = new List<int>(); // list of prospective matches
            foreach (var item in source)
            {
                // Remove bad prospective matches
                prospects.RemoveAll(k => !comparer.Equals(item, sub[p - k]));

                // Is it the start of a prospective match ?
                if (comparer.Equals(item, sub[0]))
                {
                    prospects.Add(p);
                }

                // Does current character continue partial match ?
                if (comparer.Equals(item, sub[i]))
                {
                    i++;
                    // Do we have a complete match ?
                    if (i == sub.Length)
                    {
                        // Bingo !
                        return p - sub.Length + 1;
                    }
                }
                else // Mismatch
                {
                    // Do we have prospective matches to fall back to ?
                    if (prospects.Count > 0)
                    {
                        // Yes, use the first one
                        int k = prospects[0];
                        i = p - k + 1;
                    }
                    else
                    {
                        // No, start from beginning of substring
                        i = 0;
                    }
                }
                p++;
            }
            // No match
            return -1;
        }
    }
}
