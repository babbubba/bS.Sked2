using bs.Data.Interfaces.BaseEntities;
using bS.Sked2.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Extensions.Common.Models
{
    public class FlatFileReaderEntry : ElementEntity
    {
        public FlatFileReaderEntry()
        {
            Key = "FlatFileReader";
            Name = "Flat File Reader";
            Description = "This elements read form a flat file (like CSV) and returns a Table value.";
        }

        public virtual string SourceFilePath { get; set; }
        public virtual string SkipStartingDataRows { get; set; }
        public virtual string FirstRowHasHeader { get; set; }
        public virtual string ColumnDelimiter { get; set; }
        public virtual string LimitToRows { get; set; }

    }

    class FlatFileReaderEntityMap : SubclassMap<FlatFileReaderEntry>
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
