using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine.Data
{
    class CollectionValue : BaseEngineValue
    {
        public CollectionValue()
        {
            value = new List<IEngineData>();
            dataType = DataType.Collection;
        }

        public CollectionValue(IList<IEngineData> val)
        {
            value = new List<IEngineData>();
            dataType = DataType.Collection;
            Set(val);
        }

        public void AddValue(IEngineData value)
        {
            ((List<IEngineData>)value).Add(value);
        }

        public void RemoveValue(IEngineData value)
        {
            ((List<IEngineData>)value).Remove(value);
        }


    }
}
