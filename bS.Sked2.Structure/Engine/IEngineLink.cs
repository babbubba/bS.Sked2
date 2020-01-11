using bS.Sked2.Structure.Models;
using System.Collections.Generic;

namespace bS.Sked2.Structure.Engine
{
    /// <summary>
    /// Base interface for Links. A link is a flow component between two flow components that permits mapping between them.
    /// </summary>
    /// <seealso cref="bS.Sked2.Structure.Engine.IEngineFlowComponent" />
    public interface IEngineLink : IEngineFlowComponent
    {
        /// <summary>
        /// Gets the mappings.
        /// </summary>
        /// <value>
        /// The mappings.
        /// </value>
        IEnumerable<IElementsMappingEntry> Mappings { get; }

        /// <summary>
        /// Gets the next element.
        /// </summary>
        /// <value>
        /// The next element.
        /// </value>
        IElementEntry NextElement { get; }

        /// <summary>
        /// Gets the previuous element.
        /// </summary>
        /// <value>
        /// The previuous element.
        /// </value>
        IElementEntry PreviuousElement { get; }
    }
}