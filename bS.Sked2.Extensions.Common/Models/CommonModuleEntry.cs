using bS.Sked2.Model;
using FluentNHibernate.Mapping;

namespace bS.Sked2.Extensions.Common.Models
{
    public class CommonModuleEntry : ElementEntity
    {
        public CommonModuleEntry()
        {
            Key = "Common";
            Name = "Common Module";
            Description = "This module contains the common elements.";
        }
    }

    class CommonModuleEntityMap : SubclassMap<CommonModuleEntry>
    {
        public CommonModuleEntityMap()
        {
            DiscriminatorValue("Common");
        }
    }
}
