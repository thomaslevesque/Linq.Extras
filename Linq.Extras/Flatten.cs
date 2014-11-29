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
        /// Returns a flattened sequence from a graph or hierarchy of elements, using the specified children selector,
        /// and in the specified traversal order.
        /// </summary>
        /// <typeparam name="TNode">The type of the elements in the source hierarchy.</typeparam>
        /// <typeparam name="TResult">The type of the elements in the output sequence.</typeparam>
        /// <param name="source">The source hierarchy to flatten.</param>
        /// <param name="childrenSelector">The delegate used to retrieve the children of an element.</param>
        /// <param name="traversalMode">The traversal order.</param>
        /// <param name="resultSelector">The delegate used to project each node of the hierarchy to a result element. It accepts the node and its level as paramaters.</param>
        /// <returns>A flat sequence of elements produced from the elements in the source hierarchy.</returns>
        [Pure]
        public static IEnumerable<TResult> Flatten<TNode, TResult>(
            [NotNull] this IEnumerable<TNode> source,
            [NotNull] Func<TNode, IEnumerable<TNode>> childrenSelector,
            TreeTraversalMode traversalMode,
            [NotNull] Func<TNode, int, TResult> resultSelector)
        {
            source.CheckArgumentNull("source");
            childrenSelector.CheckArgumentNull("childrenSelector");
            resultSelector.CheckArgumentNull("resultSelector");

            switch (traversalMode)
            {
                case TreeTraversalMode.DepthFirst:
                    return source.DepthFirstFlattenIterator(childrenSelector, resultSelector);
                case TreeTraversalMode.BreadthFirst:
                    return source.BreadthFirstFlattenIterator(childrenSelector, resultSelector);
                default:
                    throw new ArgumentOutOfRangeException("traversalMode");
            }
        }

        /// <summary>
        /// Returns a flattened sequence from a graph or hierarchy of elements, using the specified children selector,
        /// and in the specified traversal order.
        /// </summary>
        /// <typeparam name="TNode">The type of the elements in the source hierarchy.</typeparam>
        /// <typeparam name="TResult">The type of the elements in the output sequence.</typeparam>
        /// <param name="source">The source hierarchy to flatten.</param>
        /// <param name="childrenSelector">The delegate used to retrieve the children of an element.</param>
        /// <param name="traversalMode">The traversal order.</param>
        /// <param name="resultSelector">The delegate used to project each node of the hierarchy to a result element. It accepts the node as a paramater.</param>
        /// <returns>A flat sequence of elements produced from the elements in the source hierarchy.</returns>
        [Pure]
        public static IEnumerable<TResult> Flatten<TNode, TResult>(
            [NotNull] this IEnumerable<TNode> source,
            [NotNull] Func<TNode, IEnumerable<TNode>> childrenSelector,
            TreeTraversalMode traversalMode,
            [NotNull] Func<TNode, TResult> resultSelector)
        {
            resultSelector.CheckArgumentNull("resultSelector");
            return source.Flatten(childrenSelector, traversalMode, (x, _) => resultSelector(x));
        }

        /// <summary>
        /// Returns a flattened sequence from a graph or hierarchy of elements, using the specified children selector,
        /// and in the specified traversal order.
        /// </summary>
        /// <typeparam name="TNode">The type of the elements in the source hierarchy.</typeparam>
        /// <param name="source">The source hierarchy to flatten.</param>
        /// <param name="childrenSelector">The delegate used to retrieve the children of an element.</param>
        /// <param name="traversalMode">The traversal order.</param>
        /// <returns>A flat sequence of the elements in the source hierarchy.</returns>
        [Pure]
        public static IEnumerable<TNode> Flatten<TNode>(
            [NotNull] this IEnumerable<TNode> source,
            [NotNull] Func<TNode, IEnumerable<TNode>> childrenSelector,
            TreeTraversalMode traversalMode)
        {
            return source.Flatten(childrenSelector, traversalMode, (x, _) => x);
        }

        private static IEnumerable<TResult> BreadthFirstFlattenIterator<TNode, TResult>(this IEnumerable<TNode> source,
            Func<TNode, IEnumerable<TNode>> childrenSelector, Func<TNode, int, TResult> resultSelector)
        {
            var queue = source.Select(n => new NodeWithLevel<TNode>(n, 0)).ToQueue();
            while (queue.Count > 0)
            {
                var item = queue.Dequeue();
                yield return resultSelector(item.Node, item.Level);
                foreach (var child in childrenSelector(item.Node))
                {
                    queue.Enqueue(new NodeWithLevel<TNode>(child, item.Level + 1));
                }
            }
        }

        private static IEnumerable<TResult> DepthFirstFlattenIterator<TNode, TResult>(this IEnumerable<TNode> source,
            Func<TNode, IEnumerable<TNode>> childrenSelector, Func<TNode, int, TResult> resultSelector)
        {
            var list = source.Select(n => new NodeWithLevel<TNode>(n, 0)).ToLinkedList();
            while (list.Count > 0)
            {
                var current = list.First.Value;
                list.RemoveFirst();
                yield return resultSelector(current.Node, current.Level);
                var llNode = list.First;
                foreach (var child in childrenSelector(current.Node))
                {
                    var newNode = new NodeWithLevel<TNode>(child, current.Level + 1);
                    if (llNode != null)
                        list.AddBefore(llNode, newNode);
                    else
                        list.AddLast(newNode);
                }
            }
        }

        private sealed class NodeWithLevel<TNode>
        {
            private readonly TNode _node;
            private readonly int _level;

            public NodeWithLevel(TNode node, int level)
            {
                _node = node;
                _level = level;
            }

            // ReSharper disable once MemberHidesStaticFromOuterClass
            public TNode Node
            {
                get { return _node; }
            }

            public int Level
            {
                get { return _level; }
            }
        }
    }
}
