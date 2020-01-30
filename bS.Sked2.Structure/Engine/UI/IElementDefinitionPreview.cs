using System;
using System.Collections.Generic;

namespace bS.Sked2.Structure.Engine.UI
{
    public interface IElementDefinitionPreview
    {
        Guid Id { get; set; }

        IElementType Type { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        //bool IsValid { get; set; }
        int Position { get; set; }
        bool IsEnabled { get; set; }
        IList<IElementDefinitionPreviewMessage> Messages { get; set; }

        Guid PreviousId { get; set; }
        Guid NextId { get; set; }
    }
}
