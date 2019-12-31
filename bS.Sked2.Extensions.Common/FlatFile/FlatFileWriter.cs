using bs.Data.Interfaces;
using bS.Sked2.Engine.Objects;
using bS.Sked2.Structure.Base;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Models;
using bS.Sked2.Structure.Repositories;
using bS.Sked2.Structure.Service;
using bS.Sked2.Structure.Service.Messages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace bS.Sked2.Extensions.Common.FlatFile
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="bS.Sked2.Structure.Engine.BaseEngineElement" />
    public class FlatFileWriter : EngineElement
    {
        //public FlatFileWriter(ILogger logger, IMessageService messageService) : base(logger, messageService)
        //{
        //    // Config the element
        //    Key = "FlatFileWriter";
        //    Name = "Flat File Writer";
        //    Description = "This elements write a flat file (like CSV) from a Table value.";

        //    // Register element properties
        //    RegisterInputProperties("CsvFilePath", "CSV file path", DataType.String, true);
        //    RegisterInputProperties("ColumnDelimiter", "Column char delimiter", DataType.Char, true);
        //    RegisterInputProperties("Table", "TableValue to write in flat file", DataType.Table, true);
        //}

        public FlatFileWriter(IUnitOfWork uow, IEngineRepository enginRepo, ILogger<EngineElement> logger, IMessageService messageService) : base(uow, enginRepo, logger, messageService)
        {
            // Register element properties
            RegisterInputProperties("CsvFilePath", "CSV file path", DataType.String, true);
            RegisterInputProperties("ColumnDelimiter", "Column char delimiter", DataType.Char, true);
            RegisterInputProperties("Table", "TableValue to write in flat file", DataType.Table, true);
        }

        public override void LoadFromEntity(Guid EntityId)
        {
            elementEntry = engineRepository.GetElementById(EntityId);
            // set data properties from entity!!!
        }

        /// <summary>
        /// Starts this instance. In derived class you have to execute this base before your overrided code.
        /// </summary>
        public override void Start()
        {
            base.Start();

            try
            {
                // Get parameters
                var csvFilePath = GetDataValue(EngineDataDirection.Input, "CsvFilePath").Get<string>();
                var columnDelimiter = GetDataValue(EngineDataDirection.Input, "ColumnDelimiter").Get<char>();
                var dt = GetDataValue(EngineDataDirection.Input, "Table").Get<DataTable>();

                AddMessage("Creating CSV in memory.", MessageSeverity.Debug);
                StringBuilder sb = new StringBuilder();

                // Create columns header
                IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
                sb.AppendLine(string.Join(columnDelimiter, columnNames));

                // Create rows
                foreach (DataRow row in dt.Rows)
                {
                    IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                    sb.AppendLine(string.Join(columnDelimiter, fields));
                }

                AddMessage("Writing CSV file.", MessageSeverity.Debug);
                File.WriteAllText(csvFilePath, sb.ToString());
            }
            catch (Exception e)
            {
                AddMessage($"Error writing flat file. {e.Message}", MessageSeverity.Error);
            }
        }
    }
}
