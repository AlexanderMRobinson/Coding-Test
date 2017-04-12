using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodingTest;
namespace UnitTest
{
    [TestClass]
    public class IPAddressTest
    {
        private IpAddress _ip;
        /// <summary>
        /// Ensure that IsValid property is True for valid
        /// IPV4 address (localhost)
        /// </summary>
        [TestMethod]
        public void IPAddress1()
        {
            _ip = new IpAddress("127.0.0.1");
            Assert.IsTrue(_ip.IsValid);
        }
        /// <summary>
        /// Ensure that the IsValid propert is set to False
        /// for invalid input (Uses commas as aseperators 
        /// and has alphanumeric chars)
        /// </summary>
        [TestMethod]
        public void IPAddress2()
        {
            _ip = new IpAddress("1alex,0,0,1");
            Assert.IsFalse(_ip.IsValid);
        }
        /// <summary>
        /// Ensure that IsVlaid property is False for input 
        /// that doesn't conform to IPV4 standard of 4 bytes
        /// (has 5 bytes)
        /// </summary>
        [TestMethod]
        public void IPAddress3()
        {
            _ip = new IpAddress("127.0.0.1.12");
            Assert.IsFalse(_ip.IsValid);
        }
        /// <summary>
        /// Ensure that each byte in the address has a value 
        /// appropriate for a byte (0-255, instead of 300 as
        /// is in the test).
        /// </summary>
        [TestMethod]
        public void IPAddress4()
        {
            _ip = new IpAddress("300.0.0.1");
            Assert.IsFalse(_ip.IsValid);
        }
    }
}
