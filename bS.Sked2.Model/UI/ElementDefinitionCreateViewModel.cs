using bS.Sked2.Structure.Engine.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bS.Sked2.Model.UI
{
    public class ElementDefinitionCreateViewModel : IElementDefinitionCreate
    {
        [Required]
        public string ElementTypeKey { get; set; }
        [StringLength(50), Required]
        public string Name { get; set; }
        [StringLength(250), Required]
        public string Description { get; set; }
        public IEnumerable<IElementType> ElementTypesList { get; set; }
    }
}
