using bS.Sked2.Model.Interfaces;
using bS.Sked2.Model.Interfaces.Elements;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Model.Elements.Base
{
    public class ExecutableElementInstance : IExecutableElementInstance, IPersisterEntity
    {
        public Guid Id { get; set; }

        public DateTime BeginDate {get; set;}
        public DateTime EndDate{get; set;}
        public bool HasErrors{get; set;}
        public bool HasWarning{get; set;}
        public bool Success{get; set;}
    }

    class ExecutableElementInstanceMap : ClassMap<ExecutableElementInstance>
    {
        public ExecutableElementInstanceMap()
        {
            Table(nameof(ExecutableElementInstance));
            Id(x => x.Id).GeneratedBy.GuidComb().Column("Id");
            Map(x => x.BeginDate);
            Map(x => x.EndDate);
            Map(x => x.HasErrors);
            Map(x => x.HasWarning);
            Map(x => x.Success);
        }
    }
}
