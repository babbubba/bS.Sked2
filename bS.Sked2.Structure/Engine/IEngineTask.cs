using bS.Sked2.Structure.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine
{
    /// <summary>
    /// Contiene uno o piu Elementi <see cref="IEngineElement"/> e rappresenta il flusso di esecuzione degli elementi stessi. Sostanzialmente rappresenta un compito finito.
    /// </summary>
    public interface IEngineTask : IStartable, IEngineComponent
    {
        /// <summary>
        /// Gets or sets the parent job.
        /// </summary>
        /// <value>
        /// The parent job.
        /// </value>
        IEngineJob ParentJob { get; set; }
        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        IEngineElement[] Elements { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [fail if any element has error].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [fail if any element has error]; otherwise, <c>false</c>.
        /// </value>
        bool FailIfAnyElementHasError { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [fail if any element has warning].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [fail if any element has warning]; otherwise, <c>false</c>.
        /// </value>
        bool FailIfAnyElementHasWarning { get; set; }
    }
}
