using bs.Data;
using bs.Data.Interfaces;
using bS.Sked2.Structure.Models;
using bS.Sked2.Structure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Model.Repositories
{
    public class EngineRepository : Repository, IEngineRepository
    {
        public EngineRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {


        }

        public IJobEntry GetJobById(Guid Id)
        {
            return base.GetById<JobEntry>(Id);
        }

        public ITaskEntry GetTaskById(Guid Id)
        {
            return base.GetById<TaskEntry>(Id);
        }

        public IElementEntry GetElementById(Guid Id)
        {
            return base.GetById<ElementEntry>(Id);
        }

        public IModuleEntry GetModuleById(Guid Id)
        {
            return base.GetById<ModuleEntry>(Id);
        }

        public IInstanceEntry CreateNewInstance()
        {
            var newInstance = new InstanceEntry();
            base.Create(newInstance);
            return newInstance;
        }

        public void CreateElement(IElementEntry entityToCreate)
        {
            base.Create((ElementEntry)entityToCreate);
        }

        public void CreateTask(ITaskEntry taskEntry)
        {
            base.Create((TaskEntry)taskEntry);
        }

        //public void CreateLinkElement(IElementsLinkEntry linkElement)
        //{
        //    base.Create((ElementsLinkEntry)linkElement);

        //}
    }
}
