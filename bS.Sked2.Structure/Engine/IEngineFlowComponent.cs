using bS.Sked2.Structure.Service.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine
{
    /// <summary>Base interface for all elements, tasks, jobs, links and components.</summary>
    public interface IEngineFlowComponent
    {
        Guid? InstanceID { get; }

        /// <summary>
        /// Adds an execution message in the related istance.
        /// </summary>
        /// <param name="Message">The message.</param>
        /// <param name="severity">The severity (Optional: default is Info).</param>
        void AddMessage(string Message, MessageSeverity severity = MessageSeverity.Info);

        /// <summary>
        /// Gets a value indicating whether this instance has errors.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has errors; otherwise, <c>false</c>.
        /// </value>
        bool HasErrors { get; }
       
        /// <summary>
        /// Gets a value indicating whether this instance has warnings.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has warnings; otherwise, <c>false</c>.
        /// </value>
        bool HasWarnings { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is running.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is running; otherwise, <c>false</c>.
        /// </value>
        bool IsRunning { get; }

        /// <summary>
        /// Gets a value indicating whether this instance has completed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has completed; otherwise, <c>false</c>.
        /// </value>
        bool HasCompleted { get; }
      
        /// <summary>
        /// Starts this instance.
        /// </summary>
        void Start();
     
        /// <summary>
        /// Stops this instance.
        /// </summary>
        void Stop();
      
        /// <summary>
        /// Pauses this instance.
        /// </summary>
        void Pause();

        /// <summary>
        /// Determines whether this instance [can be executed].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance [can be executed]; otherwise, <c>false</c>.
        /// </returns>
        bool CanBeExecuted();

        /// <summary>
        /// Loads from entity.
        /// </summary>
        /// <param name="EntityId">The entity identifier.</param>
        void LoadFromEntity(Guid EntityId);
    }
}
