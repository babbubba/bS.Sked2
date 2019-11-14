using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine
{
    public interface IEngineElementProperty
    {
        string Key { get; }
        string Description { get; }
        DataType DataType { get; }
        bool Mandatory { get; }
        IEngineData Value { get; set; }
    }
}
