using bs.Data.Interfaces.BaseEntities;
using bS.Sked2.Structure.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;

namespace bS.Sked2.Model
{
    public class ModuleEntry : IAuditableEntity, IEnableableEntity, ILogicallyDeletableEntity, IPersistentEntity, IModuleEntry
    {
        public ModuleEntry()
        {
            Instances = new List<IInstanceEntry>();
            InputProperties = new List<IElementPropertyEntry>();
            OutputProperties = new List<IElementPropertyEntry>();
        }
        public virtual DateTime? CreationDate { get; set; }
        public virtual DateTime? DeletionDate { get; set; }
        public virtual string Description { get; set; }
        public virtual Guid Id { get; set; }
        public virtual IList<IElementPropertyEntry> InputProperties { get; set; }
        public virtual IList<IInstanceEntry> Instances { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual bool IsEnabled { get; set; }
        public virtual string Key { get; set; }
        public virtual DateTime? LastUpdateDate { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<IElementPropertyEntry> OutputProperties { get; set; }
        public virtual ITaskEntry ParentTask { get; set; }
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
            References<TaskEntry>(x => x.ParentTask);
            HasMany<InstanceEntry>(x => x.Instances);
            HasMany<ElementPropertyEntry>(x => x.InputProperties).KeyColumn("inputModule").Cascade.AllDeleteOrphan();
            HasMany<ElementPropertyEntry>(x => x.OutputProperties).KeyColumn("outputModule").Cascade.AllDeleteOrphan();
        }
    }

}
