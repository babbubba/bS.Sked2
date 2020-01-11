using bS.Sked2.Structure.Service.Messages;
using System;

namespace bS.Sked2.Structure.Models
{
    /// <summary>
    /// The message persister. Every <see cref="IInstanceEntry"/> owns a list of messages.
    /// </summary>
    public interface IMessageEntry
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        IInstanceEntry Instance { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        string Message { get; set; }

        /// <summary>
        /// Gets or sets the severity.
        /// </summary>
        /// <value>
        /// The severity.
        /// </value>
        MessageSeverity Severity { get; set; }
    }
}