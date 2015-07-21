using JetBrains.Annotations;

namespace Linq.Extras
{
    /// <summary>
    /// Represents an element associated with its index in a sequence.
    /// </summary>
    /// <typeparam name="T">The type of the element.</typeparam>
    [PublicAPI]
    public interface IIndexedItem<out T>
    {
        /// <summary>
        /// The index of the element in a sequence.
        /// </summary>
        int Index { get; }
        /// <summary>
        /// The value of the element.
        /// </summary>
        T Item { get; }
    }
}