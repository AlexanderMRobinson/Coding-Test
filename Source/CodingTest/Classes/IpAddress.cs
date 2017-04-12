using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTest
{
    public class IpAddress : ValidBase
    {
        /// <summary>
        /// Constructor of IpAddress class.
        /// </summary>
        /// <param name="input">String of IPV4 address (e.g. 123.124.245.10)</param>
        public IpAddress(string input) : base(input)
        {
            input = input.Trim(); //Trim whitespace
            //Check input is only numeric characters and period (.) and values in ip address
            //are in range 0 - 255 in line with IPV4 spec.
            //Sets base classes isValid bool value accordingly
            IsValid = (HasValidChars(input, false) && 
                       HasValidAddressBytes(input));
            //Set base classes string value based on validity.
            ValidValue = IsValid ? input : ""; 
        }
        /// <summary>
        /// Checks input strings integer values are between 0 and 255
        /// </summary>
        /// <param name="inp">IP address string</param>
        /// <returns>Bool representing validity.</returns>
        private bool HasValidAddressBytes(string inp)
        {
            bool retVal = true;
            string[] sInp = inp.Split('.'); //Split the string on period
            if (sInp.Length == 4) //Ensure the IP address only has 4 integer values
            {
                int convert = 0;
                foreach (string s in sInp) //Iterate over each string in array
                {
                    if (Int32.TryParse(s, out convert)) //Attempt to convert it to integer
                    {
                        //If conversion was successful ensure is in correct range.
                        //Elsewise set retVal to false and return.
                        if (!(convert >= 0 && convert <= 255)) 
                        {
                            retVal = false;
                            break;
                        }
                    }
                    else
                    {
                        retVal = false;
                        break;
                    }
                }
            }
            else
            {
                retVal = false;
            }
            return retVal;
        }
    }
}
