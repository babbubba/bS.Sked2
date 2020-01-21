using bS.Sked2.Structure.Engine.UI;
using bS.Sked2.Structure.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace bS.Sked2.WebManagementConsole.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class EngineController : ControllerBase
    {
        private readonly IEngineUIService engineUIService;

        public EngineController(IEngineUIService engineUIService)
        {
            this.engineUIService = engineUIService;
        }

        [HttpGet]
        [Route("getjobs")]
        public IEnumerable<IJobDefinitionDetail> GetJobs()
        {
            return engineUIService.GetJobs();
        }
        [HttpGet("{id}")]
        [Route("getjob/{id}")]
        public IJobDefinitionDetail GetJob(string id)
        {
            return engineUIService.GetJobs().FirstOrDefault(x=>x.Id == Guid.Parse(id));
        }
    }
}