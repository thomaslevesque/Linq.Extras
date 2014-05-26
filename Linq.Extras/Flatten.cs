using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    partial class EnumerableExtensions
    {
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

        public static IEnumerable<TNode> Flatten<TNode>(
            [NotNull] this IEnumerable<TNode> source,
            [NotNull] Func<TNode, IEnumerable<TNode>> childrenSelector,
            TreeTraversalMode traversalMode)
        {
            return Flatten(source, childrenSelector, traversalMode, (x, _) => x);
        }

        private static IEnumerable<TResult> BreadthFirstFlattenIterator<TNode, TResult>(this IEnumerable<TNode> source,
            Func<TNode, IEnumerable<TNode>> childrenSelector, Func<TNode, int, TResult> resultSelector)
        {
            var queue = new Queue<NodeWithLevel<TNode>>(source.Select(n => new NodeWithLevel<TNode>(n, 0)));
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
            var list = new LinkedList<NodeWithLevel<TNode>>(source.Select(n => new NodeWithLevel<TNode>(n, 0)));
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

        private class NodeWithLevel<TNode>
        {
            private readonly TNode _node;
            private readonly int _level;

            public NodeWithLevel(TNode node, int level)
            {
                _node = node;
                _level = level;
            }

            // ReSharper disable MemberHidesStaticFromOuterClass
            public TNode Node
                // ReSharper restore MemberHidesStaticFromOuterClass
            {
                get { return _node; }
            }

            public int Level
            {
                get { return _level; }
            }
        }
    }

    public enum TreeTraversalMode
    {
        DepthFirst,
        BreadthFirst
    }
}
