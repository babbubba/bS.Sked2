using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using bS.Sked2.Structure.Engine;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace bS.Sked2.EngineService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EngineController : ControllerBase
    {
        private readonly ILogger<EngineController> logger;
        private readonly ILifetimeScope container;

        public EngineController(ILogger<EngineController> logger, ILifetimeScope container)
        {
            this.logger = logger;
            this.container = container;
        }
        [HttpGet]
        public bool ExecuteTask()
        {
            using (var scope = container.BeginLifetimeScope())
            {
                var job = scope.Resolve<IEngineJob>();
                var task = scope.Resolve<IEngineTask>();
                var engine = scope.Resolve<IEngine>();

                task.ParentJob = job;
                task.Name = "Task di prova";
                task.Key = Guid.NewGuid().ToString();
                task.Elements = new IEngineElement[] { };
                engine.ExecuteTask(task);

                // engine.ExecuteJob()
                //engine.ExecuteJob()
                return true;
            }
        }
    }
}