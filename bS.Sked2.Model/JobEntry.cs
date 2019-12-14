using bs.Data.Interfaces.BaseEntities;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Model
{
    public class JobEntry : IAuditableEntity, IEnableableEntity, ILogicallyDeletableEntity, IPersistentEntity
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
        public virtual  DateTime? BeginTime { get; }
        public virtual DateTime? EndTime { get; }
        public virtual bool FailIfAnyTaskHasError { get; set; }
        public virtual bool FailIfAnyTaskHasWarning { get; set; }
        public virtual bool HasCompleted { get; set; }
        public virtual bool IsPaused { get; set; }
        public virtual bool IsRunning { get; set; }
      
        //public virtual IEngineTask[] Tasks { get; set; }
        //public virtual IEngineTrigger[] Triggers { get; set; }
    }

    class JobEntryMap : ClassMap<JobEntry>
    {
        public JobEntryMap()
        {
            //DiscriminatorValue("FlatFileReader");
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
            Map(x => x.InstanceID);
            Map(x => x.BeginTime);
            Map(x => x.EndTime);
            Map(x => x.FailIfAnyTaskHasError);
            Map(x => x.FailIfAnyTaskHasWarning);
            Map(x => x.HasCompleted);
            Map(x => x.IsPaused);
            Map(x => x.IsRunning);
        }
    }
}
