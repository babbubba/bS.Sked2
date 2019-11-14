using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine
{
    public interface IEngine
    {
        void ExecuteJob(IEngineJob job);
        void ExecuteTask(IEngineTask task);
        void ExecuteElement(IEngineElement element);
    }
}
