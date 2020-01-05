using bS.Sked2.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace bS.Sked2.Structure.Engine.Data
{
    public class BoolValue : BaseEngineValue
    {
        public override DataType DataType => DataType.Bool;
        public static DataType DataTypeConst => DataType.Bool;

        public override bool CanPersistInStorage => true;

        public BoolValue()
        {

        }
        public BoolValue(bool value)
        {
            Set(value);
        }

        public override void ReadFromStringValue(string stringValue)
        {
            DeserializeValueFromString<bool>(stringValue);
        }
    }
}
