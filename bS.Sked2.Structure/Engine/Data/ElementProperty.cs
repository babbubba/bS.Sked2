using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine.Data
{
    public class ElementProperty : IEngineElementProperty
    {
        public ElementProperty(string key, string description, DataType dataType, bool mandatory)
        {
            Key = key;
            Description = description;
            DataType = dataType;
            Mandatory = mandatory;
        }

        public string Key { get; }

        public string Description { get; }

        public DataType DataType { get; }

        public bool Mandatory { get; }

        public IEngineData Value { get; set; }

    }
}
