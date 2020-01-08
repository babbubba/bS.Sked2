using bS.Sked2.Model;
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
        }

        public virtual string TargetFilePath { get; set; }
        public virtual string ColumnDelimiter { get; set; }
    }

    class FlatFileWriterEntryMap : SubclassMap<FlatFileWriterEntry>
    {
        public FlatFileWriterEntryMap()
        {
            DiscriminatorValue("FlatFileWriter");
            Map(x => x.TargetFilePath);
            Map(x => x.ColumnDelimiter);
        }
    }
}
