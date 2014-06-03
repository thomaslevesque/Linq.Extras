using JetBrains.Annotations;

namespace Linq.Extras
{
    [PublicAPI]
    public interface IIndexedItem<T>
    {
        int Index { get; }
        T Value { get; }
    }
}