using bs.Data.Interfaces.BaseEntities;
using bS.Sked2.Structure.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;

namespace bS.Sked2.Model
{
    

    public class TaskEntry : IAuditableEntity, IEnableableEntity, ILogicallyDeletableEntity, IPersistentEntity, ITaskEntry
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime? CreationDate { get; set; }
        public virtual DateTime? LastUpdateDate { get; set; }
        public virtual bool IsEnabled { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual DateTime? DeletionDate { get; set; }

        public virtual string Key { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual Guid? InstanceID { get; set; }
        public virtual DateTime? BeginTime { get; }
        public virtual DateTime? EndTime { get; }
        public virtual bool FailIfAnyElementHasError { get; set; }
        public virtual bool FailIfAnyElementHasWarning { get; set; }
        public virtual bool HasCompleted { get; set; }
        public virtual bool IsPaused { get; set; }
        public virtual bool IsRunning { get; set; }
        public virtual IJobEntry ParentJob { get; set; }
        public virtual List<IMessageEntry> Messages { get; set; }
        public virtual List<IElementEntity> Elements { get; set; }
    }

    class TaskEntryMap : ClassMap<TaskEntry>
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
            Map(x => x.InstanceID);
            Map(x => x.BeginTime);
            Map(x => x.EndTime);
            Map(x => x.FailIfAnyElementHasError);
            Map(x => x.FailIfAnyElementHasWarning);
            Map(x => x.HasCompleted);
            Map(x => x.IsPaused);
            Map(x => x.IsRunning);
            References<JobEntry>(x => x.ParentJob);
            HasMany<MessageEntry>(x => x.Messages);
            HasMany<ElementEntity>(x => x.Elements);
        }
    }
}
