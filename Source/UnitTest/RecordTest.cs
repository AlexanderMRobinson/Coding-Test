using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodingTest;

namespace UnitTest
{
    [TestClass]
    public class RecordTests
    {
        private Record _rec;
        /// <summary>
        /// Function to trim and split input string so that it is in the form expected.
        /// </summary>
        /// <param name="inp">Input string</param>
        /// <returns>Array of strings</returns>
        public static string[] ArrayifyString(string inp)
        {
            inp = inp.Trim();
            return inp.Split(',');
        }
        /// <summary>
        /// Ensure that constructor doesn't throw array size exception for 
        /// valid input.
        /// </summary>
        [TestMethod]
        public void ValidArrayInputSize()
        {
            string[] inp = ArrayifyString("A.example.COM,1.1.1.1,NO,11,Faulty fans");
            try
            {
                _rec = new Record(inp);
                Assert.IsFalse(false); //Pass if doesn't throw exception
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
        /// <summary>
        /// Ensure constructor throws array size exception for invalid input.
        /// </summary>
        [TestMethod]
        public void InvalidArrayInputSize()
        {
            string[] inp = ArrayifyString("A.example.COM,1.1.1.1,NO,11");
            try
            {
                _rec = new Record(inp);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.IsFalse(false); //Pass if doesn't throw exception
            }
        }
        /// <summary>
        /// Ensure that for a valid input that requires a patch, based on 
        /// Patched? and OS version attributes only, the PatchRequired
        /// property returns True.
        /// </summary>
        [TestMethod]
        public void InputRequiringPatch()
        {
            string[] inp = ArrayifyString("a.example.com,1.1.1.1,NO,12,Faulty fans");
            _rec = new Record(inp);
            Assert.IsTrue(_rec.PatchRequired);
        }
        /// <summary>
        /// Ensure that for an invalid input that has been patched
        /// the PatchRequired property returns False.
        /// </summary>
        [TestMethod]
        public void InputAlreadyPatched()
        {
            string[] inp = ArrayifyString("A.example.com,1.1.1.1,YES,12,Faulty fans");
            _rec = new Record(inp);
            Assert.IsFalse(_rec.PatchRequired);
        }
        /// <summary>
        /// Ensure that for an input that has an OS pre 12 the 
        /// PatchRequired property returns false.
        /// </summary>
        [TestMethod]
        public void InputOSTooOld()
        {
            string[] inp = ArrayifyString("A.example.com,1.1.1.1,NO,5,Faulty fans");
            _rec = new Record(inp);
            Assert.IsFalse(_rec.PatchRequired);
        }
        /// <summary>
        /// Ensure that in the hypothetical situation a router is 
        /// pre 12 and has been patched, the PatchRequired property 
        /// still returns false.
        /// </summary>
        [TestMethod]
        public void InputOldOSAndPatched()
        {
            string[] inp = ArrayifyString("A.example.com,1.1.1.1,YES,5,Faulty fans");
            _rec = new Record(inp);
            Assert.IsFalse(_rec.PatchRequired);
        }
        /// <summary>
        /// Ensure that when a valid IP address is entered, it is displayed 
        /// in the IPAddress property.
        /// Note: Use not equal on "", as empty string is returned for invalid
        /// IP address.
        /// </summary>
        [TestMethod]
        public void ValidIPAddress()
        {
            string[] inp = ArrayifyString("A.example.com,1.1.1.1,YES,5,Faulty fans");
            _rec = new Record(inp);
            Assert.AreNotEqual("",_rec.IPAddress);
        }
        /// <summary>
        /// Ensure that empty string is returned by IPAddress property for 
        /// an IP address with integers outside of valid range (0 => x <= 255)
        /// </summary>
        [TestMethod]
        public void InvalidIPAddress1()
        {
            string[] inp = ArrayifyString("A.example.com,256.1.1.1,YES,5,Faulty fans");
            _rec = new Record(inp);
            Assert.AreEqual("", _rec.IPAddress);
        }
        /// <summary>
        /// Ensure that empty string is returned by IPAddress property for 
        /// an IP address with none valid characters.
        /// Valid chars - 0-9.
        /// </summary>
        [TestMethod]
        public void InvalidIPAddress2()
        {
            string[] inp = ArrayifyString("A.example.com,hi.1.1.1,YES,5,Faulty fans");
            _rec = new Record(inp);
            Assert.AreEqual("", _rec.IPAddress);
        }
        /// <summary>
        /// Ensure the hostname property doesn't return empty string when 
        /// presented with valid hostname.
        /// </summary>
        [TestMethod]
        public void ValidHostname()
        {
            string[] inp = ArrayifyString("A.example.com,1.1.1.1,YES,5,Faulty fans");
            _rec = new Record(inp);
            Assert.AreNotEqual("", _rec.Hostname);
        }
        /// <summary>
        /// Ensure the hostname property returns empty string when 
        /// presented with invalid hostname, one which contains
        /// invalid characters.
        /// Valid characters - a-zA-Z0-9.-
        /// </summary>
        [TestMethod]
        public void InvalidHostname1()
        {
            string[] inp = ArrayifyString("\\.example.com,1.1.1.1,YES,5,Faulty fans");
            _rec = new Record(inp);
            Assert.AreEqual("", _rec.Hostname);
        }
        /// <summary>
        /// Ensure the hostname property returns empty string when 
        /// presented with invalid hostname, one which contains
        /// invalid top level domain.
        /// Valid TLD's - .com, .uk, .org, .net, .info, .biz
        /// </summary>
        [TestMethod]
        public void InvalidHostname2()
        {
            string[] inp = ArrayifyString("a.example.john,1.1.1.1,YES,5,Faulty fans");
            _rec = new Record(inp);
            Assert.AreEqual("", _rec.Hostname);
        }
        /// <summary>
        /// Ensure ToString outputs appropriate string for input without notes
        /// attribute.
        /// </summary>
        [TestMethod]
        public void ToStringNoNotes()
        {
            string[] inp = ArrayifyString("A.example.com,1.1.1.1,YES,12,");
            _rec = new Record(inp);
            Assert.AreEqual("a.example.com (1.1.1.1), OS version 12", _rec.ToString());
        }
        /// <summary>
        /// Ensure ToString outputs appropriate string for input without notes
        /// attribute.
        /// Note - Notes are lower case as entire input string is lower cased
        /// this will be changed if enough time available.
        /// </summary>
        [TestMethod]
        public void ToStringNotes()
        {
            string[] inp = ArrayifyString("A.example.com,1.1.1.1,YES,12,This is a test");
            _rec = new Record(inp);
            Assert.AreEqual("a.example.com (1.1.1.1), OS version 12 [This is a test]", _rec.ToString());
        }
    }
}
