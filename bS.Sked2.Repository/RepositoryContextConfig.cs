using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Repository
{
    public class RepositoryContextConfig
    {
        public string ConnectionString { get; set; }
        public string ExtraDllEntityModelFolders { get; set; }
        public string DbType { get; set; }
    }
}
