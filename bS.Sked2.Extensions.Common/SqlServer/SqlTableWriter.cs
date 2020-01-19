using bs.Data.Interfaces;
using bS.Sked2.Engine.Objects;
using bS.Sked2.Extensions.Common.Models;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Models;
using bS.Sked2.Structure.Repositories;
using bS.Sked2.Structure.Service;
using bS.Sked2.Structure.Service.Messages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace bS.Sked2.Extensions.Common.SqlServer
{
    public class SqlTableWriter : EngineElement
    {
        public SqlTableWriter(IUnitOfWork uow, IEngineRepository enginRepo, ILogger logger, IMessageService messageService) : base(uow, enginRepo, logger, messageService)
        {
            // Register element properties
            RegisterInputProperties("ConnectionString", "Sql Server Connection String", DataType.String, true);
            RegisterInputProperties("SqlTable", "Sql destination table", DataType.String, true);
            RegisterInputProperties("ColumnsMapping", "Columns Mapping", DataType.Collection);
            RegisterInputProperties("SourceTable", "Source TableValue", DataType.Table, true);
        }

        public override string Key => "SqlTableWriter";
        public static string KeyConst => "SqlTableWriter";
        public static string Name => "Sql Table Writer";
        public static string Description => "This elements insert a Table Data Value into a SQL Server table.";

        public override IEngineEntry GetEmptyEntity()
        {
            return new SqlTableWriterEntry();
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