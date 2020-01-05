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
using bS.Sked2.Service.Validation;
using bs.Data.Interfaces;
using bS.Sked2.Structure.Repositories;
using bS.Sked2.Model.Repositories;
using bs.Data;
using bS.Sked2.Extensions.Common.Models;
using bS.Sked2.Model;

namespace bS.Sked2.Engine.Tests
{
    [TestClass()]
    public class EngineTests
    {
        private IUnitOfWork uow;
        private IUnitOfWork messageUow;
        private IEngineRepository engineRepository;
        private MessageRepository messageRepository;
        private ILogger<Engine> engineLogger;
        private ILogger<EngineElement> engineElementLogger;
        private IMessageService messageService;
        private ISqlValidationService sqlValidationService;
        private Common commonModule;
        private Engine engine;

        private static IUnitOfWork CreateUnitOfWork_Sqlite()
        {
            var dbContext = new DbContext
            {
                ConnectionString = "Data Source=.\\bs.Data.Test.db;Version=3;BinaryGuid=False;",
                DatabaseEngineType = "sqlite",
                Create = true,
                Update = true,
                LookForEntitiesDllInCurrentDirectoryToo = true,
                EntitiesFileNameScannerPatterns = new string[] { "bS.Sked2.Extensions.*.dll", "bS.Sked2.Model.dll" }
            };
            var uOW = new UnitOfWork(dbContext);
            return uOW;
        }

        [TestInitialize]
        public void Init()
        {
            engineLogger = Mock.Of<ILogger<Engine>>();
            engineElementLogger = Mock.Of<ILogger<EngineElement>>();
            
            //logger2 = Mock.Of<ILogger<EngineJob>>();
            //logger3 = Mock.Of<ILogger<EngineTask>>();

            sqlValidationService = new SqlValidationService(engineLogger);
            engine = new Engine(engineLogger);

            uow = CreateUnitOfWork_Sqlite();
            //messageUow = CreateUnitOfWork_Sqlite();

            engineRepository = new EngineRepository(uow);
            messageRepository = new MessageRepository(uow);

            messageService = new MessageService(uow, messageRepository);


            //commonModule = new Common(engineLogger, messageService, uow, engineRepository );
            //commonModule.Init();
            //job = new EngineJob(logger2, messageService);
            //job.Start();
            //task = new EngineTask(logger3, messageService);
            //task.ParentJob = job;
            //task.Start();
        }
        [TestMethod()]
        public void ExecuteFlatFileReader()
        {
            #region Create Entity
            uow.BeginTransaction();

            var taskEntry = new TaskEntry
            {
                 FailIfAnyElementHasError = true,
                  FailIfAnyElementHasWarning = false,
                   IsEnabled = true,
                    Key = "TaskTest",
                     Name = "Task di prova"
            };

            engineRepository.CreateTask(taskEntry);


            var elementFlatileReaderEntry = new FlatFileReaderEntry
            {
                ColumnDelimiter = "<char>59</char>",
                FirstRowHasHeader = "<boolean>true</boolean>",
                SourceFilePath = @"<string>c:\temp</string>",
                LimitToRows = "<int>0</int>",
                SkipStartingDataRows = "<int>0</int>",
                ParentTask = taskEntry
            };
            engineRepository.CreateElement(elementFlatileReaderEntry);

            taskEntry.Elements.Add(elementFlatileReaderEntry);

            uow.Commit();
            #endregion

            FlatFileReader flatFileReader = GetFlatFileReader();
            flatFileReader.LoadFromEntity(elementFlatileReaderEntry.Id);
            //2e9e40c3-54cd-486e-a250-ab39015ed5ca
            engine.ExecuteElement(flatFileReader);
            var resultFlatFileReader = flatFileReader.GetDataValue(EngineDataDirection.Output, "Table")?.Get<DataTable>();
            Assert.IsNotNull(resultFlatFileReader);
        }

        //[TestMethod()]
        //public void ExecuteSqlQueryReader()
        //{
        //    SqlQueryReader sqlQueryReader = GetSqlQueryReader();
        //    sqlQueryReader.ParentTask = task;
        //    engine.ExecuteElement(sqlQueryReader);
        //    var resultSqlQueryReader = sqlQueryReader.GetDataValue(EngineDataDirection.Output, "Table")?.Get<DataTable>();
        //    Assert.IsNotNull(resultSqlQueryReader);
        //}


