namespace bS.Sked2.Structure.Engine.Data.Types
{
    /// <summary>
    /// Virtual path (file or folder) rapresentation.
    /// </summary>
    /// <seealso cref="bS.Sked2.Structure.Base.FileSystem.IVirtualPath" />
    public class VirtualPath : Base.FileSystem.IVirtualPath
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualPath"/> class.
        /// </summary>
        public VirtualPath()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualPath"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public VirtualPath(string path)
        {
            Path = path;
        }

        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public string Path { get; }

        /// <summary>
        /// Gets the real path.
        /// </summary>
        /// <value>
        /// The real path.
        /// </value>
        public string RealPath => Path;
    }
}