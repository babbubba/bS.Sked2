using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Base.FileSystem
{
    /// <summary>
    /// Represent a virtual path object used in a file manager
    /// </summary>
    public interface IVirtualPath
    {
        string Path { get; }
        string RealPath { get; }
    }

  

}
