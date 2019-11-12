using bS.Sked2.Structure.Service;

namespace bS.Sked2.Service.Storage
{
    public class StorageServiceConfig : IStorageServiceConfig
    {
        public StorageServiceConfig(string rootPath)
        {
            RootPath = rootPath;
        }

        public string RootPath { get; }
    }
}
