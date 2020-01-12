using System.Collections.Generic;

namespace bS.Sked2.Structure.Engine.UI
{
    public interface IElementDefinitionCreate
    {
        IElementType ElementType { get; set; }
        string Name { get; set; }
        string Description { get; set; }

        IEnumerable<IElementType> ElementTypesList { get; set; }


    }
}
