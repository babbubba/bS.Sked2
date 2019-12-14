using bS.Sked2.Structure.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine
{
    /// <summary>
    /// Contiene unio o più Task <see cref="IEngineTask"/> e sostanzialmente rappresengta un lavoro completo composto da uno o più compiti.
    /// </summary>
    public interface IEngineJob : IStartable, IEngineComponent
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        string Key { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        string Description { get; set; }
        IEngineTask[] Tasks { get; set; }
        IEngineTrigger[] Triggers { get; set; }

        bool FailIfAnyTaskHasError { get; set; }
        bool FailIfAnyTaskHasWarning { get; set; }
    }
}
