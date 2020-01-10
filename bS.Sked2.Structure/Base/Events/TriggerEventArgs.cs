using System;

namespace bS.Sked2.Structure.Engine.Events
{
    /// <summary>
    /// The trigger event argument
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class TriggerEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TriggerEventArgs"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="lastExecutionTime">The last execution time.</param>
        /// <param name="instanceID">The instance identifier.</param>
        public TriggerEventArgs(string name, DateTime lastExecutionTime, Guid? instanceID)
        {
            Name = name;
            LastExecutionTime = lastExecutionTime;
            InstanceID = instanceID;
            FiredTime = DateTime.Now;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TriggerEventArgs"/> class.
        /// </summary>
        /// <param name="currentTrigger">The current trigger.</param>
        public TriggerEventArgs(IEngineTrigger currentTrigger)
        {
            Name = currentTrigger.Name;
            LastExecutionTime = currentTrigger.LastExecutionTime;
            InstanceID = currentTrigger.InstanceID;
            FiredTime = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the fired time.
        /// </summary>
        /// <value>
        /// The fired time.
        /// </value>
        public DateTime FiredTime { get; set; }

        /// <summary>
        /// Gets or sets the instance identifier.
        /// </summary>
        /// <value>
        /// The instance identifier.
        /// </value>
        public Guid? InstanceID { get; set; }

        /// <summary>
        /// Gets or sets the last execution time.
        /// </summary>
        /// <value>
        /// The last execution time.
        /// </value>
        public DateTime LastExecutionTime { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
    }
}