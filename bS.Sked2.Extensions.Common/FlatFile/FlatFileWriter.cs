using bs.Data.Interfaces;
using bS.Sked2.Engine.Objects;
using bS.Sked2.Extensions.Common.Models;
using bS.Sked2.Structure.Base;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Engine.Data;
using bS.Sked2.Structure.Engine.Data.Types;
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

        public FlatFileWriter(IUnitOfWork uow, IEngineRepository enginRepo, ILogger logger, IMessageService messageService) : base(uow, enginRepo, logger, messageService)
        {
            // Register element properties
            RegisterInputProperties("TargetFilePath", "", DataType.VirtualPath, true);
            RegisterInputProperties("ColumnDelimiter", "", DataType.Char, true);
            RegisterInputProperties("Table", "", DataType.Table, true);
        }

        public override string Key => "FlatFileWriter";
        public static string KeyConst => "FlatFileWriter";
        public static string Name => "Flat File Writer";
        public static string Description => "Writes data from a Table Data Value in a flat file";

        public override IElementEntry GetEmptyEntity()
        {
            return new FlatFileWriterEntry();
        }
        /// <summary>
        /// Starts this instance. In derived class you have to execute this base before your overrided code.
        /// </summary>
        public override void Start()
        {
            base.Start();

            try
            {
                var storageService = ((Common)ParentEngineModule).StorageService;

                // Get parameters
                //var csvFilePath = GetDataValue(EngineDataDirection.Input, "TargetFilePath").Get<string>();
                var csvFilePath = GetDataValue(EngineDataDirection.Input, "TargetFilePath").Get<VirtualPath>();
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
                //File.WriteAllText(csvFilePath, sb.ToString());
                storageService.FileSave(sb.ToString(), csvFilePath);
            }
            catch (Exception e)
            {
                AddMessage($"Error writing flat file. {e.Message}", MessageSeverity.Error);
            }
        }
    }
}
