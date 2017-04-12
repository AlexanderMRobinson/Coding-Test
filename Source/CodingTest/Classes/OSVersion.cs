using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTest
{
    public class OSVersion : ValidBase
    {
        //Boolean representing if the OSVersion has been evaluated to being 12 or above
        private bool _validVersion;
        /// <summary>
        /// Constructor for OSVersion class
        /// </summary>
        /// <param name="input">String representing OS version (e.g. 12.23)</param>
        public OSVersion(string input) : base(input)
        {
            input = input.Trim(); //Trim whitespace
            //Ensure input has valid characters using base class method
            //Sets base classes bool based on this
            IsValid = (HasValidChars(input, false)); 
            if (IsValid) //Check previously set base class boolean
            {
                ValidValue = input; //Set base class value property based on validity
                //Check version integer and determione if it is 12 or above
                _validVersion = (GetInteger(input) >= 12) ? true : false; 
            }
            else
            {
                //Set value to empty as invalid chars
                ValidValue = "";
            }
        }
        /// <summary>
        /// Method to retrieve the first integer from the OS version string
        /// The first integer is what determines if it is valid OS (i.e. 12 or above)
        /// </summary>
        /// <param name="inp">OS Version string</param>
        /// <returns>First integer of string or 0</returns>
        private int GetInteger(string inp)
        {
            int retVal = 0;
            string[] temp = inp.Split('.'); //Split string on period
            Int32.TryParse(temp[0], out retVal); //Try to parse the first member of array to int
            return retVal;//Return the integer or 0 if parse fails.
        }
        /// <summary>
        /// Property returning the _validVersion bool representing if OS is 12+
        /// </summary>
        public bool IsVersionValid
        {
            get
            {
                return _validVersion;
            }
        }
    }
}
