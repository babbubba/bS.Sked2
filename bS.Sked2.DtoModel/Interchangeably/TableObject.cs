using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bS.Sked2.DtoModel.Interchangeably
{
    public class TableObject : InterchangeablyBase<DataTable>
    {
        DataTable _table { get; set; }

        public override DataTable Get()
        {
            ThrowIfNotInit();
            ThrowIfNotValid();
            return _table;
        }

        public override void Init()
        {
            _table = new DataTable("table");
            IsInit = true;
        }

        public override void Set(DataTable value)
        {
            ThrowIfNotInit();
            _table = value;
            IsValid = true;
        }

        public IEnumerable<string> GetHeaders()
        {
            ThrowIfNotInit();
            ThrowIfNotValid();
            return from DataColumn c in _table.Columns
                   select c.ColumnName;
        }

        public IEnumerable<object> GetRow(int rowNumber)
        {
            ThrowIfNotInit();
            ThrowIfNotValid();
            if (_table.Rows.Count > rowNumber) throw new ApplicationException("Specified row number is out of table rows range.");
            foreach (DataRow row in _table.Rows)
            {
                if (_table.Rows.IndexOf(row) == rowNumber)
                { return row.ItemArray; }
            }
            throw new ApplicationException("No row found at specified row number.");
        }

        public int GetRowsCount()
        {
            return _table.Rows.Count;
        }
    }
}
