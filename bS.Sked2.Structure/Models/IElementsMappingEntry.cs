namespace bS.Sked2.Structure.Models
{
    /// <summary>
    /// The element mapping persister. Every Link element (<see cref="IElementsLinkEntry"/>) owns a list of property mapping beetwen previous and next elements in the task execution flow.
    /// </summary>
    public interface IElementsMappingEntry
    {
        /// <summary>
        /// Gets or sets the input property key.
        /// </summary>
        /// <value>
        /// The input property key.
        /// </value>
        string InputPropertyKey { get; set; }

        /// <summary>
        /// Gets or sets the output property key.
        /// </summary>
        /// <value>
        /// The output property key.
        /// </value>
        string OutputPropertyKey { get; set; }

        /// <summary>
        /// Gets or sets the parent link.
        /// </summary>
        /// <value>
        /// The parent link.
        /// </value>
        IElementsLinkEntry ParentLink { get; set; }
    }
}