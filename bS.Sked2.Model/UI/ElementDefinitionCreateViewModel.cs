using bS.Sked2.Structure.Engine.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bS.Sked2.Model.UI
{
    public class ElementDefinitionCreateViewModel : IElementDefinitionCreate
    {
        public string ElementTypeKey { get; set; }
        [StringLength(50), Required]
        public string Name { get; set; }
        [StringLength(250), Required]
        public string Description { get; set; }
        [Required]
        public Guid ParentTaskId { get; set; }
        public IEnumerable<IElementType> ElementTypesList { get; set; }
        public Guid Id { get; set; }
    }
}
