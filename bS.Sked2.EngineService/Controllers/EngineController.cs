using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using bS.Sked2.Engine.Objects;
using bS.Sked2.Model.Repositories;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Repositories;
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
        private readonly IEngine engine;

        public EngineController(ILogger<EngineController> logger, ILifetimeScope container,  IEngine engine)
        {
            this.logger = logger;
            this.container = container;
            this.engine = engine;
        }
        [HttpGet]
        public bool ExecuteTask(Guid Id)
        {
            using (var scope = container.BeginLifetimeScope())
            {
                var engineTask = scope.Resolve<IEngineTask>();
                engineTask.LoadFromEntity(Id);
                engine.ExecuteTask(engineTask);

            }

            return true;
        }
    }
}