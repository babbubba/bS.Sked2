﻿using bS.Sked2.Extensions.Common.FlatFile.Helper;
using bS.Sked2.Shared;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Engine.Data;
using bS.Sked2.Structure.Service;
using bS.Sked2.Structure.Service.Messages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bS.Sked2.Extensions.Common.FlatFile
{
    public class FlatFileReader : BaseEngineElement
    {
        public FlatFileReader(ILogger logger, IMessageService messageService) : base(logger, messageService)
        {
            // Config the element
            Key = "FlatFileReader";
            Name = "Flat File Reader";
            Description = "This elements read form a flat file (like CSV) and returns a Table value.";

            // Register element properties
            RegisterInputProperties("SourceFilePath", "Source file path", DataType.String, true);
            RegisterInputProperties("SkipStartingDataRows", "Starting row to skip", DataType.Int);
            RegisterInputProperties("FirstRowHasHeader", "Using first row as header", DataType.Bool, true);
            RegisterInputProperties("ColumnDelimiter", "Column char delimiter", DataType.Char, true);
            RegisterInputProperties("LimitToRows", "Limit result rows", DataType.Int);

            RegisterOutputProperties("Table", "Rows imported from flat file", DataType.Table, true);
        }

        /// <summary>
        /// Starts this instance. In derived class you have to execute this base before your overrided code.
        /// </summary>
        public override void Start()
        {
            base.Start();
            var sourceFilePath = GetDataValue(EngineDataDirection.Input, "SourceFilePath").Get<string>();
            var skipStartingDataRows = GetDataValue(EngineDataDirection.Input, "SkipStartingDataRows").Get<int?>();
            var firstRowHasHeader = GetDataValue(EngineDataDirection.Input, "FirstRowHasHeader").Get<bool>();
            var columnDelimiter = GetDataValue(EngineDataDirection.Input, "ColumnDelimiter").Get<char>();
            var limitToRows = GetDataValue(EngineDataDirection.Input, "LimitToRows").Get<int?>();

            //Start parsing file
            AddMessage("Configuring parser to read file", MessageSeverity.Debug);
            var parser = new GenericParserAdapter(sourceFilePath);
            parser.SkipStartingDataRows = skipStartingDataRows ?? 0;
            parser.FirstRowHasHeader = firstRowHasHeader;
            parser.ColumnDelimiter = columnDelimiter;
            parser.MaxRows = limitToRows ?? 0;
            var table = new TableValue();

            AddMessage($"Begin parsing file: '{sourceFilePath}'.", MessageSeverity.Debug);
            table.Set(parser.GetDataTable());

            AddMessage($"Setting output value in element.", MessageSeverity.Debug);
            SetDataValue( EngineDataDirection.Output,"Table", table);

            AddMessage($"Releasing unnecessary resources.", MessageSeverity.Debug);
            parser.Dispose();

            AddMessage($"Parsing file completed. {table.RowCount} read.", MessageSeverity.Debug);
        }

    }
}