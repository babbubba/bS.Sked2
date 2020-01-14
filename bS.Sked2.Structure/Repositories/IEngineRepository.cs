using bS.Sked2.Structure.Models;
using System;
using System.Collections.Generic;

namespace bS.Sked2.Structure.Repositories

{
    public interface IEngineRepository
    {
        void CreateElement(IElementEntry entityToCreate);

        void CreateJob(IJobEntry jobEntry);

        void CreateModule(IModuleEntry moduleEntry);

        IInstanceEntry CreateNewInstance();

        void CreateTask(ITaskEntry taskEntry);

        IElementEntry GetElementById(Guid Id);

        IEnumerable<IElementEntry> GetElements();

        IJobEntry GetJobById(Guid Id);
        IEnumerable<IJobEntry> GetJobs();

        IModuleEntry GetModuleById(Guid Id);

        ITaskEntry GetTaskById(Guid Id);
        IEnumerable<ITaskEntry> GetTasks();

        void UpdateEment(IElementEntry elementEntry);

        void UpdateJob(IJobEntry jobEntry);

        void UpdateTask(ITaskEntry taskEntry);
    }
}