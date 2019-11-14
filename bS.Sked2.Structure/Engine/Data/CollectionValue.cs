using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine.Data
{
    public class CollectionValue : BaseEngineValue
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

        public void AddValue(IEngineData val)
        {
            ((List<IEngineData>)value).Add(val);
        }

        public void RemoveValue(IEngineData val)
        {
            ((List<IEngineData>)value).Remove(val);
        }


    }
}
