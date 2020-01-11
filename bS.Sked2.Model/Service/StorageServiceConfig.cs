using bS.Sked2.Structure.Service;

namespace bS.Sked2.Model.Service
{
    /// <summary>
    /// Configuration for StorageService
    /// </summary>
    /// <seealso cref="IStorageServiceConfig" />
    public class StorageServiceConfig : IStorageServiceConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StorageServiceConfig"/> class.
        /// </summary>
        /// <param name="rootPath">The root path for the virtual file system.</param>
        public StorageServiceConfig(string rootPath)
        {
            RootPath = rootPath;
        }

        /// <summary>
        /// Gets the root path of the virtual file system.
        /// </summary>
        /// <value>
        /// The root path.
        /// </value>
        public string RootPath { get; }
    }
}