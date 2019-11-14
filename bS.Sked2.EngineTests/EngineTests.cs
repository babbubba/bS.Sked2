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
           messageService = Mock.Of<IMessageService>();
            engine = new Engine(logger);
        }
        [TestMethod()]
        public void ExecuteElementTest()
        {
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

            flatFileReader.SetDataValue(EngineDataDirection.Input, "SourceFilePath", new StringValue(@"c:\temp\flatfile.csv"));
            flatFileReader.SetDataValue(EngineDataDirection.Input, "FirstRowHasHeader", new BoolValue(true));
            flatFileReader.SetDataValue(EngineDataDirection.Input, "ColumnDelimiter", new CharValue(';'));
            engine.ExecuteElement(flatFileReader);
        }
    }
}