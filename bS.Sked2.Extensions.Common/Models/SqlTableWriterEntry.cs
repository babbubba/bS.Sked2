using bS.Sked2.Model;
using FluentNHibernate.Mapping;

namespace bS.Sked2.Extensions.Common.Models
{
    public class SqlTableWriterEntry : ElementEntity
    {
        public SqlTableWriterEntry()
        {
            Key = "SqlTableWriter";
            Name = "SQL Table Writer";
            Description = "This elements insert TableValue to a SQL Server table.";
        }

        public virtual string ConnectionString { get; set; }
        public virtual string SqlTable { get; set; }
        public virtual string ColumnsMapping { get; set; }
    }

    class SqlTableWriterEntryMap : SubclassMap<SqlTableWriterEntry>
    {
        public SqlTableWriterEntryMap()
        {
            DiscriminatorValue("SqlTableWriter");
            Map(x => x.ConnectionString);
            Map(x => x.SqlTable);
            Map(x => x.ColumnsMapping);
        }
    }
}
