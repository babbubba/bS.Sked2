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
    public class ElementEntity : IAuditableEntity, IEnableableEntity, ILogicallyDeletableEntity, IPersistentEntity, IElementEntity
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
        public virtual ITaskEntry ParentTask { get; set; }

    }


    class ElementEntityMap : ClassMap<ElementEntity>
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
        }
    }
}
