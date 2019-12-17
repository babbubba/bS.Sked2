using System;

namespace bS.Sked2.Model.Repositories
{
    public interface IEngineRepository
    {
        JobEntry GetJobById(Guid Id);
        TaskEntry GetTaskById(Guid Id);
    }
}