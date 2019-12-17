﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
using bS.Sked2.Service.Validation;

namespace bS.Sked2.Engine.Tests
{
    [TestClass()]
    public class EngineTests
    {
        private ILogger<Engine> logger;
        private ILogger<EngineJob> logger2;
        private ILogger<EngineTask> logger3;
        private IMessageService messageService;
        private ISqlValidationService sqlValidationService;
        private Common commonModule;
        private Engine engine;
        private EngineJob job;
        private EngineTask task;

        [TestInitialize]
        public void Init()
        {
            logger = Mock.Of<ILogger<Engine>>();
            logger2 = Mock.Of<ILogger<EngineJob>>();
            logger3 = Mock.Of<ILogger<EngineTask>>();

            messageService = new MessageService();
            sqlValidationService = new SqlValidationService(logger);
            engine = new Engine(logger, messageService);

            commonModule = new Common(logger, messageService);
            commonModule.Init();
            job = new EngineJob(logger2, messageService);
            job.Start();
            task = new EngineTask(logger3, messageService);
            task.ParentJob = job;
            task.Start();
        }
        [TestMethod()]
        public void ExecuteFlatFileReader()
        {
            FlatFileReader flatFileReader = GetFlatFileReader();
            flatFileReader.ParentTask = task;
            engine.ExecuteElement(flatFileReader);
            var resultFlatFileReader = flatFileReader.GetDataValue(EngineDataDirection.Output, "Table")?.Get<DataTable>();
            Assert.IsNotNull(resultFlatFileReader);
        }

        [TestMethod()]
        public void ExecuteSqlQueryReader()
        {
            SqlQueryReader sqlQueryReader = GetSqlQueryReader();
            sqlQueryReader.ParentTask = task;
            engine.ExecuteElement(sqlQueryReader);
            var resultSqlQueryReader = sqlQueryReader.GetDataValue(EngineDataDirection.Output, "Table")?.Get<DataTable>();
            Assert.IsNotNull(resultSqlQueryReader);
        }


        [TestMethod()]
        public void ExecuteSqlWriter()
        {
            SqlTableWriter sqlWriter = GetSqlWriter();
            sqlWriter.ParentTask = task;
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

        [TestMethod()]
        public void ExecuteTask()
        {
            //task = new EngineTask(logger, messageService);
            task.ParentJob = job;
            task.Name = "Task di prova";
            task.Key = Guid.NewGuid().ToString();
            task.Elements = new IEngineElement[] { GetFlatFileReader(), GetSqlQueryReader(), GetSqlWriter() };
            engine.ExecuteTask(task);
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

        private FlatFileReader GetFlatFileReader()
        {
            var flatFileReader = new FlatFileReader(logger, messageService);
            flatFileReader.ParentModule = commonModule;
            flatFileReader.SetDataValue(EngineDataDirection.Input, "SourceFilePath", new StringValue(@".\TestDataFiles\provincia-regione-sigla.csv"));
            flatFileReader.SetDataValue(EngineDataDirection.Input, "FirstRowHasHeader", new BoolValue(false));
            flatFileReader.SetDataValue(EngineDataDirection.Input, "ColumnDelimiter", new CharValue(','));
            return flatFileReader;
        }
        private SqlQueryReader GetSqlQueryReader()
        {
            var sqlQueryReader = new SqlQueryReader(logger, messageService, sqlValidationService);
            sqlQueryReader.ParentModule = commonModule;
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
            return sqlQueryReader;
        }
        private SqlTableWriter GetSqlWriter()
        {
            DataTable dt = GetTestTable();
            var sqlWriter = new SqlTableWriter(logger, messageService);
            sqlWriter.ParentModule = commonModule;
            sqlWriter.SetDataValue(EngineDataDirection.Input, "ConnectionString", new StringValue(@"Data Source=(localdb)\mssqllocaldb;Persist Security Info=True;User ID=sa;Password=gabe;Database=EPS_HR;"));
            sqlWriter.SetDataValue(EngineDataDirection.Input, "SqlTable", new StringValue(@"TestTable"));
            var mappings = new CollectionValue();
            mappings.AddValue(new DictionaryEntryValue("Column1", "3"));
            mappings.AddValue(new DictionaryEntryValue("Column2", "1"));
            mappings.AddValue(new DictionaryEntryValue("Column3", "2"));
            sqlWriter.SetDataValue(EngineDataDirection.Input, "ColumnsMapping", mappings);
            sqlWriter.SetDataValue(EngineDataDirection.Input, "SourceTable", new TableValue(dt));
            return sqlWriter;
        }

    }
}