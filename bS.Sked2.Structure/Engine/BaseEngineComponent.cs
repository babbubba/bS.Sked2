using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bS.Sked2.Structure.Models;
using bS.Sked2.Structure.Service;
using bS.Sked2.Structure.Service.Messages;
using Microsoft.Extensions.Logging;

namespace bS.Sked2.Structure.Engine
{
    public abstract class BaseEngineComponent : IEngineComponent
    {
        protected ILogger logger;
        protected IMessageService messageService;
        protected IInstanceEntry instance;

        public BaseEngineComponent(ILogger logger, IMessageService messageService)
        {
            this.logger = logger;
            this.messageService = messageService;
            //instance.Messages = new List<IMessageEntry>();
        }
        
        public Guid? InstanceID => instance?.Id;


        /// <summary>
        /// Adds the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="severity">The severity.</param>
        public void AddMessage(string message, MessageSeverity severity = MessageSeverity.Info)
        {
            var messageEntity = messageService.CreateMessage(message, instance, severity);
            messageService.SaveMessage(messageEntity);
            instance.Messages.Add(messageEntity);
        }

        /// <summary>
        /// Gets a value indicating whether this instance has errors.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has errors; otherwise, <c>false</c>.
        /// </value>
        public bool HasErrors => instance.Messages?.Any(m => m.Severity == MessageSeverity.Error || m.Severity == MessageSeverity.Fatal) ?? false;

        /// <summary>
        /// Gets a value indicating whether this instance has warnings.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has warnings; otherwise, <c>false</c>.
        /// </value>
        public bool HasWarnings => instance.Messages?.Any(m => m.Severity == MessageSeverity.Warning) ?? false;

        /// <summary>
        /// Gets a value indicating whether this instance is running.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is running; otherwise, <c>false</c>.
        /// </value>
        public  bool IsRunning => instance.BeginTime != null && !instance.IsPaused && instance.EndTime == null;

        /// <summary>
        /// Gets a value indicating whether this instance has completed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has completed; otherwise, <c>false</c>.
        /// </value>
        public  bool HasCompleted => instance.BeginTime != null && !instance.IsPaused && instance.EndTime != null;


        /// <summary>
        /// Starts this instance.
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public abstract void Stop();

        /// <summary>
        /// Pauses this instance.
        /// </summary>
        public abstract void Pause();

        /// <summary>
        /// Determines whether this instance [can be executed].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance [can be executed]; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool CanBeExecuted();

        /// <summary>
        /// Loads from entity.
        /// </summary>
        /// <param name="EntityId">The entity identifier.</param>
        public abstract void LoadFromEntity(Guid EntityId);
    }
}
