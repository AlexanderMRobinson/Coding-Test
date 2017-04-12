using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodingTest;
namespace UnitTest
{
    [TestClass]
    public class ValidBaseTest : ValidBase
    {

        public ValidBaseTest()
            : base("")
        { }
        /// <summary>
        /// Ensure HasValidChars method returns true when presented with
        /// a valid alphanumerric string.
        /// </summary>
        [TestMethod]
        public void ValidAlphaNumericChars()
        {

            Assert.IsTrue(HasValidChars("ajsdhjhdbj231235443bsjhbcjhb", true));            
        }
        /// <summary>
        /// Ensure HasValidChars method returns false when presented with
        /// an invalid alphanumerric string.
        /// </summary>
        [TestMethod]
        public void InvalidAlphaNumericChars()
        {
            Assert.IsFalse(HasValidChars("\\!£$%^@~{}:?>><)(**:-)(}-:", true));
        }
        /// <summary>
        /// Ensure HasValidChars method returns true when presented with
        /// a valid numeric string.
        /// </summary>
        [TestMethod]
        public void ValidNumericChars()
        {
            Assert.IsTrue(HasValidChars("1234.56789", false));
        }
        /// <summary>
        /// Ensure HasValidChars method returns true when presented with
        /// an invalid numerric string.
        /// </summary>
        [TestMethod]
        public void InvalidNumericChars()
        {
            Assert.IsFalse(HasValidChars("thisshouldfail-asithasinvalidchars.", false));
        }
    }
}
