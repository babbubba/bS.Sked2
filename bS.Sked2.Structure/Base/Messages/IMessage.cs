namespace bS.Sked2.Structure.Service.Messages
{
    public interface IMessage
    {
        string Message { get; set; }
        MessageSeverity Severity { get; set; }
    }
}
