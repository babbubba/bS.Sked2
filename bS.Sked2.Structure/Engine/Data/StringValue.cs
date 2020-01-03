using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace bS.Sked2.Structure.Engine.Data
{
    public class StringValue : BaseEngineValue
    {
        public override DataType DataType => DataType.String;

        public override string StoragePrefixValue => "!*STRING#";

        public override bool CanPersistInStorage => true;
        public StringValue()
        {

        }
        public StringValue(string value)
        {
            Set(value);
        }

        public override void ReadFromStringValue(string stringValue)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(string));
            StringReader sr = new StringReader(stringValue);
            using (XmlReader writer = XmlReader.Create(sr))
            {
                value = xmlSerializer.Deserialize(writer);
            }
        }
    }
}
