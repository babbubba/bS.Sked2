using bS.Sked2.Structure.Models;
using bS.Sked2.Structure.Service;
using bS.Sked2.Structure.Service.Messages;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace bS.Sked2.Structure.Engine
{
    /// <summary>
    /// BAse class for all elements, tasks, jobs, links and components
    /// </summary>
    /// <seealso cref="IEngineFlowComponent" />
    public abstract class BaseEngineComponent : IEngineFlowComponent
    {
        /// <summary>
        /// The logger
        /// </summary>
        protected ILogger logger;

        /// <summary>
        /// The message service
        /// </summary>
        protected IMessageService messageService;

        /// <summary>
        /// The instance
        /// </summary>
        protected IInstanceEntry instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEngineComponent" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="messageService">The message service.</param>
        public BaseEngineComponent(ILogger logger, IMessageService messageService)
        {
            this.logger = logger;
            this.messageService = messageService;
        }

        /// <summary>
        /// Gets the instance identifier.
        /// </summary>
        /// <value>
        /// The instance identifier.
        /// </value>
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
        public bool IsRunning => instance.BeginTime != null && !instance.IsPaused && instance.EndTime == null;

        /// <summary>
        /// Gets a value indicating whether this instance has completed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has completed; otherwise, <c>false</c>.
        /// </value>
        public bool HasCompleted => instance.BeginTime != null && !instance.IsPaused && instance.EndTime != null;

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

        public abstract IEngineEntry GetEmptyEntity();

        //{
        //    throw new EngineException(logger, "This engine component has to implement the 'GetEmptyEntity' in its final class!"); ;
        //}
    }
}