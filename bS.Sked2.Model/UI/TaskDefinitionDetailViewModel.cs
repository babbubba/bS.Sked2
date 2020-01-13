using bS.Sked2.Structure.Engine.UI;
using System;

namespace bS.Sked2.Model.UI
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="bS.Sked2.Structure.Engine.UI.ITaskDefinitionDetail" />
    public class TaskDefinitionDetailViewModel : ITaskDefinitionDetail
    {
        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [fail if any element has error].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [fail if any element has error]; otherwise, <c>false</c>.
        /// </value>
        public bool FailIfAnyElementHasError { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [fail if any element has warning].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [fail if any element has warning]; otherwise, <c>false</c>.
        /// </value>
        public bool FailIfAnyElementHasWarning { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the last update date.
        /// </summary>
        /// <value>
        /// The last update date.
        /// </value>
        public DateTime? LastUpdateDate { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public int Position { get; set; }
    }
}