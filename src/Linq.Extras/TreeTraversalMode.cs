namespace Linq.Extras
{
    /// <summary>
    /// Defines the possible traversal orders when traversing a hierarchy of objects.
    /// </summary>
    public enum TreeTraversalMode
    {
        /// <summary>
        /// Depth-first traversal. For each node, explores its children before its siblings.
        /// </summary>
        DepthFirst,
        /// <summary>
        /// Breadth-first traversal. For each node, explores its siblings before its children.
        /// </summary>
        BreadthFirst
    }
}