using bS.Sked2.Structure.Engine.UI;
using System.ComponentModel.DataAnnotations;

namespace bS.Sked2.Model.UI
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="bS.Sked2.Structure.Engine.UI.ITaskDefinitionCreate" />
    public class TaskDefinitionCreateViewModel : ITaskDefinitionCreate
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [StringLength(250), Required]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [fail if any element has error].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [fail if any element has error]; otherwise, <c>false</c>.
        /// </value>
        [Required]
        public bool FailIfAnyElementHasError { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [fail if any element has warning].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [fail if any element has warning]; otherwise, <c>false</c>.
        /// </value>
        [Required]
        public bool FailIfAnyElementHasWarning { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [StringLength(50), Required]
        public string Name { get; set; }
        [Required]
        public bool IsEnabled { get; set; }
        [Required]
        public string ParentJobId { get; set; }
    }
}