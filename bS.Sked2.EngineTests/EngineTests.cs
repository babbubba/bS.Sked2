using Microsoft.VisualStudio.TestTools.UnitTesting;
using bS.Sked2.Engine;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Moq;
using bS.Sked2.Extensions.Common.FlatFile;
using bS.Sked2.Structure.Service;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Engine.Objects;
using bS.Sked2.Structure.Engine.Data;
using System.Data;
using bS.Sked2.Service.Message;
using bS.Sked2.Extensions.Common.SqlServer;
using bS.Sked2.Extensions.Common;

namespace bS.Sked2.Engine.Tests
{
    [TestClass()]
    public class EngineTests
    {
        private ILogger<Engine> logger;
        private IMessageService messageService;
        private Common commonModule;
        private Engine engine;
        private EngineJob job;
        private EngineTask task;

        [TestInitialize]
        public void Init()
        {
            logger = Mock.Of<ILogger<Engine>>();
            engine = new Engine(logger);

            messageService = new MessageService();
            commonModule = new Common(logger, messageService);
            commonModule.Init();
            job = new EngineJob(logger, messageService);
            job.Start();
            task = new EngineTask(logger, messageService);
            task.ParentJob = job;
            task.Start();
        }
        [TestMethod()]
        public void ExecuteFlatFileReader()
        {
            var flatFileReader = new FlatFileReader(logger, messageService);
            flatFileReader.ParentModule = commonModule;
            flatFileReader.ParentTask = task;
            flatFileReader.SetDataValue(EngineDataDirection.Input, "SourceFilePath", new StringValue(@".\TestDataFiles\provincia-regione-sigla.csv"));
            flatFileReader.SetDataValue(EngineDataDirection.Input, "FirstRowHasHeader", new BoolValue(false));
            flatFileReader.SetDataValue(EngineDataDirection.Input, "ColumnDelimiter", new CharValue(','));
            engine.ExecuteElement(flatFileReader);
            var resultFlatFileReader = flatFileReader.GetDataValue(EngineDataDirection.Output, "Table").Get<DataTable>();
            Assert.IsNotNull(resultFlatFileReader);

        }

        [TestMethod()]
        public void ExecuteSqlQueryReader()
        {
            var sqlQueryReader = new SqlQueryReader(logger, messageService);
            sqlQueryReader.ParentModule = commonModule;
            sqlQueryReader.ParentTask = task;
            sqlQueryReader.SetDataValue(EngineDataDirection.Input, "ConnectionString", new StringValue(@"Data Source=(localdb)\mssqllocaldb;Persist Security Info=True;User ID=sa;Password=gabe;Database=EPS_HR;"));
            sqlQueryReader.SetDataValue(EngineDataDirection.Input, "SqlQuery", new StringValue(@"
            SELECT [Id]
                  ,[Code]
                  ,[PayrollingCode]
                  ,[Name]
                  ,[Description]
                  ,[Order]
                  ,[Type]
                  ,[IsEnabled]
              FROM [BaseActivities];"));
            engine.ExecuteElement(sqlQueryReader);
            var resultSqlQueryReader = sqlQueryReader.GetDataValue(EngineDataDirection.Output, "Table").Get<DataTable>();
            Assert.IsNotNull(resultSqlQueryReader);
        }

        [TestMethod()]
        public void ExecuteSqlWriter()
        {
            DataTable dt = GetTestTable();

            var sqlWriter = new SqlWriter(logger, messageService);
            sqlWriter.ParentModule = commonModule;
            sqlWriter.ParentTask = task;
            sqlWriter.SetDataValue(EngineDataDirection.Input, "ConnectionString", new StringValue(@"Data Source=(localdb)\mssqllocaldb;Persist Security Info=True;User ID=sa;Password=gabe;Database=EPS_HR;"));
            sqlWriter.SetDataValue(EngineDataDirection.Input, "SqlTable", new StringValue(@"TestTable"));
            var mappings = new CollectionValue();
            mappings.AddValue(new DictionaryEntryValue("Column1", "3"));
            mappings.AddValue(new DictionaryEntryValue("Column2", "1"));
            mappings.AddValue(new DictionaryEntryValue("Column3", "2"));
            sqlWriter.SetDataValue(EngineDataDirection.Input, "ColumnsMapping", mappings);
            sqlWriter.SetDataValue(EngineDataDirection.Input, "SourceTable", new TableValue(dt));

            engine.ExecuteElement(sqlWriter);

        }

        [TestMethod()]
        public void ExecuteFlatFileWriter()
        {
            DataTable dt = GetTestTable();

            var flatFileWriter = new FlatFileWriter(logger, messageService);
            flatFileWriter.ParentModule = commonModule;
            flatFileWriter.ParentTask = task;
            flatFileWriter.SetDataValue(EngineDataDirection.Input, "CsvFilePath", new StringValue(@".\TestDataFiles\CsvCreated.csv"));
            flatFileWriter.SetDataValue(EngineDataDirection.Input, "ColumnDelimiter", new CharValue(';'));
            flatFileWriter.SetDataValue(EngineDataDirection.Input, "Table", new TableValue(dt));

            engine.ExecuteElement(flatFileWriter);

        }

        private static DataTable GetTestTable()
        {
            var dt = new DataTable("table");
            dt.Columns.Add("Column1");
            dt.Columns.Add("Column2");
            dt.Columns.Add("Column3");
            for (int i = 1; i < 120; i++)
            {
                var r = dt.NewRow();
                r["Column1"] = $"Col1_Row{i}";
                r["Column2"] = $"Col2_Row{i}";
                r["Column3"] = $"Col3_Row{i}";
                dt.Rows.Add(r);
            }

            return dt;
        }
    }
}