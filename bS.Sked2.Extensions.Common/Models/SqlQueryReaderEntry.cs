using bS.Sked2.Model;
using FluentNHibernate.Mapping;

namespace bS.Sked2.Extensions.Common.Models
{
    public class SqlQueryReaderEntry : ElementEntry
    {
        public SqlQueryReaderEntry()
        {
            Key = "SqlQueryReader";
            Name = "SQL Query Reader";
            Description = "This elements read data form a SQL Server query and returns a Table value.";
        }

        public virtual string ConnectionString { get; set; }
        public virtual string SqlQuery { get; set; }
    }

    class SqlQueryReaderEntryMap : SubclassMap<SqlQueryReaderEntry>
    {
        public SqlQueryReaderEntryMap()
        {
            DiscriminatorValue("SqlQueryReader");
            Map(x => x.ConnectionString);
            Map(x => x.SqlQuery);
        }
    }
}
