using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Models
{
    public interface IElementsMappingEntry
    {
        string InputPropertyKey { get; set; }
        string OutputPropertyKey { get; set; }
        IElementsLinkEntry ParentLink { get; set; }
    }
}
