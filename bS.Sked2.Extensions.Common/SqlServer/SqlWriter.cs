using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Service;
using bS.Sked2.Structure.Service.Messages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace bS.Sked2.Extensions.Common.SqlServer
{
    public class SqlWriter : BaseEngineElement
    {
        public SqlWriter(ILogger logger, IMessageService messageService) : base(logger, messageService)
        {
            // Config the element
            Key = "SqlWriter";
            Name = "SQL Writer";
            Description = "This elements insert TableValue to a SQL Server table.";

            // Register element properties
            RegisterInputProperties("ConnectionString", "Sql Server Connection String", DataType.String, true);
            RegisterInputProperties("SqlTable", "Sql destination table", DataType.String, true);
            RegisterInputProperties("ColumnsMapping", "Columns Mapping", DataType.Collection);
            RegisterInputProperties("SourceTable", "Source TableValue", DataType.Table, true);
        }

        public override void Start()
        {
            base.Start();

            try
            {
                // Get parameters
                var connectionString = GetDataValue(EngineDataDirection.Input, "ConnectionString").Get<string>();
                var sqlTable = GetDataValue(EngineDataDirection.Input, "SqlTable").Get<string>();
                var columnsMapping = GetDataValue(EngineDataDirection.Input, "ColumnsMapping").Get<List<IEngineData>>();
                var sourceTable = GetDataValue(EngineDataDirection.Input, "SourceTable").Get<DataTable>();

                using (SqlBulkCopy bulkCopy =
                              new SqlBulkCopy(connectionString))
                {
                    bulkCopy.DestinationTableName = sqlTable;

                    foreach (var columnMapping in columnsMapping)
                    {
                        var val = columnMapping.Get<DictionaryEntry>();

                        bulkCopy.ColumnMappings.Add((string)val.Key, (string)val.Value);
                    }
                    // Write from the source to the destination.
                    bulkCopy.WriteToServer(sourceTable);
                }
            }
            catch (Exception e)
            {
                AddMessage($"Error writing SQL Table. {e.Message}", MessageSeverity.Error);
            }
        }
    }


}
