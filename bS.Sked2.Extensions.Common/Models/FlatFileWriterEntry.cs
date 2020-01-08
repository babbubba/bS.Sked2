using bS.Sked2.Model;
using bS.Sked2.Structure.Engine;
using FluentNHibernate.Mapping;

namespace bS.Sked2.Extensions.Common.Models
{
    public class FlatFileWriterEntry : ElementEntry
    {
        public FlatFileWriterEntry()
        {
            Key = "FlatFileWriter";
            Name = "Flat File Writer";
            Description = "This elements write a flat file (like CSV) from a Table value.";

            InputProperties.Add(new ElementPropertyEntry("TargetFilePath", "Target file path", DataType.String, true));
            InputProperties.Add(new ElementPropertyEntry("ColumnDelimiter", "Column char delimite", DataType.Char, true));
            InputProperties.Add(new ElementPropertyEntry("Table", "Table Value to write in flat file", DataType.Table, true));
        }
    }

    class FlatFileWriterEntryMap : SubclassMap<FlatFileWriterEntry>
    {
        public FlatFileWriterEntryMap()
        {
            DiscriminatorValue("FlatFileWriter");
        }
    }
}
