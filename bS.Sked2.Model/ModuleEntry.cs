using bs.Data.Interfaces.BaseEntities;
using bS.Sked2.Structure.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;

namespace bS.Sked2.Model
{
    public class ModuleEntry : IAuditableEntity, IEnableableEntity, ILogicallyDeletableEntity, IPersistentEntity, IModuleEntry
    {
        public virtual bool IsDeleted { get; set; }
        public virtual DateTime? DeletionDate { get; set; }
        public virtual Guid Id { get; set; }
        public virtual bool IsEnabled { get; set; }
        public virtual DateTime? CreationDate { get; set; }
        public virtual DateTime? LastUpdateDate { get; set; }
        public virtual string Description { get; set; }
        public virtual string Key { get; set; }
        public virtual List<IInstanceEntry> Instances { get; set; }
        public virtual string Name { get; set; }

    }

    class ModuleEntityMap : ClassMap<ModuleEntry>
    {
        public ModuleEntityMap()
        {
            // here we specify the name of the column
            // that will define the type of the element
            DiscriminateSubClassesOnColumn("ModuleType").Not.Nullable();
            Table("Modules");
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.IsDeleted);
            Map(x => x.DeletionDate);
            Map(x => x.IsEnabled);
            Map(x => x.CreationDate);
            Map(x => x.LastUpdateDate);
            Map(x => x.Key);
            Map(x => x.Name);
            Map(x => x.Description);
            HasMany<InstanceEntry>(x => x.Instances);
        }
    }

}
