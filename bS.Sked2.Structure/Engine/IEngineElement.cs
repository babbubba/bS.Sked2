using bS.Sked2.Structure.Models;

namespace bS.Sked2.Structure.Engine
{
    /// <summary>
    /// E' l'elemento che rappresenta una operazione specifica in un Task. Ogni elemento viene eseguito dal suo specifico modulo <see cref="IEngineModule" />.
    /// </summary>
    /// <seealso cref="bS.Sked2.Structure.Base.IStartable" />
    /// <seealso cref="bS.Sked2.Structure.Engine.IEngineComponent" />
    public interface IEngineElement : IEngineComponent
    {
        string Key { get; }

        /// <summary>
        /// Gets the parent Engine Module.
        /// </summary>
        /// <value>
        /// The parent module.
        /// </value>
        IModuleEntry ParentModule { get; }

        /// <summary>
        /// Gets the parent task.
        /// </summary>
        /// <value>
        /// The parent task.
        /// </value>
        ITaskEntry ParentTask { get; }

        /// <summary>
        /// Gets the data value.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <param name="propertyKey">The property key.</param>
        /// <returns></returns>
        IEngineData GetDataValue(EngineDataDirection direction, string propertyKey);

        /// <summary>
        /// Registers the input properties.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="description">The description.</param>
        /// <param name="dataType">Type of the data.</param>
        /// <param name="mandatory">if set to <c>true</c> [mandatory].</param>
        void RegisterInputProperties(string key, string description, DataType dataType, bool mandatory = false);

        /// <summary>
        /// Registers the output properties.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="description">The description.</param>
        /// <param name="dataType">Type of the data.</param>
        /// <param name="mandatory">if set to <c>true</c> [mandatory].</param>
        void RegisterOutputProperties(string key, string description, DataType dataType, bool mandatory = false);

        /// <summary>
        /// Sets the data value.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <param name="propertyKey">The property key.</param>
        /// <param name="value">The value.</param>
        void SetDataValue(EngineDataDirection direction, string propertyKey, IEngineData value);

        void SetDataValueIfEmpty(EngineDataDirection direction, string propertyKey, IEngineData value);
    }
}