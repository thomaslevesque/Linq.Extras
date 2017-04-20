﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Linq.Extras
{
    partial class XEnumerable
    {
        /// <summary>
        /// Indicates whether a sequence is null or empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">The sequence to check.</param>
        /// <returns>true if the source sequence is null or empty; otherwise, false.</returns>
        [Pure]
        [ContractAnnotation("source:null => true")]
        public static bool IsNullOrEmpty<TSource>(
            this IEnumerable<TSource> source)
        {
            return source == null || !source.Any();
        }

        /// <summary>
        /// Indicates whether a sequence is null or empty.
        /// </summary>
        /// <param name="source">The sequence to check.</param>
        /// <returns>true if the source sequence is null or empty; otherwise, false.</returns>
        [Pure]
        [ContractAnnotation("source:null => true")]
        public static bool IsNullOrEmpty(
            this IEnumerable source)
        {
            if (source == null)
                return true;
            var collection = source as ICollection;
            if (collection != null)
                return collection.Count == 0;
            var enumerator = source.GetEnumerator();
            return !enumerator.MoveNext();
        }
    }
}
