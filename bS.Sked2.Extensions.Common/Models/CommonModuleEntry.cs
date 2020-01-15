using bS.Sked2.Model;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Models;
using FluentNHibernate.Mapping;

namespace bS.Sked2.Extensions.Common.Models
{
    public class CommonModuleEntry : ModuleEntry
    {
        public CommonModuleEntry()
        {
            Key = Common.KeyConst;
            Name = Common.Name;
            Description = Common.Description;

            InputProperties.Add(new ElementPropertyEntry("WorkspacePath", "Workspace folder root path", DataType.String, true));
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
