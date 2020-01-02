using bS.Sked2.Structure.Models;
using System;

namespace bS.Sked2.Structure.Repositories

{
    public interface IEngineRepository
    {
        IJobEntry GetJobById(Guid Id);
        ITaskEntry GetTaskById(Guid Id);
        IElementEntity GetElementById(Guid Id);
        IModuleEntry GetModuleById(Guid Id);
        IInstanceEntry CreateNewInstance();
    }
}