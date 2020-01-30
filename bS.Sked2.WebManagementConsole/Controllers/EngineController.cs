using bS.Sked2.Model.UI;
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
        [Route("getjobedit/{id}")]
        public IJobDefinitionEdit GetJobEdit(string id)
        {
            return engineUIService.GetEditJob(Guid.Parse(id));
        }

        [HttpGet]
        [Route("getjobcreate")]
        public IJobDefinitionCreate GetJobCreate()
        {
            return engineUIService.GetCreateJob();
        }

        [HttpPut]
        [Route("createjob")]
        public string CreateJob(JobDefinitionCreateViewModel jobDefinition)
        {
            return engineUIService.CreateNewJob(jobDefinition).ToString();
        }

        [HttpPut]
        [Route("savejob")]
        public bool SaveJob(JobDefinitionEditViewModel jobDefinition)
        {
            engineUIService.EditJob(jobDefinition);
            return true;
        }

        [HttpGet("{id}")]
        [Route("getjobtasks/{id}")]
        public IEnumerable<ITaskDefinitionDetail> GetJobTasks(string id)
        {
            return engineUIService.GetTasks().Where(x => x.ParentJobId == Guid.Parse(id));
        }

        [HttpGet("{id}")]
        [Route("gettask/{id}")]
        public ITaskDefinitionDetail GetTask(string id)
        {
            return engineUIService.GetTasks().FirstOrDefault(x => x.Id == Guid.Parse(id));
        }

        [HttpGet]
        [Route("gettaskcreate")]
        public ITaskDefinitionCreate GetTaskCreate()
        {
            return engineUIService.GetCreateTask();
        }

        [HttpPut]
        [Route("createtask")]
        public string CreateTask(TaskDefinitionCreateViewModel taskDefinition)
        {
            return engineUIService.CreateNewTask(taskDefinition).ToString();
        }

        [HttpPut]
        [Route("edittask")]
        public bool EditTask(TaskDefinitionEditViewModel taskDefinition)
        {
            engineUIService.EditTask(taskDefinition);
            return true;
        }

        [HttpGet("{id}")]
        [Route("gettaskelements/{id}")]
        public IEnumerable<IElementDefinitionPreview> GetTaskElements(string id)
        {
            return engineUIService.GetElementsPreview(Guid.Parse(id));
        }

        [HttpGet]
        [Route("getelementcreate")]
        public IElementDefinitionCreate GetElementreate()
        {
            return engineUIService.GetCreateElement();
        }

        [HttpPut]
        [Route("createelement")]
        public string CreateElement(ElementDefinitionCreateViewModel elementdefinition)
        {
            return engineUIService.CreateNewElement(elementdefinition).ToString();
        }
    }
}