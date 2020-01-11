using bS.Sked2.Structure.Engine.Events;
using System;

namespace bS.Sked2.Structure.Engine
{
    /// <summary>
    /// Interface for all triggers type. A trigger is an event manager that is used to manage the job start.
    /// </summary>
    /// <seealso cref="bS.Sked2.Structure.Engine.IEngineFlowComponent" />
    public interface IEngineTrigger : IEngineFlowComponent
    {
        /// <summary>
        /// Occurs when [trigger fired].
        /// </summary>
        event EventHandler<TriggerEventArgs> TriggerFired;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is enable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is enable; otherwise, <c>false</c>.
        /// </value>
        bool IsEnable { get; set; }

        /// <summary>
        /// Gets or sets the jobs related to this trigger.
        /// </summary>
        /// <value>
        /// The jobs.
        /// </value>
        IEngineJob[] Jobs { get; set; }

        /// <summary>
        /// Gets or sets the last execution time.
        /// </summary>
        /// <value>
        /// The last execution time.
        /// </value>
        DateTime LastExecutionTime { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }
    }
}