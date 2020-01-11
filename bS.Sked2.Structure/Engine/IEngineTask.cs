using bS.Sked2.Structure.Base;
using bS.Sked2.Structure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine
{
    /// <summary>
    /// The engine task class
    /// </summary>
    public interface IEngineTask : IEngineFlowComponent
    {
        /// <summary>
        /// Gets the parent task.
        /// </summary>
        /// <value>
        /// The parent task.
        /// </value>
        IJobEntry ParentJob { get; }
    }
}
