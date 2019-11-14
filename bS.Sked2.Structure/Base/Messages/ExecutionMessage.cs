using bS.Sked2.Structure.Service.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Base.Messages
{
    public class ExecutionMessage : IMessage
    {
        public string Message { get; set; }
        public MessageSeverity Severity { get; set; }
        public string Instance { get; set; }
    }
}
