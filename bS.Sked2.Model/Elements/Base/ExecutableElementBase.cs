using bS.Sked2.DtoModel.Interfaces;
using bS.Sked2.Model.Interfaces;
using bS.Sked2.Model.Interfaces.Elements;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Model.Elements.Base
{
    public class ExecutableElementBase : IPersisterEntity, IExecutableElement
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime UpdateDate { get; set; }
        public virtual bool StopParentIfErrorOccurs { get; set; }
        public virtual bool StopParentIfWarningOccurs { get; set; }


        public virtual int X { get; set; }
        public virtual int Y { get; set; }
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }
        public virtual bool IsEnable { get; set; }


        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual IEnumerable<IInterchangeablyBaseDto> InParameters { get; set; }
        public virtual IEnumerable<IInterchangeablyBaseDto> OutParameters { get; set; }

        public virtual IEnumerable<IExecutableElementInstance> Instances { get; set; }
    }

    class ExecutableElementBaseMap : ClassMap<ExecutableElementBase>
    {
        public ExecutableElementBaseMap()
        {
            Table(nameof(Elements));
            Id(x => x.Id).GeneratedBy.GuidComb().Column("Id");
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.X);
            Map(x => x.Y);
            Map(x => x.Width);
            Map(x => x.Height);
            Map(x => x.IsEnable);
            //References<ElementTypeModel>(x => x.ElementType).Not.Nullable();
            HasMany<ExecutableElementInstance>(x => x.Instances);
            Map(x => x.StopParentIfErrorOccurs);
            Map(x => x.StopParentIfWarningOccurs);
            Map(x => x.CreateDate);
            Map(x => x.UpdateDate);
            DiscriminateSubClassesOnColumn("ElementType");
        }
    }
}
