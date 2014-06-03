using System.Collections.Generic;
using JetBrains.Annotations;

namespace Linq.Extras
{
    [PublicAPI]
    public interface INode<T>
    {
        T Item { get; }
        IList<INode<T>> Children { get; }
        int Level { get; }
        INode<T> Parent { get; }
    }
}