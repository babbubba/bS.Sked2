using bs.Data.Interfaces;
using bS.Sked2.Engine.Objects;
using bS.Sked2.Extensions.Common.Models;
using bS.Sked2.Structure.Base;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Engine.Data;
using bS.Sked2.Structure.Repositories;
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
    public class SqlTableWriter : EngineElement
    {
        //public SqlTableWriter(ILogger logger, IMessageService messageService) : base(logger, messageService)
        //{
        //    // Config the element
        //    Key = "SqlWriter";
        //    Name = "SQL Writer";
        //    Description = "This elements insert TableValue to a SQL Server table.";

        //    // Register element properties
        //    RegisterInputProperties("ConnectionString", "Sql Server Connection String", DataType.String, true);
        //    RegisterInputProperties("SqlTable", "Sql destination table", DataType.String, true);
        //    RegisterInputProperties("ColumnsMapping", "Columns Mapping", DataType.Collection);
        //    RegisterInputProperties("SourceTable", "Source TableValue", DataType.Table, true);
        //}

        public SqlTableWriter(IUnitOfWork uow, IEngineRepository enginRepo, ILogger<EngineElement> logger, IMessageService messageService) : base(uow, enginRepo, logger, messageService)
        {
            // Register element properties
            RegisterInputProperties("ConnectionString", "Sql Server Connection String", DataType.String, true);
            RegisterInputProperties("SqlTable", "Sql destination table", DataType.String, true);
            RegisterInputProperties("ColumnsMapping", "Columns Mapping", DataType.Collection);
            RegisterInputProperties("SourceTable", "Source TableValue", DataType.Table, true);
        }

        public override string Key => "SqlTableWriter";


        public override void LoadFromEntity(Guid EntityId)
        {
            elementEntry = engineRepository.GetElementById(EntityId);
            // set data properties from entity!!!
            //SqlTableWriterEntry
            var entity = (SqlTableWriterEntry)elementEntry;

            // set data properties from entity
            SetDataValue(EngineDataDirection.Input, "ConnectionString", new StringValue(entity.ConnectionString));
            SetDataValue(EngineDataDirection.Input, "SqlTable", new StringValue(entity.SqlTable));
            var columnsMapping = new List<IEngineData>();
            foreach (var row in entity.ColumnsMapping.Split("|!*&$%!|"))
            {
                var cells = row.Split("|!@#§!|");
                columnsMapping.Add( new DictionaryEntryValue(cells[0], cells[1]));
            }
            SetDataValue(EngineDataDirection.Input, "ColumnsMapping", new CollectionValue(columnsMapping));
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
