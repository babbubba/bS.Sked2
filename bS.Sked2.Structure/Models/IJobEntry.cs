using System;
using System.Collections.Generic;

namespace bS.Sked2.Structure.Models
{
    /// <summary>
    ///
    /// </summary>
    public interface IJobEntry
    {
        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        DateTime? CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the deletion date.
        /// </summary>
        /// <value>
        /// The deletion date.
        /// </value>
        DateTime? DeletionDate { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [fail if any task has error].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [fail if any task has error]; otherwise, <c>false</c>.
        /// </value>
        bool FailIfAnyTaskHasError { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [fail if any task has warning].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [fail if any task has warning]; otherwise, <c>false</c>.
        /// </value>
        bool FailIfAnyTaskHasWarning { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the instances.
        /// </summary>
        /// <value>
        /// The instances.
        /// </value>
        IList<IInstanceEntry> Instances { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        string Key { get; set; }

        /// <summary>
        /// Gets or sets the last update date.
        /// </summary>
        /// <value>
        /// The last update date.
        /// </value>
        DateTime? LastUpdateDate { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the tasks.
        /// </summary>
        /// <value>
        /// The tasks.
        /// </value>
        IList<ITaskEntry> Tasks { get; set; }

        /// <summary>
        /// Gets or sets the triggers.
        /// </summary>
        /// <value>
        /// The triggers.
        /// </value>
        IList<ITriggerEntry> Triggers { get; set; }
    }
}