using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    partial class EnumerableEx
    {
        [Pure]
        public static IEnumerable<TSource> DistinctUntilChanged<TSource>([NotNull] this IEnumerable<TSource> source)
        {
            source.CheckArgumentNull("source");
            return source.DistinctUntilChangedImpl(Identity, null);
        }

        /// <summary>
        /// Renvoie une séquence qui contient seulement des valeurs contiguës distinctes selon le comparateur spécifié.
        /// </summary>
        /// <typeparam name="TSource">Type des éléments de la séquence</typeparam>
        /// <param name="source">Séquence d'entrée</param>
        /// <param name="comparer">Comparateur à utiliser pour comparer les éléments. Si null, le comparateur par défaut sera utilisé</param>
        /// <returns>Une séquence qui contient seulement des valeurs contiguës distinctes</returns>
        [Pure]
        public static IEnumerable<TSource> DistinctUntilChanged<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            IEqualityComparer<TSource> comparer)
        {
            source.CheckArgumentNull("source");
            return source.DistinctUntilChangedImpl(Identity, comparer);
        }

        /// <summary>
        /// Renvoie une séquence qui contient seulement des valeurs contiguës distinctes selon le comparateur spécifié.
        /// </summary>
        /// <typeparam name="TSource">Type des éléments de la séquence</typeparam>
        /// <typeparam name="TKey">Type de la clé de comparaison</typeparam>
        /// <param name="source">Séquence d'entrée</param>
        /// <param name="keySelector">Fonction qui renvoie la clé à utiliser pour comparer les éléments</param>
        /// <returns>Une séquence qui contient seulement des valeurs contiguës distinctes</returns>
        [Pure]
        public static IEnumerable<TSource> DistinctUntilChanged<TSource, TKey>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TKey> keySelector)
        {
            source.CheckArgumentNull("source");
            keySelector.CheckArgumentNull("keySelector");
            return source.DistinctUntilChangedImpl(keySelector, null);
        }

        /// <summary>
        /// Renvoie une séquence qui contient seulement des valeurs contiguës distinctes selon le comparateur spécifié.
        /// </summary>
        /// <typeparam name="TSource">Type des éléments de la séquence</typeparam>
        /// <typeparam name="TKey">Type de la clé de comparaison</typeparam>
        /// <param name="source">Séquence d'entrée</param>
        /// <param name="keySelector">Fonction qui renvoie la clé à utiliser pour comparer les éléments</param>
        /// <param name="keyComparer">Comparateur à utiliser pour comparer les clés. Si null, le comparateur par défaut sera utilisé</param>
        /// <returns>Une séquence qui contient seulement des valeurs contiguës distinctes</returns>
        [Pure]
        public static IEnumerable<TSource> DistinctUntilChanged<TSource, TKey>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> keyComparer)
        {
            source.CheckArgumentNull("source");
            keySelector.CheckArgumentNull("keySelector");

            return source.DistinctUntilChangedImpl(keySelector, keyComparer);
        }

        [Pure]
        private static IEnumerable<TSource> DistinctUntilChangedImpl<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> keyComparer)
        {
            keyComparer = keyComparer ?? EqualityComparer<TKey>.Default;
            using (var en = source.GetEnumerator())
            {
                if (!en.MoveNext())
                    yield break;

                yield return en.Current;
                TKey prevKey = keySelector(en.Current);

                while (en.MoveNext())
                {
                    TKey key = keySelector(en.Current);
                    if (!keyComparer.Equals(prevKey, key))
                    {
                        yield return en.Current;
                        prevKey = key;
                    }
                }
            }
        }

        [Pure]
        static T Identity<T>(T arg) { return arg; }
    }
}
