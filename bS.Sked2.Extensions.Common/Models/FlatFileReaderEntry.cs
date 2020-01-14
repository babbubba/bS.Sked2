using bs.Data.Interfaces.BaseEntities;
using bS.Sked2.Extensions.Common.FlatFile;
using bS.Sked2.Model;
using bS.Sked2.Structure.Engine;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Extensions.Common.Models
{
    public class FlatFileReaderEntry : ElementEntry
    {
        public FlatFileReaderEntry()
        {
            Key = FlatFileReader.KeyConst;
            Name = FlatFileReader.Name;
            Description = FlatFileReader.Description;

            InputProperties.Add(new ElementPropertyEntry("SourceFilePath", "Source file path", DataType.VirtualPath, true));
            InputProperties.Add(new ElementPropertyEntry("SkipStartingDataRows", "Starting row to skip", DataType.Int));
            InputProperties.Add(new ElementPropertyEntry("FirstRowHasHeader", "Use first row as header", DataType.Bool, true));
            InputProperties.Add(new ElementPropertyEntry("ColumnDelimiter", "Column char delimiter", DataType.Char, true));
            InputProperties.Add(new ElementPropertyEntry("LimitToRows", "Limit result to rows number", DataType.Int));

            OutputProperties.Add(new ElementPropertyEntry("Table", "Rows imported from flat file", DataType.Table, true));
        }
    }

    class FlatFileReaderEntityMap : SubclassMap<FlatFileReaderEntry>
    {
        public FlatFileReaderEntityMap()
        {
            DiscriminatorValue("FlatFileReader");
        }
    }
}
