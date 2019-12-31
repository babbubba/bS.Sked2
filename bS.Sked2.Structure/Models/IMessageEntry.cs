using bS.Sked2.Structure.Service.Messages;
using System;

namespace bS.Sked2.Structure.Models
{
    public interface IMessageEntry
    {
        Guid Id { get; set; }
        string Message { get; set; }
        MessageSeverity Severity { get; set; }
        IInstanceEntry Instance { get; set; }
    }
}