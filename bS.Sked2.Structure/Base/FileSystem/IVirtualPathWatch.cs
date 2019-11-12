using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Base.FileSystem
{
    public interface IVirtualPathWatch
    {
        event EventHandler<string> Renamed;
        event EventHandler<string> Changed;
        event EventHandler<string> Created;
        event EventHandler<string> Deleted;

        bool EnableRaisingEvents { set; }
        bool IncludeSubdirectories { set; }

    }
}
