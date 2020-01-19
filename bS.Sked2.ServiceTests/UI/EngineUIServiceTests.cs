using bs.Data;
using bs.Data.Interfaces;
using bS.Sked2.Model.Engine;
using bS.Sked2.Model.Repositories;
using bS.Sked2.Model.Service;
using bS.Sked2.Shared;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Engine.Data;
using bS.Sked2.Structure.Engine.Data.Types;
using bS.Sked2.Structure.Repositories;
using bS.Sked2.Structure.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace bS.Sked2.Service.UI.Tests
{
    [TestClass()]
    public class EngineUIServiceTests
    {
        private ServiceProvider serviceProvider;

        [TestMethod()]
        public void CreationAndEditTest()
        {
            // Get the services
            var engine = serviceProvider.GetService<IEngine>();
            engine.Init();

            var engineUIService = serviceProvider.GetService<IEngineUIService>();

            // Create job
            var jobVM = engineUIService.GetCreateJob();
            jobVM.Name = "Job di prova 2";
            jobVM.Description = "Job di prova creato con UI service e contenente un task.";
            jobVM.FailIfAnyTaskHasError = true;
            var jobId = engineUIService.CreateNewJob(jobVM);

            // Edit Job
            var jobEditVM = engineUIService.GetEditJob(jobId);
            jobEditVM.Description = "Job di prova creato con UI service (modificato).";
            engineUIService.EditJob(jobId, jobEditVM);

            // Create task
            var taskVM = engineUIService.GetCreateTask();
            taskVM.Name = "Task di prova 2";
            taskVM.Description = "Task di prova creato con UI service.";
            var taskId = engineUIService.CreateNewTask(taskVM);

            // Add task to job
            engineUIService.AddTaskToJob(jobId, taskId);

            // Edit the task
            var taskEditVM = engineUIService.GetEditTask(taskId);
            taskEditVM.Description = "Task di prova creato con UI service (modificato).";
            engineUIService.EditTask(taskId, taskEditVM);

            // Create the module
            var moduleVM = engineUIService.GetCreateModule();
            moduleVM.Name = "Modulo di prova(Common)";
            moduleVM.Description = "Modulo di prova Common";
            moduleVM.ParentTaskId = taskId;
            moduleVM.ModuleTypeKey = moduleVM.ModuleTypesList.First(etp => etp.Key == "Common").Key;
            var moduleId = engineUIService.CreateNewModule(moduleVM);

            //Edit the module
            var moduleEditVM = engineUIService.GetEditModule(moduleId);
            //moduleEditVM.ParentTaskId = taskId;
            moduleEditVM.InputProperties.First(p => p.Key == "WorkspacePath").Value = new VirtualPathValue(new VirtualPath(@".\")).WriteToStringValue().Base64Encode();
            engineUIService.EditModule(moduleId, moduleEditVM);

            // Create Element 1 (FlatFileReader)
            var element1VM = engineUIService.GetCreateElement();
            element1VM.Name = "Read CSV File";
            element1VM.Description = "Read teh CSV file with the cities.";
            element1VM.ElementTypeKey = element1VM.ElementTypesList.First(etp => etp.Key == "FlatFileReader").Key;
            element1VM.ParentTaskId = taskId;
            var element1Id = engineUIService.CreateNewElement(element1VM);

            // Edit the Element1 (FlatFileReader)
            var element1EditVM = engineUIService.GetEditElement(element1Id);
            element1EditVM.ParentModuleId = moduleId;
            element1EditVM.InputProperties.First(p => p.Key == "ColumnDelimiter").Value = new CharValue(',').WriteToStringValue().Base64Encode();
            element1EditVM.InputProperties.First(p => p.Key == "FirstRowHasHeader").Value = new BoolValue(false).WriteToStringValue().Base64Encode();
            element1EditVM.InputProperties.First(p => p.Key == "SourceFilePath").Value = new VirtualPathValue(new VirtualPath(@"\TestDataFiles\provincia-regione-sigla.csv")).WriteToStringValue().Base64Encode();
            engineUIService.EditElement(element1Id, element1EditVM);
        }

        [TestInitialize]
        public void Init()
        {
            var services = new ServiceCollection();

            var dbContext = new DbContext
            {
                ConnectionString = "Data Source=.\\bs.Data.Test.db;Version=3;BinaryGuid=False;",
                DatabaseEngineType = "sqlite",
                Create = false,
                Update = true,
                LookForEntitiesDllInCurrentDirectoryToo = true,
                EntitiesFileNameScannerPatterns = new string[] { "bS.Sked2.Extensions.*.dll", "bS.Sked2.Model.dll" }
            };

            var engineUiServiceConfigx = new EngineUIServiceConfig
            {
                ExtensionsFolder = @"..\..\..\..\bS.Sked2.Extensions.Common\bin\Debug\netcoreapp3.0\"
            };

            var engineConfig = new EngineConfig
            {
                ExtensionsFolder = @"..\..\..\..\bS.Sked2.Extensions.Common\bin\Debug\netcoreapp3.0\"
            };

            services.AddSingleton(Mock.Of<ILogger>());
            services.AddSingleton<IDbContext>(dbContext);
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IEngineRepository, EngineRepository>();
            services.AddSingleton<IEngineConfig>(engineConfig);
            services.AddSingleton<IEngine, Engine.Engine>();
            services.AddSingleton<IEngineUIServiceConfig>(engineUiServiceConfigx);
            services.AddTransient<IEngineUIService, EngineUIService>();
            serviceProvider = services.BuildServiceProvider();
        }
    }
}