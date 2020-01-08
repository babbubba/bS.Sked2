using bS.Sked2.Structure.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Model
{
 

    public class ElementsLinkEntry : ElementEntry, IElementsLinkEntry
    {
        public ElementsLinkEntry()
        {
            Key = "ElementsLink";
            Name = "ElementsLink";
            Description = "This element is the link between two different elements.";
        }

        public virtual IList<IElementsMappingEntry> Mappings { get; set; }
        public virtual IElementEntry Previuous { get; set; }
        public virtual IElementEntry Next { get; set; }

    }

    class ElementsLinkEntryMap : SubclassMap<ElementsLinkEntry>
    {
        public ElementsLinkEntryMap()
        {
            DiscriminatorValue("ElementsLinkEntry");
            References<ElementEntry>(x => x.Previuous);
            References<ElementEntry>(x => x.Next);
            HasMany<ElementsMappingEntry>(X => X.Mappings);
        }
    }
}
