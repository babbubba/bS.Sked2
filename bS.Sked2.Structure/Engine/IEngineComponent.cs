using bS.Sked2.Structure.Service.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine
{
    public interface IEngineComponent
    {
        Guid? InstanceID { get; }

        /// <summary>
        /// Adds the message.
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
    }
}
