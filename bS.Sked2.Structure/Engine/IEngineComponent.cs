using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine
{
    public interface IEngineComponent
    {
        Guid? InstanceID { get; }
    }
}
