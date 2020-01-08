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
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace bS.Sked2.Engine.Tests
{
    [TestClass()]
    public class EngineTests
    {
        private static IServiceProvider serviceProvider;

        [TestInitialize]
        public void Init()
        {
            var services = new ServiceCollection();

            var dbContext = new DbContext
            {
                ConnectionString = "Data Source=.\\bs.Data.Test.db;Version=3;BinaryGuid=False;",
                DatabaseEngineType = "sqlite",
                Create = true,
                Update = true,
                LookForEntitiesDllInCurrentDirectoryToo = true,
                EntitiesFileNameScannerPatterns = new string[] { "bS.Sked2.Extensions.*.dll", "bS.Sked2.Model.dll" }
            };

            services.AddSingleton(Mock.Of<ILogger<Engine>>());
            services.AddSingleton(Mock.Of<ILogger<EngineElement>>());
            services.AddSingleton(Mock.Of<ILogger<EngineTask>>());

            services.AddSingleton<IDbContext>(dbContext);

            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IEngineRepository, EngineRepository>();
            services.AddSingleton<IMessageRepository, MessageRepository>();


            services.AddSingleton<IMessageService, MessageService>();
            services.AddSingleton<ISqlValidationService,SqlValidationService>();
            services.AddSingleton<IEngine,Engine>();
            services.AddSingleton<IEngineTask, EngineTask>();
            //IEngineTask

            services.AddTransient<EngineLink>();
            services.AddTransient<FlatFileReader>();
            services.AddTransient<FlatFileWriter>();
            services.AddTransient<SqlQueryReader>();
            services.AddTransient<SqlTableWriter>();

            serviceProvider = services.BuildServiceProvider();

        }

        [TestMethod()]
        public void ExecuteFlatFileReader()
        {

            var uow = serviceProvider.GetService<IUnitOfWork>();
            var engineRepository = serviceProvider.GetService<IEngineRepository>();
            var engine = serviceProvider.GetService<IEngine>();

            //Create entities to test
            uow.BeginTransaction();

            TaskEntry taskEntry = GetTaskEntry();
            engineRepository.CreateTask(taskEntry);

            FlatFileReaderEntry elementFlatileReaderEntry = GetFlatFileReaderEntry();
            elementFlatileReaderEntry.ParentTask = taskEntry;
            engineRepository.CreateElement(elementFlatileReaderEntry);

            taskEntry.Elements.Add(elementFlatileReaderEntry);

            uow.Commit();

            // Create Engine element for execution
            var flatFileReader = serviceProvider.GetService<FlatFileReader>();

            // Load the entity in the engine  element
            flatFileReader.LoadFromEntity(elementFlatileReaderEntry.Id);

            // Execute
            engine.ExecuteElement(flatFileReader);

            // Check the result
            var resultFlatFileReader = flatFileReader.GetDataValue(EngineDataDirection.Output, "Table")?.Get<DataTable>();
            Assert.IsNotNull(resultFlatFileReader);
        }

        [TestMethod()]
        public void TestLinkElement()
        {
            var uow = serviceProvider.GetService<IUnitOfWork>();
            var engineRepository = serviceProvider.GetService<IEngineRepository>();
            var engine = serviceProvider.GetService<IEngine>();


            //Create entities to test
            uow.BeginTransaction();

            TaskEntry taskEntry = GetTaskEntry();
            engineRepository.CreateTask(taskEntry);

            //first
            var elementFlatileReaderEntry = GetFlatFileReaderEntry();
            elementFlatileReaderEntry.ParentTask = taskEntry;
            engineRepository.CreateElement(elementFlatileReaderEntry);

            //second
            var elementFlatFileWriterEntry = GetFlatFileWriterEntry();
            elementFlatFileWriterEntry.ParentTask = taskEntry;
            engineRepository.CreateElement(elementFlatFileWriterEntry);

            //link
            var linkElement = new ElementsLinkEntry(elementFlatileReaderEntry, elementFlatFileWriterEntry);
            linkElement.ParentTask = taskEntry;
            linkElement.Mappings.Add(new ElementsMappingEntry
            {
                InputPropertyKey = "Table",
                OutputPropertyKey = "Table",
                ParentLink = linkElement
            });
            engineRepository.CreateElement(linkElement);

            taskEntry.Elements.Add(elementFlatileReaderEntry);
            taskEntry.Elements.Add(linkElement);
            taskEntry.Elements.Add(elementFlatFileWriterEntry);

            uow.Commit();

            var engineTask = serviceProvider.GetService<IEngineTask>();
            engineTask.LoadFromEntity(taskEntry.Id);

            engine.ExecuteTask(engineTask);

        }

            private static FlatFileReaderEntry GetFlatFileReaderEntry()
        {
            var elementFlatileReaderEntry = new FlatFileReaderEntry();
            elementFlatileReaderEntry.InputProperties.FirstOrDefault(x => x.Key == "ColumnDelimiter").Value = "<char>44</char>";
            elementFlatileReaderEntry.InputProperties.FirstOrDefault(x => x.Key == "FirstRowHasHeader").Value = "<boolean>false</boolean>";
            elementFlatileReaderEntry.InputProperties.FirstOrDefault(x => x.Key == "SourceFilePath").Value = @"<string>.\TestDataFiles\provincia-regione-sigla.csv</string>";
            return elementFlatileReaderEntry;
        }


        private static FlatFileWriterEntry GetFlatFileWriterEntry()
        {
            var elementFlatileWriterEntry = new FlatFileWriterEntry();
            elementFlatileWriterEntry.InputProperties.FirstOrDefault(x => x.Key == "ColumnDelimiter").Value = "<char>44</char>";
            //elementFlatileWriterEntry.InputProperties.FirstOrDefault(x => x.Key == "Table").Value = "<boolean>false</boolean>";
            elementFlatileWriterEntry.InputProperties.FirstOrDefault(x => x.Key == "TargetFilePath").Value = @"<string>.\TestDataFiles\provincia-regione-sigla.output.csv</string>";
            return elementFlatileWriterEntry;
        }

        private static TaskEntry GetTaskEntry()
        {
            return new TaskEntry
            {
                FailIfAnyElementHasError = true,
                FailIfAnyElementHasWarning = false,
                IsEnabled = true,
                Key = "TaskTest",
                Name = "Task di prova"
            };
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

        //private FlatFileReader GetFlatFileReader()
        //{
        //    var flatFileReader = new FlatFileReader(uow, engineRepository, engineElementLogger, messageService);
        //    flatFileReader.SetDataValue(EngineDataDirection.Input, "SourceFilePath", new StringValue(@".\TestDataFiles\provincia-regione-sigla.csv"));
        //    flatFileReader.SetDataValue(EngineDataDirection.Input, "FirstRowHasHeader", new BoolValue(false));
        //    flatFileReader.SetDataValue(EngineDataDirection.Input, "ColumnDelimiter", new CharValue(','));
        //    return flatFileReader;
        //}
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