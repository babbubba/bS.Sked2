using bS.Sked2.Extensions.Common.SqlServer;
using bS.Sked2.Model;
using bS.Sked2.Structure.Engine;
using FluentNHibernate.Mapping;

namespace bS.Sked2.Extensions.Common.Models
{
    public class SqlTableWriterEntry : ElementEntry
    {
        public SqlTableWriterEntry()
        {
            //Key = "SqlTableWriter";
            //Name = "SQL Table Writer";
            //Description = "This elements insert TableValue to a SQL Server table.";

            Key = SqlTableWriter.KeyConst;
            Name = SqlTableWriter.Name;
            Description = SqlTableWriter.Description;

            InputProperties.Add(new ElementPropertyEntry("ConnectionString", "Sql Server Connection String", DataType.String, true));
            InputProperties.Add(new ElementPropertyEntry("SqlTable", "Sql destination table", DataType.String, true));
            InputProperties.Add(new ElementPropertyEntry("ColumnsMapping", "Columns Mapping", DataType.Collection));
            InputProperties.Add(new ElementPropertyEntry("SourceTable", "Source Table Value", DataType.Table, true));
        }
    }

    class SqlTableWriterEntryMap : SubclassMap<SqlTableWriterEntry>
    {
        public SqlTableWriterEntryMap()
        {
            DiscriminatorValue("SqlTableWriter");
        }
    }
}
