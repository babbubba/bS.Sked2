using bS.Sked2.Structure.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Models
{
    public interface IElementPropertyEntry
    {
        string Key { get; set; }
        string Description { get; set; }
        DataType DataType { get; set; }
        bool Mandatory { get; set; }
        string Value { get; set; }
    }
}
