using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    partial class EnumerableExtensions
    {
        public static IEnumerable<INode<TSource>> ToHierarchy<TSource, TId>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TId> idSelector,
            [NotNull] Func<TSource, TId> parentIdSelector)
        {
            return ToHierarchy(source, idSelector, parentIdSelector, default(TId));
        }

        public static IEnumerable<INode<TSource>> ToHierarchy<TSource, TId>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TId> idSelector,
            [NotNull] Func<TSource, TId> parentIdSelector,
            TId rootParentId)
        {
            source.CheckArgumentNull("source");
            idSelector.CheckArgumentNull("idSelector");
            parentIdSelector.CheckArgumentNull("parentIdSelector");
            
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

        private class Node<T> : INode<T>
        {
            public T Item { get; set; }
            public IList<INode<T>> Children { get; set; }
            public int Level { get; set; }
            public INode<T> Parent { get; set; }
        }
    }

    public interface INode<T>
    {
        T Item { get; }
        IList<INode<T>> Children { get; }
        int Level { get; }
        INode<T> Parent { get; }
    }
}
