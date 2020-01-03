using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace bS.Sked2.Structure.Engine.Data
{
    public class CharValue : BaseEngineValue
    {
        public override DataType DataType => DataType.Char;

        public override string StoragePrefixValue => "!*CHAR#";

        public override bool CanPersistInStorage => true;
        public CharValue()
        {

        }
        public CharValue(char value)
        {
            Set(value);
        }

        public override void ReadFromStringValue(string stringValue)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(char));
            StringReader sr = new StringReader(stringValue);

            using (XmlReader writer = XmlReader.Create(sr))
            {
                value = xmlSerializer.Deserialize(writer);
            }
        }
    }
}
