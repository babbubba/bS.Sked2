using System;
using System.Collections.Generic;
using System.Text;
using bS.Sked2.Structure.Service;
using Microsoft.Extensions.Logging;

namespace bS.Sked2.Structure.Engine
{
    public abstract class BaseEngineModule : BaseEngineComponent, IEngineModule
    {
        public BaseEngineModule(ILogger logger, IMessageService messageService) : base(logger, messageService)
        {
        }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; protected set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; protected set; }

        public void Init()
        {
            instanceId = Guid.NewGuid();
        }
    }
}
