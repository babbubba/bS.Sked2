using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Models
{
    public interface IElementsLinkEntry
    {
        IList<IElementsMappingEntry> Mappings { get; set; }
        IElementEntry Next { get; set; }
        IElementEntry Previuous { get; set; }
    }
}
