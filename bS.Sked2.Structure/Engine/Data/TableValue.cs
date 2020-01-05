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
        public static DataType DataTypeConst => DataType.Table;


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

        public override void ReadFromStringValue(string stringValue)
        {
            DeserializeValueFromString<DataTable>(stringValue);
        }
    }
}
