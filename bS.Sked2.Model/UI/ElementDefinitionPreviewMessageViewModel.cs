using bS.Sked2.Structure.Engine.UI;
using bS.Sked2.Structure.Service.Messages;

namespace bS.Sked2.Model.UI
{
    public class ElementDefinitionPreviewMessageViewModel : IElementDefinitionPreviewMessage
    {
        public string Message { get; set; }
        public MessageSeverity Severity { get; set; }
    }
}
