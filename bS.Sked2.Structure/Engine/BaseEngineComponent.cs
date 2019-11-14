using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bS.Sked2.Structure.Service;
using bS.Sked2.Structure.Service.Messages;
using Microsoft.Extensions.Logging;

namespace bS.Sked2.Structure.Engine
{
    public abstract class BaseEngineComponent : IEngineComponent
    {
        protected Guid? instanceId;
        protected ILogger logger;
        protected IMessageService messageService;
        protected IList<IMessage> messages;

        public BaseEngineComponent(ILogger logger, IMessageService messageService)
        {
            this.logger = logger;
            this.messageService = messageService;
            messages = new List<IMessage>();
        }
        
        public Guid? InstanceID => instanceId;


        /// <summary>
        /// Adds the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="severity">The severity.</param>
        public void AddMessage(string message, MessageSeverity severity = MessageSeverity.Info)
        {
            messages.Add(messageService.CreateMessage(message, this, severity));
        }

        /// <summary>
        /// Gets a value indicating whether this instance has errors.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has errors; otherwise, <c>false</c>.
        /// </value>
        public bool HasErrors => messages?.Any(m => m.Severity == MessageSeverity.Error || m.Severity == MessageSeverity.Fatal) ?? false;

        /// <summary>
        /// Gets a value indicating whether this instance has warnings.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has warnings; otherwise, <c>false</c>.
        /// </value>
        public bool HasWarnings => messages?.Any(m => m.Severity == MessageSeverity.Warning) ?? false;
    }
}
