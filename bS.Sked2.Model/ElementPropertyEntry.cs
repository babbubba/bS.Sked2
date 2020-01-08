using bs.Data.Interfaces.BaseEntities;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Model
{
    public class ElementPropertyEntry : IElementPropertyEntry, IPersistentEntity
    {
        public ElementPropertyEntry()
        {

        }

        public ElementPropertyEntry(string key, string description, DataType dataType, bool mandatory = false, string value = "NULL")
        {
            Key = key;
            Description = description;
            DataType = dataType;
            Mandatory = mandatory;
            Value = value;
        }
        public virtual Guid Id { get; set; }

        public virtual string Key { get; set; }

        public virtual string Description { get; set; }

        public virtual DataType DataType { get; set; }

        public virtual bool Mandatory { get; set; }

        public virtual string Value { get; set; }
    }

    class ElementPropertyEntryMap : ClassMap<ElementPropertyEntry>
    {
        public ElementPropertyEntryMap()
        {
            Table("ElementProperties");
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Key);
            Map(x => x.Description);
            Map(x => x.DataType);
            Map(x => x.Mandatory);
            Map(x => x.Value);
        }
    }
}
