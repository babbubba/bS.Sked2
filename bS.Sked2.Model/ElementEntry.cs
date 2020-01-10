using bs.Data.Interfaces.BaseEntities;
using bS.Sked2.Structure.Base;
using bS.Sked2.Structure.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Model
{


    /// <summary>
    /// The base Element class for TPH (table per hierarchy) Elements. Every Elements implement this class andare mapped as subclass using differnet 'DiscriminatorValue' for every element.
    /// </summary>
    /// <seealso cref="bs.Data.Interfaces.BaseEntities.IAuditableEntity" />
    /// <seealso cref="bs.Data.Interfaces.BaseEntities.IEnableableEntity" />
    /// <seealso cref="bs.Data.Interfaces.BaseEntities.ILogicallyDeletableEntity" />
    /// <seealso cref="bs.Data.Interfaces.BaseEntities.IPersistentEntity" />
    public class ElementEntry : IAuditableEntity, IEnableableEntity, ILogicallyDeletableEntity, IPersistentEntity, IElementEntry
    {
        public ElementEntry()
        {
            Instances = new List<IInstanceEntry>();
            InputProperties = new List<IElementPropertyEntry>();
            OutputProperties = new List<IElementPropertyEntry>();
        }
        public virtual bool IsDeleted { get; set; }
        public virtual DateTime? DeletionDate { get; set; }
        public virtual Guid Id { get; set; }
        public virtual bool IsEnabled { get; set; }
        public virtual DateTime? CreationDate { get; set; }
        public virtual DateTime? LastUpdateDate { get; set; }
        public virtual string Description { get; set; }
        public virtual string Key { get; set; }
        public virtual IList<IInstanceEntry> Instances { get; set; }
        public virtual string Name { get; set; }
        public virtual ITaskEntry ParentTask { get; set; }
        public virtual IModuleEntry ParentModule { get; set; }

        public virtual IList<IElementPropertyEntry> InputProperties { get; set; }
        public virtual IList<IElementPropertyEntry> OutputProperties { get; set; }

        public virtual int Position { get; set; }

    }

    class ElementEntityMap : ClassMap<ElementEntry>
    {
        public ElementEntityMap()
        {
            // here we specify the name of the column
            // that will define the type of the element
            DiscriminateSubClassesOnColumn("ElementType").Not.Nullable();
            Table("Elements");
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
            References<TaskEntry>(x => x.ParentTask);
            References<ModuleEntry>(x => x.ParentModule);
            HasMany<ElementPropertyEntry>(x => x.InputProperties).KeyColumn("input").Cascade.AllDeleteOrphan();
            HasMany<ElementPropertyEntry>(x => x.OutputProperties).KeyColumn("output").Cascade.AllDeleteOrphan();
            Map(x => x.Position);


        }
    }
}
