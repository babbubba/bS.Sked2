using System.Collections.Generic;

namespace bS.Sked2.Structure.Models
{
    /// <summary>
    /// The link element persister
    /// </summary>
    public interface IElementsLinkEntry
    {
        /// <summary>
        /// Gets or sets the mappings.
        /// </summary>
        /// <value>
        /// The mappings.
        /// </value>
        IList<IElementsMappingEntry> Mappings { get; set; }

        /// <summary>
        /// Gets or sets the next.
        /// </summary>
        /// <value>
        /// The next.
        /// </value>
        IElementEntry Next { get; set; }

        /// <summary>
        /// Gets or sets the previuous.
        /// </summary>
        /// <value>
        /// The previuous.
        /// </value>
        IElementEntry Previuous { get; set; }
    }
}