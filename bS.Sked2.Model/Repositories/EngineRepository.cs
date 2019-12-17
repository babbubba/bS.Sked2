using bs.Data;
using bs.Data.Interfaces;
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

        public JobEntry GetJobById(Guid Id)
        {
            return base.GetById<JobEntry>(Id);
        }

        public TaskEntry GetTaskById(Guid Id)
        {
            return base.GetById<TaskEntry>(Id);
        }
    }
}
