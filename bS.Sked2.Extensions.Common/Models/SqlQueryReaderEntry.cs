using bS.Sked2.Model;
using bS.Sked2.Structure.Engine;
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

            InputProperties.Add(new ElementPropertyEntry("ConnectionString", "Sql Server Connection String", DataType.String, true));
            InputProperties.Add(new ElementPropertyEntry("SqlQuery", "SqL Query", DataType.String, true));
            
            OutputProperties.Add(new ElementPropertyEntry("Table", "Rows imported from Sql Query", DataType.Table, true));
        }

    }



    class SqlQueryReaderEntryMap : SubclassMap<SqlQueryReaderEntry>
    {
        public SqlQueryReaderEntryMap()
        {
            DiscriminatorValue("SqlQueryReader");
        }
    }
}
