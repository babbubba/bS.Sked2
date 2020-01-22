using System;
using System.ComponentModel.DataAnnotations;

namespace bS.Sked2.Structure.Engine.UI
{
    /// <summary>
    ///
    /// </summary>
    public interface IJobDefinitionEdit
    {
        [Required]
        Guid Id {get;set;}
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [StringLength(250), Required]
        string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [fail if any task has error].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [fail if any task has error]; otherwise, <c>false</c>.
        /// </value>
        [Required]
        bool FailIfAnyTaskHasError { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [fail if any task has warning].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [fail if any task has warning]; otherwise, <c>false</c>.
        /// </value>
        [Required]
        bool FailIfAnyTaskHasWarning { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        [Required]
        bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [StringLength(50), Required]
        string Name { get; set; }
    }
}