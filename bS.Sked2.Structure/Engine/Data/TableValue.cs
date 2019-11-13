using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace bS.Sked2.Structure.Engine.Data
{
    public class TableValue : IEngineData
    {
        DataTable value;
        public TableValue()
        {
            value = new DataTable("Table");
        }
        public TableValue(DataTable table)
        {
           // value = new DataTable("Table");
            value = table;
        }

        public string DataType => "Table";

        public bool CanPersistInStorage => false;
        public bool IsFilled { get; set; }
        public bool IsValid { get; set; }

        public object Get()  
        {
            return value;
        }

        public void Set(object value)
        {
            this.value = value as DataTable;
            if (this.value == null)
            {
                IsValid = false;
                IsFilled = true;
            }
        }
    }
}
