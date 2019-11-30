using JetBrains.Annotations;

namespace Linq.Extras
{
    /// <summary>
    /// Represents an element associated with its index in a sequence.
    /// </summary>
    /// <typeparam name="T">The type of the element.</typeparam>
    [PublicAPI]
    public readonly struct ItemWithIndex<T>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ItemWithIndex{T}"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="index"></param>
        public ItemWithIndex(T item, int index)
        {
            Item = item;
            Index = index;
        }

        /// <summary>
        /// The value of the element.
        /// </summary>
        public T Item { get; }

        /// <summary>
        /// The index of the element in the sequence.
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// Deconstructs this <see cref="ItemWithIndex{T}"/> into its item and index.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="index">The index.</param>
        public void Deconstruct(out T item, out int index)
        {
            item = Item;
            index = Index;
        }
    }
}