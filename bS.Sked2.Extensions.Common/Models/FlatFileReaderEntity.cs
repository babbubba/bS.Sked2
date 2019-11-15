using bs.Data.Interfaces.BaseEntities;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Extensions.Common.Models
{
    public class FlatFileReaderEntity : BaseAuditableEntity
    {
        public virtual string SourceFilePath { get; set; }
        public virtual int? SkipStartingDataRows { get; set; }
        public virtual bool FirstRowHasHeader { get; set; }
        public virtual string ColumnDelimiter { get; set; }
        public virtual int? LimitToRows { get; set; }



        //RegisterInputProperties("SourceFilePath", "Source file path", DataType.String, true);
        //RegisterInputProperties("SkipStartingDataRows", "Starting row to skip", DataType.Int);
        //RegisterInputProperties("FirstRowHasHeader", "Using first row as header", DataType.Bool, true);
        //RegisterInputProperties("ColumnDelimiter", "Column char delimiter", DataType.Char, true);
        //RegisterInputProperties("LimitToRows", "Limit result rows", DataType.Int);
    }

    class FlatFileReaderEntityMap : SubclassMap<FlatFileReaderEntity>
    {
        public FlatFileReaderEntityMap()
        {
            // indicates that the base class is abstract
            Abstract();
            DiscriminatorValue("FlatFileReader");
            Map(x => x.SourceFilePath);
            Map(x => x.SkipStartingDataRows);
            Map(x => x.FirstRowHasHeader);
            Map(x => x.ColumnDelimiter);
            Map(x => x.LimitToRows);
        }
    }
}
