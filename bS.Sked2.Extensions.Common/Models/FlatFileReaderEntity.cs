using bs.Data.Interfaces.BaseEntities;
using bS.Sked2.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Extensions.Common.Models
{
    public class FlatFileReaderEntity : ElementEntity
    {
        public virtual string SourceFilePath { get; set; }
        public virtual int? SkipStartingDataRows { get; set; }
        public virtual bool FirstRowHasHeader { get; set; }
        public virtual string ColumnDelimiter { get; set; }
        public virtual int? LimitToRows { get; set; }

    }

    class FlatFileReaderEntityMap : SubclassMap<FlatFileReaderEntity>
    {
        public FlatFileReaderEntityMap()
        {
            DiscriminatorValue("FlatFileReader");
            Map(x => x.SourceFilePath);
            Map(x => x.SkipStartingDataRows);
            Map(x => x.FirstRowHasHeader);
            Map(x => x.ColumnDelimiter);
            Map(x => x.LimitToRows);
        }
    }
}
