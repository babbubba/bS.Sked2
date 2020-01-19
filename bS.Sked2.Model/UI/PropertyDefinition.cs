using bS.Sked2.Structure.Engine.UI;
using System.ComponentModel.DataAnnotations;

namespace bS.Sked2.Model.UI
{
    public class PropertyDefinition : IPropertyDefinition
    {
        [Required]
        public Structure.Engine.DataType DataType { get; set; }
        [Required]
        public string Value { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
    }
}
