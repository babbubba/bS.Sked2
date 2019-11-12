using bS.Sked2.Structure.Base;
using System.Text;

namespace bS.Sked2.Structure.Engine
{
    /// <summary>
    /// E' l'elemento che rappresenta una operazione specifica in un Task. Ogni elemento viene eseguito dal suo specifico modulo <see cref="IEngineModule"/>.
    /// </summary>
    public interface IEngineElement : IStartable
    {
        /// <summary>
        /// Gets the data input value from the data key.
        /// </summary>
        /// <param name="dataKey">The data key.</param>
        /// <returns></returns>
        IEngineData GetDataInputValue(string dataKey);
        /// <summary>
        /// Gets the data output value from the data key.
        /// </summary>
        /// <param name="dataKey">The data key.</param>
        /// <returns></returns>
        IEngineData GetDataOutputValue(string dataKey);

        /// <summary>
        /// Sets the data input value for the specified the data key.
        /// </summary>
        /// <param name="dataKey">The data key.</param>
        /// <param name="value">The value.</param>
        void SetDataInputValue(string dataKey, IEngineData value);
        /// <summary>
        /// Sets the data output value for the specified the data key.
        /// </summary>
        /// <param name="dataKey">The data key.</param>
        /// <param name="value">The value.</param>
        void SetDataOutputValue(string dataKey, IEngineData value);
        /// <summary>
        /// Gets the parent task.
        /// </summary>
        /// <value>
        /// The parent task.
        /// </value>
        IEngineTask ParentTask { get; }
        /// <summary>
        /// Gets the parent Engine Module.
        /// </summary>
        /// <value>
        /// The parent module.
        /// </value>
        IEngineModule ParentModule { get; }
    }
}
