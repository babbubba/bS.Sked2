using System;
using bs.Data.Interfaces.BaseEntities;
using FluentNHibernate.Mapping;

namespace bS.Sked2.Model
{
    public class TriggerEntry : IAuditableEntity, IEnableableEntity, ILogicallyDeletableEntity, IPersistentEntity
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime? CreationDate { get; set; }
        public virtual DateTime? LastUpdateDate { get; set; }
        public virtual bool IsEnabled { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual DateTime? DeletionDate { get; set; }
    }

    class TriggerEntryMap : ClassMap<TriggerEntry>
    {
        public TriggerEntryMap()
        {
            //DiscriminatorValue("FlatFileReader");
            Table("Triggers");
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.IsDeleted);
            Map(x => x.DeletionDate);
            Map(x => x.IsEnabled);
            Map(x => x.CreationDate);
            Map(x => x.LastUpdateDate);
        }
    }
}