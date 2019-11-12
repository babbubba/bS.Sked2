using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Base.FileSystem
{
    /// <summary>
    /// It rapresent the result value for a virtual file/folder watcher.
    /// </summary>
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
