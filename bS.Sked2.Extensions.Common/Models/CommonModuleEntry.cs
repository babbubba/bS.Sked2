using bS.Sked2.Model;
using bS.Sked2.Structure.Engine;
using FluentNHibernate.Mapping;

namespace bS.Sked2.Extensions.Common.Models
{
    public class CommonModuleEntry : ElementEntry
    {
        public CommonModuleEntry()
        {
            Key = "Common";
            Name = "Common Module";
            Description = "This module contains the common elements.";

            InputProperties.Add(new ElementPropertyEntry("WorkspacePath", "Workspace folder path", DataType.VirtualPath, true));
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
