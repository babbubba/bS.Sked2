using bS.Sked2.Extensions.Common.FlatFile.Helper;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Engine.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Extensions.Common.FlatFile
{
    public class FlatFileReader : BaseEngineElement
    {
        public FlatFileReader(ILogger logger, IEngineTask parentTask, IEngineModule parentModule) : base(logger, parentTask, parentModule)
        {
        }

        public override void Pause()
        {
            base.Pause();
        }

        public override void Start()
        {
            base.Start();
            var sourceFilePath = (string)GetDataInputValue("SourceFilePath").Get();
            var skipStartingDataRows = (int?)GetDataInputValue("SkipStartingDataRows").Get();
            var firstRowHasHeader = (bool?)GetDataInputValue("FirstRowHasHeader").Get();
            var columnDelimiter = (char?)GetDataInputValue("ColumnDelimiter").Get();
            var limitToRows = (int?)GetDataInputValue("LimitToRows").Get();
            //Start parsing file
            var parser = new GenericParserAdapter(sourceFilePath);
            parser.SkipStartingDataRows = skipStartingDataRows ?? 0;
            parser.FirstRowHasHeader = firstRowHasHeader ?? true;
            parser.ColumnDelimiter = columnDelimiter ?? ';';
            parser.MaxRows = limitToRows ?? 0;

            var table = new TableValue();
            table.Set(parser.GetDataTable());
            SetDataOutputValue("Table", table);
        }

        public override void Stop()
        {
            base.Stop();
        }
    }
}
