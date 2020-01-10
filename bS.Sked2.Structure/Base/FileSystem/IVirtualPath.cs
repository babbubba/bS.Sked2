namespace bS.Sked2.Structure.Base.FileSystem
{
    /// <summary>
    /// Represent a virtual path object used in a file manager
    /// </summary>
    public interface IVirtualPath
    {
        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        string Path { get; }

        /// <summary>
        /// Gets the real path.
        /// </summary>
        /// <value>
        /// The real path.
        /// </value>
        string RealPath { get; }
    }
}