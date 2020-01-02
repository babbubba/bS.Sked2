using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine
{
    /// <summary>
    /// Tutti i moduli di estensione devono implementare questa interfaccia. Un modulo di estensione rappresenta un insieme di operazioni disponibili all'interno di un task. Ogni operazione è rappresentata da un Elemento <see cref="IEngineElement"/>
    /// </summary>
    public interface IEngineModule
    {
        string Key { get; }
        void Init();
    }
}
