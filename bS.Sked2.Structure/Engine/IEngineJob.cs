using bS.Sked2.Structure.Base;
using bS.Sked2.Structure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine
{
    /// <summary>
    /// Contiene unio o più Task <see cref="IEngineTask"/> e sostanzialmente rappresengta un lavoro completo composto da uno o più compiti.
    /// </summary>
    public interface IEngineJob : IEngineFlowComponent
    {
      
    }
}
