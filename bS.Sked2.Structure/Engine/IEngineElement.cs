using bS.Sked2.Structure.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine
{
    /// <summary>
    /// E' l'elemento che rappresenta una operazione specifica in un Task. Ogni elemento viene eseguito dal suo specifico modulo <see cref="IEngineModule"/>.
    /// </summary>
    public interface IEngineElement : IStartable
    {
    }
}
