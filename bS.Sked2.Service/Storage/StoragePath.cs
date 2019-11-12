using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Service.Storage
{
    public class StoragePath : Structure.Base.FileSystem.IVirtualPath
    {
        public StoragePath(string path)
        {
            Path = path;
        }
        public string Path { get; }

        public string RealPath => Path;
    }
}
