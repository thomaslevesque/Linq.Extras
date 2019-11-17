using System.Collections.Generic;
using JetBrains.Annotations;

namespace Linq.Extras
{
    /// <summary>
    /// Represents a node in a hierarchy.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the hierarchy.</typeparam>
    [PublicAPI]
    public interface INode<T>
    {
        /// <summary>
        /// The value of the element contained in this node.
        /// </summary>
        T Item { get; }
        /// <summary>
        /// The child nodes of this node.
        /// </summary>
        IList<INode<T>> Children { get; }
        /// <summary>
        ///  The depth of this node in the hierarchy (root nodes have level 0).
        /// </summary>
        int Level { get; }
        /// <summary>
        /// The parent node of this node.
        /// </summary>
        INode<T>? Parent { get; }
    }
}