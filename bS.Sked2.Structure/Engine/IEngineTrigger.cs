using bS.Sked2.Structure.Engine.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine
{
    /// <summary>
    /// Interfaccia base per tutti i trigger. Un trigger rappresenta il gestore di un evento che serve ad esempio per gestire l'avvio di un Job.
    /// </summary>
    public interface IEngineTrigger : IEngineFlowComponent
    {
        string Name { get; set; }


        /// <summary>
        /// Gets or sets the last execution time.
        /// </summary>
        /// <value>
        /// The last execution time.
        /// </value>
        DateTime LastExecutionTime { get; set; }
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
        /// Occurs when [trigger fired].
        /// </summary>
        event EventHandler<TriggerEventArgs> TriggerFired;

    }
}
