﻿using bS.Sked2.Structure.Engine.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bS.Sked2.Model.UI
{
    public class ElementDefinitionEditViewModel : IElementDefinitionEdit
    {
        [StringLength(50), Required]
        public string Name { get; set; }

        [StringLength(250), Required]
        public string Description { get; set; }

        [Required]
        public Guid? ParentModuleId { get; set; }

        [Required]
        public IEnumerable<IPropertyDefinition> InputProperties { get; set; }

        [Required]
        public IEnumerable<IPropertyDefinition> OutputProperties { get; set; }
    }
}