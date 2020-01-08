using bs.Data.Interfaces.BaseEntities;
using bS.Sked2.Structure.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Model
{
    public class ElementsMappingEntry : IPersistentEntity, IElementsMappingEntry
    {
        public virtual string InputPropertyKey { get; set; }
        public virtual string OutputPropertyKey { get; set; }
        public virtual IElementsLinkEntry ParentLink { get; set; }
        public virtual Guid Id { get; set; }
}

    class ElementsMappingEntryMap : ClassMap<ElementsMappingEntry>
    {
        public ElementsMappingEntryMap()
        {
            Table("ElementsMappings");
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.InputPropertyKey);
            Map(x => x.OutputPropertyKey);
            References<ElementsLinkEntry>(x => x.ParentLink);

        }
    }
}
