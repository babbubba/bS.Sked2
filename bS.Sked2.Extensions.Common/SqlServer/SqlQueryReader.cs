using bS.Sked2.Structure.Base;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Engine.Data;
using bS.Sked2.Structure.Service;
using bS.Sked2.Structure.Service.Messages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace bS.Sked2.Extensions.Common.SqlServer
{
    public class SqlQueryReader : BaseEngineElement
    {
        public SqlQueryReader(ILogger logger, IMessageService messageService) : base(logger, messageService)
        {
            // Config the element
            Key = "SqlQueryReader";
            Name = "SQL Query Reader";
            Description = "This elements read data form a SQL Server query and returns a Table value.";

            // Register element properties
            RegisterInputProperties("ConnectionString", "Sql Server Connection String", DataType.String, true);
            RegisterInputProperties("SqlQuery", "SqL Query", DataType.String, true);

            RegisterOutputProperties("Table", "Rows imported from Sql Query", DataType.Table, true);
        }

        public override void LoadEntity(IElementEntity entity)
        {
            throw new NotImplementedException();
        }

        public override IElementEntity SaveEntity(IElementEntity entity)
        {
            throw new NotImplementedException();
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

                //Start connecting to SQL
                using (SqlConnection sourceConnection =
                  new SqlConnection(connectionString))
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
