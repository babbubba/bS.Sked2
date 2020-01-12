using bS.Sked2.Structure.Service.Messages;

namespace bS.Sked2.Structure.Engine.UI
{
    public interface IElementDefinitionPreviewMessage
        {
        string Message { get; set; }
        MessageSeverity Severity { get; set; }

    }
}
