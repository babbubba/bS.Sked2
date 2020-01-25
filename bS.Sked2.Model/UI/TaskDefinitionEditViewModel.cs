using bS.Sked2.Structure.Engine.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bS.Sked2.Model.UI
{
    public class TaskDefinitionEditViewModel : ITaskDefinitionEdit
    {
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [StringLength(250), Required]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [fail if any task has error].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [fail if any task has error]; otherwise, <c>false</c>.
        /// </value>
        [Required]
        public bool FailIfAnyElementHasError { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [fail if any task has warning].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [fail if any task has warning]; otherwise, <c>false</c>.
        /// </value>
        [Required]
        public bool FailIfAnyElementHasWarning { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        [Required]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [StringLength(50), Required]
        public string Name { get; set; }
    }
}
