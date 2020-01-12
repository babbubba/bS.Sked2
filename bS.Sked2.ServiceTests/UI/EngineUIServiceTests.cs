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
        public void JobMethodsTest()
        {
            // Get the service
            var engineUIService = serviceProvider.GetService<IEngineUIService>();

            // Create
            var jobVM = engineUIService.GetNewJob();
            jobVM.Name = "Job di prova 1";
            jobVM.Description = "Job di prova creato con UI service.";
            jobVM.FailIfAnyTaskHasError = true;

            var jobId = engineUIService.CreateNewJob(jobVM);

            Assert.IsNotNull(jobId);

            // Edit
            engineUIService.EditJob(jobId, new JobDefinitionEditViewModel
            {
                Description = "Job di prova creato con UI service (modificato).",
                FailIfAnyTaskHasError = false,
                FailIfAnyTaskHasWarning = false,
            });

            // Get
            var job = engineUIService.GetJobs().FirstOrDefault(j=>j.Id == jobId);

            Assert.AreEqual(job.Description, "Job di prova creato con UI service (modificato).");
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