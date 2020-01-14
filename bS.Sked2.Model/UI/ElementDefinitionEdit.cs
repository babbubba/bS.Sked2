using bS.Sked2.Structure.Engine.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bS.Sked2.Model.UI
{
    public class ElementDefinitionEdit : IElementDefinitionEdit
    {
        [StringLength(50), Required]
        public string Name { get; set; }
        [StringLength(250), Required]
        public string Description { get; set; }
        [Required]
        public Guid? ParentModuleId { get; set; }
        [Required]
        public IEnumerable<IElementPropertyDefinition> InputProperties { get; set; }
        [Required]
        public IEnumerable<IElementPropertyDefinition> OutputProperties { get; set; }
    }

    public class ElementPropertyDefinition : IElementPropertyDefinition
    {
        [Required]
        public Structure.Engine.DataType DataType { get; set; }
        [Required]
        public string Value { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
    }
}
