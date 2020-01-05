using bs.Data.Interfaces.BaseEntities;
using bS.Sked2.Structure.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Model
{
    public class InstanceEntry: IInstanceEntry, IPersistentEntity
    {
        public InstanceEntry()
        {
            Messages = new List<IMessageEntry>();
        }
        public virtual Guid Id { get; set; }
        public virtual IList<IMessageEntry> Messages { get; set; }
        public virtual DateTime? BeginTime { get; set; }
        public virtual DateTime? EndTime { get; set; }
        public virtual bool IsPaused { get; set; }
    }

    class InstanceEntryMap : ClassMap<InstanceEntry>
    {
        public InstanceEntryMap()
        {
            Table("Instances");
            Id(x => x.Id).GeneratedBy.GuidComb();
            HasMany<MessageEntry>(x => x.Messages).Cascade.AllDeleteOrphan();
            Map(x => x.BeginTime);
            Map(x => x.EndTime);
            Map(x => x.IsPaused);
        }
    }
}
