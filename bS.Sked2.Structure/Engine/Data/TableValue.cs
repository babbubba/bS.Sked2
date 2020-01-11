using System.Data;

namespace bS.Sked2.Structure.Engine.Data
{
    /// <summary>
    /// The table EngineData value
    /// </summary>
    /// <seealso cref="bS.Sked2.Structure.Engine.Data.BaseEngineValue" />
    public class TableValue : BaseEngineValue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableValue"/> class.
        /// </summary>
        public TableValue()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TableValue"/> class.
        /// </summary>
        /// <param name="table">The table.</param>
        public TableValue(DataTable table)
        {
            Set(table);
        }

        /// <summary>
        /// Gets the data type constant.
        /// </summary>
        /// <value>
        /// The data type constant.
        /// </value>
        public static DataType DataTypeConst => DataType.Table;

        /// <summary>
        /// Gets or sets a value indicating whether this instance can persist in storage.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can persist in storage; otherwise, <c>false</c>.
        /// </value>
        public override bool CanPersistInStorage => true;

        /// <summary>
        /// Gets the type of the data.
        /// </summary>
        /// <value>
        /// The type of the data.
        /// </value>
        public override DataType DataType => DataType.Table;

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

        /// <summary>
        /// Reads from string the converted value so it can be read from database.
        /// </summary>
        /// <param name="stringValue">The string value.</param>
        public override void ReadFromStringValue(string stringValue)
        {
            DeserializeValueFromString<DataTable>(stringValue);
        }
    }
}