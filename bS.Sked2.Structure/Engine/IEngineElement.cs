using bS.Sked2.Structure.Models;

namespace bS.Sked2.Structure.Engine
{
    /// <summary>
    /// Element interface./>.
    /// </summary>
    /// <seealso cref="Base.IStartable" />
    /// <seealso cref="IEngineFlowComponent" />
    public interface IEngineElement : IEngineFlowComponent
    {
        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        string Key { get; }

        /// <summary>
        /// Gets the parent Engine Module.
        /// </summary>
        /// <value>
        /// The parent module.
        /// </value>
        IModuleEntry ParentModule { get; }
        IEngineModule ParentEngineModule { get; set; }
        IEngineElementProperty[] InputProperties { get; }
        IEngineElementProperty[] OutputProperties { get; }


        /// <summary>
        /// Gets the parent task.
        /// </summary>
        /// <value>
        /// The parent task.
        /// </value>
        ITaskEntry ParentTask { get; }

        /// <summary>
        /// Gets the data value from a property.
        /// </summary>
        /// <param name="direction">The property's direction.</param>
        /// <param name="propertyKey">The property's key.</param>
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
        /// Sets the property's data value.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <param name="propertyKey">The property key.</param>
        /// <param name="value">The value.</param>
        void SetDataValue(EngineDataDirection direction, string propertyKey, IEngineData value);

        /// <summary>
        /// Sets the property's data value only if actual value is empty.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <param name="propertyKey">The property key.</param>
        /// <param name="value">The value.</param>
        void SetDataValueIfEmpty(EngineDataDirection direction, string propertyKey, IEngineData value);
    }
}