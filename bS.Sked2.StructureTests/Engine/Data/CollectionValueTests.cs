using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine.Data.Tests
{
    [TestClass()]
    public class CollectionValueTests
    {
        [TestMethod()]
        public void TestDataValues()
        {

            var collection = new CollectionValue();
            collection.AddValue(new BoolValue(true));
            collection.AddValue(new BoolValue(false));
            collection.AddValue(new CharValue('a'));
            collection.AddValue(new CharValue('b'));
            collection.AddValue(new DateTimeValue(DateTime.Now));
            collection.AddValue(new DecimalValue(12.345m));
            collection.AddValue(new DoubleValue(123.45678901));
            collection.AddValue(new IntValue(1234));
            collection.AddValue(new StringValue("abcdef"));
            collection.AddValue(new DictionaryEntryValue("chiave","valore"));

            var str = collection.WriteToStringValue();

            var collection2 = new CollectionValue();

            collection2.ReadFromStringValue(str);

          
        }
    }
}