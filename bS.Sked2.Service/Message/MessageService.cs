using bS.Sked2.Structure.Base.Messages;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Service;
using bS.Sked2.Structure.Service.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Service.Message
{
    public class MessageService : IMessageService
    {
        public IMessage CreateMessage(string message, MessageSeverity severity)
        {
            var mess = new ExecutionMessage
            {
                Message = message,
                Severity = severity
            };
            return mess;
        }

        public IMessage CreateMessage(string message, IEngineComponent engineComponent, MessageSeverity severity)
        {
            var mess = new ExecutionMessage
            {
                Message = message,
                Severity = severity,
                Instance = engineComponent.InstanceID.ToString()
            };
            return mess;
        }
    }
}
