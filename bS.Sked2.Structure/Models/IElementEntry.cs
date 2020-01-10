using System;
using System.Collections.Generic;

namespace bS.Sked2.Structure.Models
{
    /// <summary>
    /// The base persister for Elements.
    /// </summary>
    public interface IElementEntry
    {
        Guid Id { get; set; }
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
        /// Gets or sets the input properties.
        /// </summary>
        /// <value>
        /// The input properties.
        /// </value>
        IList<IElementPropertyEntry> InputProperties { get; set; }
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
        /// Gets or sets the output properties.
        /// </summary>
        /// <value>
        /// The output properties.
        /// </value>
        IList<IElementPropertyEntry> OutputProperties { get; set; }
        /// <summary>
        /// Gets or sets the parent module.
        /// </summary>
        /// <value>
        /// The parent module.
        /// </value>
        IModuleEntry ParentModule { get; set; }
        /// <summary>
        /// Gets or sets the parent task.
        /// </summary>
        /// <value>
        /// The parent task.
        /// </value>
        ITaskEntry ParentTask { get; set; }
        int Position { get; set; }
    }
}