        //[TestMethod()]
        //public void ExecuteSqlWriter()
        //{
        //    SqlTableWriter sqlWriter = GetSqlWriter();
        //    sqlWriter.ParentTask = task;
        //    engine.ExecuteElement(sqlWriter);
        //}


        //[TestMethod()]
        //public void ExecuteFlatFileWriter()
        //{
        //    DataTable dt = GetTestTable();
        //    var flatFileWriter = new FlatFileWriter(engineLogger, messageService);
        //    flatFileWriter.ParentModule = commonModule;
        //    flatFileWriter.ParentTask = task;
        //    flatFileWriter.SetDataValue(EngineDataDirection.Input, "CsvFilePath", new StringValue(@".\TestDataFiles\CsvCreated.csv"));
        //    flatFileWriter.SetDataValue(EngineDataDirection.Input, "ColumnDelimiter", new CharValue(';'));
        //    flatFileWriter.SetDataValue(EngineDataDirection.Input, "Table", new TableValue(dt));

        //    engine.ExecuteElement(flatFileWriter);
        //}

        //[TestMethod()]
        //public void ExecuteTask()
        //{
        //    //task = new EngineTask(logger, messageService);
        //    task.ParentJob = job;
        //    task.Name = "Task di prova";
        //    task.Key = Guid.NewGuid().ToString();
        //    task.Elements = new IEngineElement[] { GetFlatFileReader(), GetSqlQueryReader(), GetSqlWriter() };
        //    engine.ExecuteTask(task);
        //}

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
            var flatFileReader = new FlatFileReader(uow, engineRepository, engineElementLogger, messageService);
            flatFileReader.SetDataValue(EngineDataDirection.Input, "SourceFilePath", new StringValue(@".\TestDataFiles\provincia-regione-sigla.csv"));
            flatFileReader.SetDataValue(EngineDataDirection.Input, "FirstRowHasHeader", new BoolValue(false));
            flatFileReader.SetDataValue(EngineDataDirection.Input, "ColumnDelimiter", new CharValue(','));
            return flatFileReader;
        }
        //private SqlQueryReader GetSqlQueryReader()
        //{
        //    var sqlQueryReader = new SqlQueryReader(uow, engineRepository, engineElementLogger, messageService);
        //    sqlQueryReader.ParentModule = commonModule;
        //    sqlQueryReader.SetDataValue(EngineDataDirection.Input, "ConnectionString", new StringValue(@"Data Source=(localdb)\mssqllocaldb;Persist Security Info=True;User ID=sa;Password=gabe;Database=EPS_HR;"));
        //    sqlQueryReader.SetDataValue(EngineDataDirection.Input, "SqlQuery", new StringValue(@"
        //    SELECT [Id]
        //          ,[Code]
        //          ,[PayrollingCode]
        //          ,[Name]
        //          ,[Description]
        //          ,[Order]
        //          ,[Type]
        //          ,[IsEnabled]
        //      FROM [BaseActivities];"));
        //    return sqlQueryReader;
        //}
        //private SqlTableWriter GetSqlWriter()
        //{
        //    DataTable dt = GetTestTable();
        //    var sqlWriter = new SqlTableWriter(engineLogger, messageService);
        //    sqlWriter.ParentModule = commonModule;
        //    sqlWriter.SetDataValue(EngineDataDirection.Input, "ConnectionString", new StringValue(@"Data Source=(localdb)\mssqllocaldb;Persist Security Info=True;User ID=sa;Password=gabe;Database=EPS_HR;"));
        //    sqlWriter.SetDataValue(EngineDataDirection.Input, "SqlTable", new StringValue(@"TestTable"));
        //    var mappings = new CollectionValue();
        //    mappings.AddValue(new DictionaryEntryValue("Column1", "3"));
        //    mappings.AddValue(new DictionaryEntryValue("Column2", "1"));
        //    mappings.AddValue(new DictionaryEntryValue("Column3", "2"));
        //    sqlWriter.SetDataValue(EngineDataDirection.Input, "ColumnsMapping", mappings);
        //    sqlWriter.SetDataValue(EngineDataDirection.Input, "SourceTable", new TableValue(dt));
        //    return sqlWriter;
        //}

    }
}