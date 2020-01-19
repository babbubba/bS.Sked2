using bs.Data.Interfaces;
using bS.Sked2.Engine.Objects;
using bS.Sked2.Extensions.Common.Models;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Engine.Data;
using bS.Sked2.Structure.Models;
using bS.Sked2.Structure.Repositories;
using bS.Sked2.Structure.Service;
using bS.Sked2.Structure.Service.Messages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace bS.Sked2.Extensions.Common.SqlServer
{
    public class SqlQueryReader : EngineElement
    {
        private readonly ISqlValidationService sqlValidationService;

        public SqlQueryReader(IUnitOfWork uow, IEngineRepository enginRepo, ILogger<Engine.Engine> logger, IMessageService messageService, ISqlValidationService sqlValidationService) : base(uow, enginRepo, logger, messageService)
        {
            this.sqlValidationService = sqlValidationService;

            // Register element properties
            RegisterInputProperties("ConnectionString", "Sql Server Connection String", DataType.String, true);
            RegisterInputProperties("SqlQuery", "SqL Query", DataType.String, true);

            RegisterOutputProperties("Table", "Rows imported from Sql Query", DataType.Table, true);
        }

        public override string Key => "SqlQueryReader";
        public static string KeyConst => "SqlQueryReader";

        public static string Name => "Sql Query Reader";
        public static string Description => "This elements read data form a SQL Server query and returns a Table value.";

        public override IEngineEntry GetEmptyEntity()
        {
            return new SqlQueryReaderEntry();
        }

        public override void Start()
        {
            base.Start();

            try
            {
                var table = new TableValue();

                // Get parameters
                var connectionString = GetDataValue(EngineDataDirection.Input, "ConnectionString").Get<string>();
                var sqlQuery = GetDataValue(EngineDataDirection.Input, "SqlQuery").Get<string>();

                var sqlErrors = new List<string>();
                if (!sqlValidationService.IsSQLQueryValid(sqlQuery, out sqlErrors))
                {
                    AddMessage($"Error/s in the sql query provided: {string.Join("; ", sqlErrors)}", MessageSeverity.Error);
                    return;
                }

                //Start connecting to SQL
                using (SqlConnection sourceConnection = new SqlConnection(connectionString))
                {
                    sourceConnection.Open();

                    // Get data from the source table as a SqlDataReader.
                    SqlCommand commandSourceData = new SqlCommand(sqlQuery, sourceConnection);
                    SqlDataReader reader = commandSourceData.ExecuteReader();
                    var dataTable = new DataTable();
                    dataTable.Load(reader);
                    table.Set(dataTable);

                    AddMessage($"Setting output value in element.", MessageSeverity.Debug);
                    SetDataValue(EngineDataDirection.Output, "Table", table);

                    AddMessage($"Importing Sql Query completed. {table.RowCount} rows read.", MessageSeverity.Debug);
                }
            }
            catch (Exception e)
            {
                AddMessage($"Error importing SQL Query. {e.Message}", MessageSeverity.Error);
            }
        }
    }
}