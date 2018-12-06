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
        /// Builds a hierarchy from a flat sequence of elements, based on an Id/ParentId relation.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in <c>source</c>.</typeparam>
        /// <typeparam name="TId">The type of the elements' identifier.</typeparam>
        /// <param name="source">The flat sequence of elements to build a hierarchy from.</param>
        /// <param name="idSelector">A function that returns the id of an element.</param>
        /// <param name="parentIdSelector">A function that returns the parent id of an element.</param>
        /// <param name="rootParentId">The parent id of the root elements (0 or -1, for instance).</param>
        /// <returns>A sequence containing the root nodes of the hierarchy. Each node in the hierarchy has a collection of child nodes and a link to the parent node.</returns>
        [Pure]
        public static IEnumerable<INode<TSource>> ToHierarchy<TSource, TId>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TId> idSelector,
            [NotNull] Func<TSource, TId> parentIdSelector,
            TId rootParentId = default(TId))
        {
            source.CheckArgumentNull(nameof(source));
            idSelector.CheckArgumentNull(nameof(idSelector));
            parentIdSelector.CheckArgumentNull(nameof(parentIdSelector));
            
            return ToHierarchyImpl(source, idSelector, parentIdSelector, rootParentId);
        }

        private static IEnumerable<INode<TSource>> ToHierarchyImpl<TSource, TId>(
            IEnumerable<TSource> source,
            Func<TSource, TId> idSelector,
            Func<TSource, TId> parentIdSelector,
            TId rootParentId)
        {
            var lookup = source.ToLookup(parentIdSelector, item => new Node<TSource> { Item = item });
            var roots = lookup[rootParentId].ToList();
            var queue = roots.ToQueue();

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                var children = new List<INode<TSource>>();
                var id = idSelector(node.Item);
                foreach (var childNode in lookup[id])
                {
                    childNode.Level = node.Level + 1;
                    childNode.Parent = node;
                    children.Add(childNode);
                    queue.Enqueue(childNode);
                }
                node.Children = children;
            }
            return roots;
        }

        private sealed class Node<T> : INode<T>
        {
            public T Item { get; set; }
            public IList<INode<T>> Children { get; set; }
            public int Level { get; set; }
            public INode<T> Parent { get; set; }
        }
    }
}
