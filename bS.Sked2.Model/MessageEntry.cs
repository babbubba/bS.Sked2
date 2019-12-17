using System;
using bs.Data.Interfaces.BaseEntities;
using bS.Sked2.Structure.Service.Messages;
using FluentNHibernate.Mapping;

namespace bS.Sked2.Model
{
    public class MessageEntry :  IPersistentEntity, IMessage
    {
        public virtual Guid Id { get; set; }
        public string Message { get; set; }
        public MessageSeverity Severity { get; set; }
        public string Instance { get; set; }
    }

    class MessageEntryMap : ClassMap<MessageEntry>
    {
        public MessageEntryMap()
        {
            Table("Messages");
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Message);
            Map(x => x.Severity);
            Map(x => x.Instance);
        }
    }
}