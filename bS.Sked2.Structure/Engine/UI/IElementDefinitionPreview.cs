using System.Collections.Generic;

namespace bS.Sked2.Structure.Engine.UI
{
    public interface IElementDefinitionPreview
    {
        IElementType ElementType { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        bool IsValid { get; set; }
        IList<IElementDefinitionPreviewMessage> Messages { get; set; }
    }
}
