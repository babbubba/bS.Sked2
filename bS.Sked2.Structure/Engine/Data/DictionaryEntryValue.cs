using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace bS.Sked2.Structure.Engine.Data
{
    public class DictionaryEntryValue : BaseEngineValue
    {
        public override DataType DataType => DataType.DictionaryEntry;

        public override bool CanPersistInStorage => true;
        public DictionaryEntryValue()
        {

        }
        public DictionaryEntryValue(string key, string value)
        {
            var entry = new DictionaryEntry(key, value);
            Set(entry);
        }

        //public override string WriteToStringValue()
        //{
        //    var sb = new StringBuilder();
        //    sb.Append(StoragePrefixValue);
        //    sb.Append(((DictionaryEntry)value).Key);
        //    sb.Append("|!§@#");
        //    sb.Append(((DictionaryEntry)value).Value?.ToString()??"");
        //    return sb.ToString();
        //}

        public override void ReadFromStringValue(string stringValue)
        {
            DeserializeValueFromString<DictionaryEntry>(stringValue);
        }
    }
}
