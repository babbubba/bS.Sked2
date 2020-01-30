using bS.Sked2.Structure.Engine.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Model.UI
{
    public class ElementDefinitionPreviewViewModel : IElementDefinitionPreview
    {
        public Guid Id { get; set; }

        public IElementType Type { get ; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public bool IsValid { get; set; }

        public int Position { get; set; }

        public bool IsEnabled { get; set; }


        public IList<IElementDefinitionPreviewMessage> Messages { get; set; }
        public Guid PreviousId { get; set; }
        public Guid NextId { get; set; }
    }
}
