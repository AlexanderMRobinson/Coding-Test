using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTest
{
    public class Hostname : ValidBase
    {
        //String array of valid TLD's
        private static string[] validTLD = new string[6] { "com", "uk", "org", "net", "info", "biz" };
        /// <summary>
        /// Constructor for hostname class.
        /// </summary>
        /// <param name="input">String of hostname (e.g. a.example.com)</param>
        public Hostname(string input) : base(input)
        {
            input = input.Trim(); //Trim whitespace
            //Check hostname has valid characters, valid TLD and has valid length
            //Ensures hostname conforms to RFC 1123
            //Sets base classes isValid bool value accordingly
            IsValid = (HasValidChars(input, true) &&
                       HasValidTLD(input) &&
                       (input.Length <= 253));
            //Set base classes string value based on validity.
            ValidValue = IsValid ? input : "";
        }
        /// <summary>
        /// Checks to determine if top level domain of input data is valid.
        /// Currently only accepted TLD's are: .com, .uk, .org, .net, .info, .biz 
        /// </summary>
        /// <param name="inp">Input string</param>
        /// <returns>Boolean value stipulatingif input satisfies criteria.</returns>
        private bool HasValidTLD(string inp)
        {
            bool retVal = false;
            string[] temp = inp.Split('.'); //Split hostname 
            int len = temp.Length; 
            if (len > 0)
            {
                //Take last tag from hostname, which will be TLD
                //Check to see if the TLD is within validTLD string array.
                if (validTLD.Contains(temp[len - 1]))
                {
                    retVal = true;
                }
            }
            return retVal;
        }
    }
}
