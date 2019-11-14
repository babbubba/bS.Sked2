﻿using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine
{
    /// <summary>
    /// Tutti i moduli di estensione devono implementare questa interfaccia. Un modulo di estensione rappresenta un insieme di operazioni disponibili all'interno di un task. Ogni operazione è rappresentata da un Elemento <see cref="IEngineElement"/>
    /// </summary>
    public interface IEngineModule : IEngineComponent
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        string Key { get; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        string Description { get; }

        void Init();
    }
}
