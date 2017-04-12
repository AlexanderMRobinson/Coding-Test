using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodingTest;
namespace UnitTest
{
    [TestClass]
    public class HostnameTest
    {
        private Hostname _hn;
        /// <summary>
        /// Ensure that given a valid input (has valid chars and TLD)
        /// the IsValid property of associated ValidBase is set to True.
        /// </summary>
        [TestMethod]
        public void Hostname1()
        {
            _hn = new Hostname("maps.google.com");
            Assert.IsTrue(_hn.IsValid);
        }

        /// <summary>
        /// Ensure that given an invalid input (bad char - #) the
        /// IsValid property of associated ValidBase is set to False.
        /// </summary>
        [TestMethod]
        public void Hostname2()
        {
            _hn = new Hostname("m#aps.google.com");
            Assert.IsFalse(_hn.IsValid);
        }

        /// <summary>
        /// Ensure that given an invalid input (bad TLD - .fail) the
        /// IsValid property of associated ValidBase is set to False.
        /// </summary>
        [TestMethod]
        public void Hostname3()
        {
            _hn = new Hostname("maps.google.fail");
            Assert.IsFalse(_hn.IsValid);
        }

        /// <summary>
        /// Ensure that given an invalid input (bad TLD & bad chars) the
        /// IsValid property of associated ValidBase is set to False.
        /// </summary>
        [TestMethod]
        public void Hostname4()
        {
            _hn = new Hostname("ma#ps.google.fail");
            Assert.IsFalse(_hn.IsValid);
        }
    }
}
