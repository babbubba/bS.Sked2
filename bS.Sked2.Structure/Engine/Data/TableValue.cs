using System.Collections.Generic;
using System.Data;
using System.Text;

namespace bS.Sked2.Structure.Engine.Data
{
    /// <summary>
    /// Rapresent a Table Value
    /// </summary>
    /// <seealso cref="bS.Sked2.Structure.Engine.Data.BaseEngineValue" />
    public class TableValue : BaseEngineValue
    {

        public TableValue()
        {
            CanPersistInStorage = false;
            dataType = DataType.Table;
            value = new DataTable("Table");
        }

        public TableValue(DataTable table)
        {
            CanPersistInStorage = false;
            dataType = DataType.Table;
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
    }
}
