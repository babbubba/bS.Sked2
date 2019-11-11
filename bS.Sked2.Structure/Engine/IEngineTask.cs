using bS.Sked2.Structure.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine
{
    /// <summary>
    /// Contiene uno o piu Elementi <see cref="IEngineElement"/> e rappresenta il flusso di esecuzione degli elementi stessi. Sostanzialmente rappresenta un compito finito.
    /// </summary>
    public interface IEngineTask : IStartable
    {
    }
}
