using bS.Sked2.Structure.Engine.Data.Types;
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
            collection.AddValue(new BoolValue());
            collection.AddValue(new CharValue('a'));
            collection.AddValue(new CharValue('b'));
            collection.AddValue(new CharValue());
            collection.AddValue(new DateTimeValue(DateTime.Now));
            collection.AddValue(new DateTimeValue());
            collection.AddValue(new DecimalValue(12.345m));
            collection.AddValue(new DoubleValue(123.45678901));
            collection.AddValue(new DoubleValue());
            collection.AddValue(new IntValue(1234));
            collection.AddValue(new IntValue());
            collection.AddValue(new StringValue("abcdef"));
            collection.AddValue(new StringValue());
            collection.AddValue(new DictionaryEntryValue("chiave", "valore"));
            collection.AddValue(new DictionaryEntryValue());
            collection.AddValue(new VirtualPathValue(new VirtualPath(@"\file.txt")));
            collection.AddValue(new VirtualPathValue(new VirtualPath()));

            var collectionSTR = collection.WriteToStringValue();
            var collection_bis = new CollectionValue();
            collection_bis.ReadFromStringValue(collectionSTR);

            var emptyCoolection = new CollectionValue();
            var emptyCoolectionSTR = emptyCoolection.WriteToStringValue();
            var emptyCoolection_bis = new CollectionValue();
            emptyCoolection_bis.ReadFromStringValue(emptyCoolectionSTR);

            //var ssss = new StringValue();
            //var sxxxxx =ssss.WriteToStringValue();
            //var sssss = new StringValue();
            //sssss.ReadFromStringValue(sxxxxx);

            //var s123 = new StringValue("abcdef");
            //var s123STR = s123.WriteToStringValue();
            //var s123_bis = new StringValue();
            //s123_bis.ReadFromStringValue(s123STR);

        }
    }
}