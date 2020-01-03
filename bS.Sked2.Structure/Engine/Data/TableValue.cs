using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace bS.Sked2.Structure.Engine.Data
{
    /// <summary>
    /// Rapresent a Table Value
    /// </summary>
    /// <seealso cref="bS.Sked2.Structure.Engine.Data.BaseEngineValue" />
    public class TableValue : BaseEngineValue
    {
        public override DataType DataType => DataType.Table;

        public override string StoragePrefixValue => "!*TABLE#";

        public override bool CanPersistInStorage => true;

        public TableValue()
        {

        }
        public TableValue(DataTable table)
        {
            Set(table);
        }

        /// <summary>
        /// Gets the row count.
        /// </summary>
        /// <value>
        /// The row count.
        /// </value>
        public int RowCount
        {
            get
            {
                var val = value as DataTable;
                return val?.Rows?.Count ?? 0;
            }
        }

        //public override string WriteToStringValue()
        //{
        //    var sb = new StringBuilder();
        //    sb.Append(StoragePrefixValue);
        //    using (var sw = new StringWriter())
        //    {
        //        ((DataTable)value).WriteXml(sw);
        //        sb.Append(sw.ToString());
        //    }
        //    return sb.ToString();
        //}

        //public override void ReadFromStringValue(string stringValue)
        //{
        //    base.ReadFromStringValue(stringValue);
        //    string val = stringValue.Remove(0, StoragePrefixValue.Length);
        //    var sw = new StringReader(val);
        //    ((DataTable)value).ReadXml(sw);
        //}
        public override void ReadFromStringValue(string stringValue)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(DataTable));
            StringReader sr = new StringReader(stringValue);
            using (XmlReader writer = XmlReader.Create(sr))
            {
                value = xmlSerializer.Deserialize(writer);
            }
        }
    }
}
