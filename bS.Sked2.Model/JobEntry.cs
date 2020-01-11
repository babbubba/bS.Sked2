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
    /// <seealso cref="bS.Sked2.Structure.Models.IJobEntry" />
    public class JobEntry : IAuditableEntity, IEnableableEntity, ILogicallyDeletableEntity, IPersistentEntity, IJobEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobEntry"/> class.
        /// </summary>
        public JobEntry()
        {
            Tasks = new List<ITaskEntry>();
            Instances = new List<IInstanceEntry>();
            Triggers = new List<ITriggerEntry>();
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
        /// Gets or sets a value indicating whether [fail if any task has error].
        /// </summary>
        /// <value>
        /// <c>true</c> if [fail if any task has error]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool FailIfAnyTaskHasError { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [fail if any task has warning].
        /// </summary>
        /// <value>
        /// <c>true</c> if [fail if any task has warning]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool FailIfAnyTaskHasWarning { get; set; }

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
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public virtual string Name { get; set; }
        /// <summary>
        /// Gets or sets the tasks.
        /// </summary>
        /// <value>
        /// The tasks.
        /// </value>
        public virtual IList<ITaskEntry> Tasks { get; set; }

        /// <summary>
        /// Gets or sets the triggers.
        /// </summary>
        /// <value>
        /// The triggers.
        /// </value>
        public virtual IList<ITriggerEntry> Triggers { get; set; }

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
    /// <seealso cref="FluentNHibernate.Mapping.ClassMap{bS.Sked2.Model.JobEntry}" />
    internal class JobEntryMap : ClassMap<JobEntry>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobEntryMap"/> class.
        /// </summary>
        public JobEntryMap()
        {
            Table("Jobs");
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.IsDeleted);
            Map(x => x.DeletionDate);
            Map(x => x.IsEnabled);
            Map(x => x.CreationDate);
            Map(x => x.LastUpdateDate);

            Map(x => x.Key);
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.FailIfAnyTaskHasError);
            Map(x => x.FailIfAnyTaskHasWarning);
            HasMany<InstanceEntry>(x => x.Instances);
            HasMany<TaskEntry>(x => x.Tasks);
            HasMany<TriggerEntry>(x => x.Triggers);
            Map(x => x.Position);

        }
    }
}