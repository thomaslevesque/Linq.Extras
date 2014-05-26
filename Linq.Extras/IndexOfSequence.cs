using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    partial class EnumerableExtensions
    {
        [Pure]
        public static int IndexOfSequence<T>(
            [NotNull] this IEnumerable<T> source,
            [NotNull] IEnumerable<T> sequence)
        {
            return source.IndexOfSequence(sequence, null);
        }

        [Pure]
        public static int IndexOfSequence<T>(
            [NotNull] this IEnumerable<T> source,
            [NotNull] IEnumerable<T> sequence,
            IEqualityComparer<T> comparer)
        {
            source.CheckArgumentNull("source");
            sequence.CheckArgumentNull("sequence");

            comparer = comparer ?? EqualityComparer<T>.Default;

            var seq = sequence.ToArray();

            int p = 0; // current position in source
            int i = 0; // current position in sequence
            var prospects = new List<int>(); // list of prospective matches
            foreach (var item in source)
            {
                // Remove bad prospective matches
                prospects.RemoveAll(k => !comparer.Equals(item, seq[p - k]));

                // Is it the start of a prospective match ?
                if (comparer.Equals(item, seq[0]))
                {
                    prospects.Add(p);
                }

                // Does current character continue partial match ?
                if (comparer.Equals(item, seq[i]))
                {
                    i++;
                    // Do we have a complete match ?
                    if (i == seq.Length)
                    {
                        // Bingo !
                        return p - seq.Length + 1;
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
                        // No, start from beginning of sequence
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
