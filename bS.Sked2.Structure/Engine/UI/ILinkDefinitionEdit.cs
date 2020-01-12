using System;
using System.Collections.Generic;

namespace bS.Sked2.Structure.Engine.UI
{
    public interface ILinkDefinitionEdit
    {
        Guid PreviousElement { get; set; }
        Guid NextElement { get; set; }
        IList<ILinkMapping> Mappings { get; set; }
    }
}
