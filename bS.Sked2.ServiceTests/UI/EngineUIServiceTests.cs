using bS.Sked2.Service.UI;
using bs.Data;
using bs.Data.Interfaces;
using bS.Sked2.Model.Repositories;
using bS.Sked2.Model.UI;
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
            // Get the service
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

            // Create Element 1
            var element1 = engineUIService.GetCreateElement();
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

            services.AddSingleton(Mock.Of<ILogger>());
            services.AddSingleton<IDbContext>(dbContext);
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IEngineRepository, EngineRepository>();
            services.AddSingleton<IEngineUIService, EngineUIService>();
            serviceProvider = services.BuildServiceProvider();
        }
    }
}