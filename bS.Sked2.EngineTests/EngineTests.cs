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

namespace bS.Sked2.Engine.Tests
{
    [TestClass()]
    public class EngineTests
    {
        private ILogger<Engine> logger;
        private IMessageService messageService;
        private Engine engine;

        [TestInitialize]
        public void Init()
        { 
           logger = Mock.Of<ILogger<Engine>>();
            engine = new Engine(logger);
        }
        [TestMethod()]
        public void ExecuteElementTest()
        {
            var messageService = new MessageService();


            var commonModule = new Extensions.Common.Common(logger, messageService);
            commonModule.Init();

            var job = new EngineJob(logger, messageService);
            job.Start();

            var task = new EngineTask(logger, messageService);
            task.ParentJob = job;
            task.Start();

            var flatFileReader = new FlatFileReader(logger, messageService);
            flatFileReader.ParentModule = commonModule;
            flatFileReader.ParentTask = task;
            flatFileReader.SetDataValue(EngineDataDirection.Input, "SourceFilePath", new StringValue(@"c:\temp\provincia-regione-sigla.csv"));
            flatFileReader.SetDataValue(EngineDataDirection.Input, "FirstRowHasHeader", new BoolValue(false));
            flatFileReader.SetDataValue(EngineDataDirection.Input, "ColumnDelimiter", new CharValue(','));
            engine.ExecuteElement(flatFileReader);
            var resultFlatFileReader = flatFileReader.GetDataValue(EngineDataDirection.Output, "Table").Get<DataTable>();

            //            var sqlQueryReader = new SqlQueryReader(logger, messageService);
            //            sqlQueryReader.ParentModule = commonModule;
            //            sqlQueryReader.ParentTask = task;
            //            sqlQueryReader.SetDataValue(EngineDataDirection.Input, "ConnectionString", new StringValue(@"Data Source=(localdb)\mssqllocaldb;Persist Security Info=True;User ID=sa;Password=gabe;Database=EPS_HR;"));
            //            sqlQueryReader.SetDataValue(EngineDataDirection.Input, "SqlQuery", new StringValue(@"
            //SELECT [Id]
            //      ,[Code]
            //      ,[PayrollingCode]
            //      ,[Name]
            //      ,[Description]
            //      ,[Order]
            //      ,[Type]
            //      ,[IsEnabled]
            //  FROM [BaseActivities];"));
            //            engine.ExecuteElement(sqlQueryReader);
            //            var resultSqlQueryReader = sqlQueryReader.GetDataValue(EngineDataDirection.Output, "Table").Get<DataTable>();

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
            sqlWriter.SetDataValue(EngineDataDirection.Input, "SourceTable", flatFileReader.GetDataValue(EngineDataDirection.Output, "Table"));
         
            engine.ExecuteElement(sqlWriter);

        }
    }
}