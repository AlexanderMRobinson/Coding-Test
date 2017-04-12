using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodingTest;

namespace UnitTest
{
    [TestClass]
    public class OSVersionTest
    {
        private OSVersion _osv;
        /// <summary>
        /// Ensure that the IsValid property is set to true for 
        /// input with valid chars (i.e. [0-9.]+).
        /// </summary>
        [TestMethod]
        public void OSVersion1()
        {
            _osv = new OSVersion("17.9");
            Assert.IsTrue(_osv.IsValid);
        }
        /// <summary>
        /// Ensure that the IsValid property is set to false for 
        /// input with invalid chars.
        /// </summary>
        [TestMethod]
        public void OSVersion2()
        {
            _osv = new OSVersion("hello");
            Assert.IsFalse(_osv.IsValid);
        }
        /// <summary>
        /// Ensure that the IsValidVersion property returns True
        /// when an OS version >= 12 is input.
        /// </summary>
        [TestMethod]
        public void OSVersion3()
        {
            _osv = new OSVersion("17.9");
            Assert.IsTrue(_osv.IsVersionValid);
        }
        /// <summary>
        /// Ensure that the IsValidVersion property returns False
        /// when an OS version < 12 is input
        /// </summary>
        [TestMethod]
        public void OSVersion4()
        {
            _osv = new OSVersion("11.9");
            Assert.IsFalse(_osv.IsVersionValid);
        }
    }
}
