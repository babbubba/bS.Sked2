using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine
{
    /// <summary>
    /// Interfaccia base per ogni dato in ingresso o in uscita da un Elemento <see cref="IEngineElement"/>
    /// </summary>
    public interface IEngineData
    {
        public string DataType { get; }
        public bool CanPersistInStorage { get; set; }
        public bool IsFilled { get; set; }
        public bool IsValid { get; set; }
        public void Set(IEngineDataValue value);
        public IEngineDataValue Get();
    }

    /// <summary>
    /// BAse interface for atomic data
    /// </summary>
    public interface IEngineDataValue
    {
    }
}
