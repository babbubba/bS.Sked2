using bs.Data.Interfaces.BaseEntities;
using bS.Sked2.Structure.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;

namespace bS.Sked2.Model
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="bs.Data.Interfaces.BaseEntities.IAuditableEntity" />
    /// <seealso cref="bs.Data.Interfaces.BaseEntities.IEnableableEntity" />
    /// <seealso cref="bs.Data.Interfaces.BaseEntities.ILogicallyDeletableEntity" />
    /// <seealso cref="bs.Data.Interfaces.BaseEntities.IPersistentEntity" />
    /// <seealso cref="bS.Sked2.Structure.Models.ITaskEntry" />
    public class TaskEntry : IAuditableEntity, IEnableableEntity, ILogicallyDeletableEntity, IPersistentEntity, ITaskEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskEntry"/> class.
        /// </summary>
        public TaskEntry()
        {
            Instances = new List<IInstanceEntry>();
            Elements = new List<IElementEntry>();
            Modules = new List<IModuleEntry>();
        }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public virtual DateTime? CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the deletion date.
        /// </summary>
        /// <value>
        /// The deletion date.
        /// </value>
        public virtual DateTime? DeletionDate { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        public virtual IList<IElementEntry> Elements { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [fail if any element has error].
        /// </summary>
        /// <value>
        /// <c>true</c> if [fail if any element has error]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool FailIfAnyElementHasError { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [fail if any element has warning].
        /// </summary>
        /// <value>
        /// <c>true</c> if [fail if any element has warning]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool FailIfAnyElementHasWarning { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the instances.
        /// </summary>
        /// <value>
        /// The instances.
        /// </value>
        public virtual IList<IInstanceEntry> Instances { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public virtual string Key { get; set; }

        /// <summary>
        /// Gets or sets the last update date.
        /// </summary>
        /// <value>
        /// The last update date.
        /// </value>
        public virtual DateTime? LastUpdateDate { get; set; }
        public virtual IList<IModuleEntry> Modules { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent job.
        /// </summary>
        /// <value>
        /// The parent job.
        /// </value>
        public virtual IJobEntry ParentJob { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public virtual int Position { get; set; }
    }

    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="FluentNHibernate.Mapping.ClassMap{bS.Sked2.Model.TaskEntry}" />
    internal class TaskEntryMap : ClassMap<TaskEntry>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskEntryMap"/> class.
        /// </summary>
        public TaskEntryMap()
        {
            Table("Tasks");
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.IsDeleted);
            Map(x => x.DeletionDate);
            Map(x => x.IsEnabled);
            Map(x => x.CreationDate);
            Map(x => x.LastUpdateDate);

            Map(x => x.Key);
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.FailIfAnyElementHasError);
            Map(x => x.FailIfAnyElementHasWarning);
            Map(x => x.Position);
            References<JobEntry>(x => x.ParentJob);
            HasMany<InstanceEntry>(x => x.Instances);
            HasMany<ElementEntry>(x => x.Elements);
            HasMany<ModuleEntry>(x => x.Modules);
        }
    }
}