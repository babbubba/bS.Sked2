using System;
using bs.Data.Interfaces.BaseEntities;
using bS.Sked2.Structure.Models;
using bS.Sked2.Structure.Service.Messages;
using FluentNHibernate.Mapping;

namespace bS.Sked2.Model
{
    public class MessageEntry :  IPersistentEntity, IMessageEntry
    {
        public virtual Guid Id { get; set; }
        public virtual string Message { get; set; }
        public virtual MessageSeverity Severity { get; set; }
        public virtual IInstanceEntry Instance { get; set; }
    }

    class MessageEntryMap : ClassMap<MessageEntry>
    {
        public MessageEntryMap()
        {
            Table("Messages");
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Message);
            Map(x => x.Severity);
            References<InstanceEntry>(x => x.Instance);
        }
    }
}