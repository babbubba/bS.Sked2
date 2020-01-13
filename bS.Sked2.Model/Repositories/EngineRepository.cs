using bs.Data;
using bs.Data.Interfaces;
using bS.Sked2.Structure.Models;
using bS.Sked2.Structure.Repositories;
using System;
using System.Collections.Generic;

namespace bS.Sked2.Model.Repositories
{
    public class EngineRepository : Repository, IEngineRepository
    {
        public EngineRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void CreateElement(IElementEntry entityToCreate)
        {
            base.Create((ElementEntry)entityToCreate);
        }

        public void CreateJob(IJobEntry jobEntry)
        {
            base.Create((JobEntry)jobEntry);
        }

        public void CreateModule(IModuleEntry moduleEntry)
        {
            base.Create((ModuleEntry)moduleEntry);
        }

        public IInstanceEntry CreateNewInstance()
        {
            var newInstance = new InstanceEntry();
            base.Create(newInstance);
            return newInstance;
        }

        public void CreateTask(ITaskEntry taskEntry)
        {
            base.Create((TaskEntry)taskEntry);
        }

        public IElementEntry GetElementById(Guid Id)
        {
            return base.GetById<ElementEntry>(Id);
        }

        public IJobEntry GetJobById(Guid Id)
        {
            return base.GetById<JobEntry>(Id);
        }

        public IEnumerable<IJobEntry> GetJobs()
        {
            return base.GetAll<JobEntry>();
        }

        public IModuleEntry GetModuleById(Guid Id)
        {
            return base.GetById<ModuleEntry>(Id);
        }

        public ITaskEntry GetTaskById(Guid Id)
        {
            return base.GetById<TaskEntry>(Id);
        }

        public IEnumerable<ITaskEntry> GetTasks()
        {
            return base.GetAll<TaskEntry>();
        }

        public void UpdateJob(IJobEntry jobEntry)
        {
            base.Update((JobEntry)jobEntry);
        }

        //public void CreateLinkElement(IElementsLinkEntry linkElement)
        //{
        //    base.Create((ElementsLinkEntry)linkElement);

        //}
    }
}