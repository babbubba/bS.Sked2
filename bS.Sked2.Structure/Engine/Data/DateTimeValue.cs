using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace bS.Sked2.Structure.Engine.Data
{
    public class DateTimeValue : BaseEngineValue
    {
        public override DataType DataType => DataType.Datetime;

        public override string StoragePrefixValue => "!*DATETIME#";

        public override bool CanPersistInStorage => true;
        public DateTimeValue()
        {

        }
        public DateTimeValue(DateTime value)
        {
            Set(value);
        }

        public override void ReadFromStringValue(string stringValue)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(DateTime));
            StringReader sr = new StringReader(stringValue);

            using (XmlReader writer = XmlReader.Create(sr))
            {
                value = xmlSerializer.Deserialize(writer);
            }
        }
    }
}
