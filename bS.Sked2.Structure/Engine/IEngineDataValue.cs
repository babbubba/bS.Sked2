using System;

namespace bS.Sked2.Structure.Engine
{
    /// <summary>
    /// Base interface for atomic data
    /// </summary>
    [Obsolete]
    public interface IEngineDataValue
    {
        /// <summary>
        /// Gets the type of the data value.
        /// </summary>
        /// <value>
        /// The type of the data.
        /// </value>
        public string DataType { get; }

        public T GetValue<T>();
       
    }
}
