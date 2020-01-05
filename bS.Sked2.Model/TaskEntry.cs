using bs.Data.Interfaces.BaseEntities;
using bS.Sked2.Structure.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;

namespace bS.Sked2.Model
{
    public class TaskEntry : IAuditableEntity, IEnableableEntity, ILogicallyDeletableEntity, IPersistentEntity, ITaskEntry
    {
        public TaskEntry()
        {
            Instances = new List<IInstanceEntry>();
            Elements = new List<IElementEntity>();
        }

        public virtual DateTime? CreationDate { get; set; }
        public virtual DateTime? DeletionDate { get; set; }
        public virtual string Description { get; set; }
        public virtual IList<IElementEntity> Elements { get; set; }
        public virtual bool FailIfAnyElementHasError { get; set; }
        public virtual bool FailIfAnyElementHasWarning { get; set; }
        public virtual Guid Id { get; set; }
        public virtual IList<IInstanceEntry> Instances { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual bool IsEnabled { get; set; }
        public virtual string Key { get; set; }
        public virtual DateTime? LastUpdateDate { get; set; }
        public virtual string Name { get; set; }
        public virtual IJobEntry ParentJob { get; set; }
    }

    internal class TaskEntryMap : ClassMap<TaskEntry>
    {
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
            References<JobEntry>(x => x.ParentJob);
            HasMany<InstanceEntry>(x => x.Instances);
            HasMany<ElementEntity>(x => x.Elements);
        }
    }
}