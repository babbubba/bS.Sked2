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
        public virtual Guid Id { get; set; }

        public virtual List<IMessageEntry> Messages { get; set; }
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsPaused { get; set; }
    }

    class InstanceEntryMap : ClassMap<InstanceEntry>
    {
        public InstanceEntryMap()
        {
            Table("Instances");
            Id(x => x.Id).GeneratedBy.GuidComb();
            HasMany<MessageEntry>(x => x.Messages);
            Map(x => x.BeginTime);
            Map(x => x.EndTime);
            Map(x => x.IsPaused);
        }
    }
}
