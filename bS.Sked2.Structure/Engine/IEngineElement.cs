using bS.Sked2.Structure.Base;
using bS.Sked2.Structure.Service.Messages;
using System;
using System.Text;

namespace bS.Sked2.Structure.Engine
{
    /// <summary>
    /// E' l'elemento che rappresenta una operazione specifica in un Task. Ogni elemento viene eseguito dal suo specifico modulo <see cref="IEngineModule"/>.
    /// </summary>
    public interface IEngineElement : IStartable, IEngineComponent
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        string Key { get;  }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get;  }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        string Description { get;  }

        /// <summary>
        /// Gets the parent task.
        /// </summary>
        /// <value>
        /// The parent task.
        /// </value>
        IEngineTask ParentTask { get; set; }

        /// <summary>
        /// Gets the parent Engine Module.
        /// </summary>
        /// <value>
        /// The parent module.
        /// </value>
        IEngineModule ParentModule { get; set; }

        /// <summary>
        /// Registers the input properties.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="description">The description.</param>
        /// <param name="dataType">Type of the data.</param>
        void RegisterInputProperties(string key, string description, DataType dataType, bool mandatory = false);

        /// <summary>
        /// Registers the output properties.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="description">The description.</param>
        /// <param name="dataType">Type of the data.</param>
        void RegisterOutputProperties(string key, string description, DataType dataType, bool mandatory = false);
       
        /// <summary>
        /// Gets the data value.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <param name="propertyKey">The property key.</param>
        /// <returns></returns>
        IEngineData GetDataValue(EngineDataDirection direction, string propertyKey);
        /// <summary>
        /// Sets the data value.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <param name="propertyKey">The property key.</param>
        /// <param name="value">The value.</param>
        void SetDataValue(EngineDataDirection direction, string propertyKey, IEngineData value);

        /// <summary>
        /// Adds the message.
        /// </summary>
        /// <param name="Message">The message.</param>
        /// <param name="severity">The severity (Optional: default is Info).</param>
        void AddMessage(string Message, MessageSeverity severity = MessageSeverity.Info);
    
    }
}
