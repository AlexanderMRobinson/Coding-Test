using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodingTest;
namespace UnitTest
{
    [TestClass]
    public class DictionaryWrapperTest
    {
        private DictionaryWrapper _dw;
        /// <summary>
        /// Ensure that a valid record that has a hostname not been seen previously 
        /// doesn't have its PatchRequired property set to false by the DictionaryWrapper. 
        /// </summary>
        [TestMethod]
        public void AddNewKeyRecord1()
        {
            _dw = new DictionaryWrapper();
            Record rec = new Record(
                RecordTests.ArrayifyString("A.example.COM,1.1.1.1,NO,12,Faulty fans"));
            _dw.Add(rec.Hostname, rec);
            Assert.IsTrue(rec.PatchRequired);
        }
        /// <summary>
        /// Ensure that a valid record that has an IP not been seen previously 
        /// doesn't have its PatchRequired property set to false by the DictionaryWrapper. 
        /// </summary>
        [TestMethod]
        public void AddNewKeyRecord2()
        {
            _dw = new DictionaryWrapper();
            Record rec = new Record(
                RecordTests.ArrayifyString("A.example.COM,1.1.1.1,NO,12,Faulty fans"));
            _dw.Add(rec.IPAddress, rec);
            Assert.IsTrue(rec.PatchRequired);
        }
        /// <summary>
        /// Ensure that given two records with the same hostname (note system is case
        /// insensitive there A == a), the dictionary wrapper will modify both of 
        /// their PatchRequired properties to false.
        /// </summary>
        [TestMethod]
        public void AddNewKeyRecord3()
        {
            _dw = new DictionaryWrapper();
            Record rec = new Record(
                RecordTests.ArrayifyString("A.example.COM,2.1.1.1,NO,12,Faulty fans"));
            Record rec1 = new Record(
                RecordTests.ArrayifyString("a.example.COM,1.1.1.1,NO,12,"));
            _dw.Add(rec.Hostname, rec);
            _dw.Add(rec1.Hostname, rec1);
            Assert.IsFalse((rec.PatchRequired) && (rec1.PatchRequired));
        }
        /// <summary>
        /// Ensure that given two records with the same IP address, the dictionary 
        /// wrapper will modify both of their PatchRequired properties to false.
        /// </summary>
        [TestMethod]
        public void AddNewKeyRecord4()
        {
            _dw = new DictionaryWrapper();
            Record rec = new Record(
                RecordTests.ArrayifyString("A.example.COM,1.1.1.1,NO,12,Faulty fans"));
            Record rec1 = new Record(
                RecordTests.ArrayifyString("b.example.COM,1.1.1.1,NO,12,"));
            _dw.Add(rec.IPAddress, rec);
            _dw.Add(rec1.IPAddress, rec1);
            Assert.IsFalse((rec.PatchRequired) && (rec1.PatchRequired));
        }
        /// <summary>
        /// Ensure that given two records with different hostname the 
        /// dictionary wrapper will not modify either of their PatchRequired 
        /// properties.
        /// </summary>
        [TestMethod]
        public void AddNewKeyRecord5()
        {
            _dw = new DictionaryWrapper();
            Record rec = new Record(
                RecordTests.ArrayifyString("A.example.COM,1.1.1.1,NO,12,Faulty fans"));
            Record rec1 = new Record(
                RecordTests.ArrayifyString("b.example.COM,1.1.1.1,NO,12,"));
            _dw.Add(rec.Hostname, rec);
            _dw.Add(rec1.Hostname, rec1);
            Assert.IsTrue((rec.PatchRequired) && (rec1.PatchRequired));
        }
        /// <summary>
        /// Ensure that given two records with differing IP address, the dictionary 
        /// wrapper will not change their PatchRequired property.
        /// </summary>
        [TestMethod]
        public void AddNewKeyRecord6()
        {
            _dw = new DictionaryWrapper();
            Record rec = new Record(
                RecordTests.ArrayifyString("A.example.COM,1.1.1.1,NO,12,Faulty fans"));
            Record rec1 = new Record(
                RecordTests.ArrayifyString("b.example.COM,2.1.1.1,NO,12,"));
            _dw.Add(rec.IPAddress, rec);
            _dw.Add(rec1.IPAddress, rec1);
            Assert.IsTrue((rec.PatchRequired) && (rec1.PatchRequired));
        }
        /// <summary>
        /// Ensure that given multiple records with the same IP, the dictionary
        /// wrapper will set all records to false.
        /// </summary>
        [TestMethod]
        public void AddNewKeyRecord7()
        {
            _dw = new DictionaryWrapper();
            Record rec = new Record(
                RecordTests.ArrayifyString("A.example.COM,2.1.1.1,NO,12,Faulty fans"));
            Record rec1 = new Record(
                RecordTests.ArrayifyString("b.example.COM,2.1.1.1,NO,12,"));
            Record rec2 = new Record(
                RecordTests.ArrayifyString("b.example.COM,2.1.1.1,NO,12,"));
            _dw.Add(rec.IPAddress, rec);
            _dw.Add(rec1.IPAddress, rec1);
            _dw.Add(rec2.IPAddress, rec2);
            Assert.IsFalse((rec.PatchRequired) && (rec1.PatchRequired) && (rec2.PatchRequired));
        }
    }
}
