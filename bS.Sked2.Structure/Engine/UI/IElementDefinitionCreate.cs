using System;
using System.Collections.Generic;

namespace bS.Sked2.Structure.Engine.UI
{
    public interface IElementDefinitionCreate
    {
        string ElementTypeKey { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        Guid ParentTaskId { get; set; }

        IEnumerable<IElementType> ElementTypesList { get; set; }
    }
}
