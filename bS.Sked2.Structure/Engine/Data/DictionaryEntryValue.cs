using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine.Data
{
    public class DictionaryEntryValue : BaseEngineValue
    {
        public DictionaryEntryValue()
        {
            CanPersistInStorage = true;
            dataType = DataType.DictionaryEntry;
        }

        public DictionaryEntryValue(string key, string value)
        {
            CanPersistInStorage = true;
            var entry = new DictionaryEntry(key, value);
            dataType = DataType.DictionaryEntry;
            Set(entry);
        }
    }
}
