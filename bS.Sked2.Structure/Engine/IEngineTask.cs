using bS.Sked2.Structure.Base;
using bS.Sked2.Structure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine
{
    /// <summary>
    /// Contiene uno o piu Elementi <see cref="IEngineElement"/> e rappresenta il flusso di esecuzione degli elementi stessi. Sostanzialmente rappresenta un compito finito.
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
