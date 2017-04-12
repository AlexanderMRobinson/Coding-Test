using System;
using System.Linq;
using System.Collections.Generic;

namespace CodingTest
{
    public abstract class ValidBase
    {
        //Static char arrays for use with validity checks.
        //Static such that only one instance is created for all instancesof the class.
        private static char[] AlphaNumeric = new char[38] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
                                                        'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
                                                        'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3',
                                                        '4', '5', '6', '7', '8', '9', '-', '.' };
        private static char[] Numeric = new char[11] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.' };

        //Base class variables private access as properties are provided for modification
        private string _validValue;
        private bool _isValid;
        private string _original;

        public ValidBase(string input)
        {
            _original = input;
        }

        /// <summary>
        /// Property for accessing and modifying _isValid variable
        /// Set is protected so only child class can modify.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            protected set
            {
                _isValid = value;
            }
        }

        /// <summary>
        /// Property for getting and modifying _validValue.
        /// </summary>
        public string ValidValue
        {
            get
            {
                return _validValue;
            }
            protected set
            {
                _validValue = value;
            }
        }

        /// <summary>
        /// Function for determining if an input string contains valid chars based on 
        /// either the AlphaNumeric or Numeric char arrays.
        /// </summary>
        /// <param name="inp">String to be valuated</param>
        /// <param name="isAlphaNumeric">Whether the char array to be used is Alphanumeric or not</param>
        /// <returns></returns>
        protected bool HasValidChars(string inp, bool isAlphaNumeric)
        {
            //Create local reference to char array based on input bool
            char[] alpha = isAlphaNumeric ? AlphaNumeric : Numeric; 

            bool retVal = true;

            if (inp != string.Empty) //Ensure input string has value
            {
                foreach (char c in inp) 
                {
                    //Ensure each character in inp is within relevant char array.
                    //Return false if the value is not in relevant array.
                    if (!alpha.Contains(c)) 
                    {
                        retVal = false;
                        break;
                    }
                }
            }
            else
            {
                retVal = false; //Return false as inp has no value.
            }
            return retVal;
        }

        /// <summary>
        /// Method to return _validValue by overriding ToString() method.
        /// </summary>
        /// <returns>String representation of child classes relevant value.</returns>
        public override string ToString()
        {
            return _original;
        }
    }
}
