using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;

namespace CodingTest
{
    /// <summary>
    /// Class for holding data of each individual record from CSV file.
    /// </summary>
    public class Record
    {
        //List of permutations of yes for patched? value.
        private static string[] patchedTrueValues = new string[13] 
        { "yes", "y", "yeah", "ye", "ys", "1", "one", "on", "o", "true", "tru", "tr", "t" };
        
        private Hostname _hostname;
        private IpAddress _ipAddress;
        private bool _patched; //no = 0, yes = 1
        private OSVersion _osVersion;
        private string _notes;
        private bool _patchNeeded;

        public Record(string[] input)
        {
            //Ensure have valid number of fields seperated by commas
            //Will throw exception if TAB or other such character is used to seperate values in a record.
            if (input.Length == 5)
            {
                _hostname = new Hostname(input[0].ToLower());
                _ipAddress = new IpAddress(input[1]);
                _patched = GetPatched(input[2].ToLower());
                _osVersion = new OSVersion(input[3]);
                _notes = GetNote(input[4]);
                _patchNeeded = IsPatchedOSVersionCheck();
            }
            else
            {
                throw new Exception("ERROR: Unable to create new record type without 5 fields seperated by commas.");
            }
        }
        /// <summary>
        /// Method to check if OS version is 12+ and the record has not been patched.
        /// Will return true only if record is not patched and has OS of 12+.
        /// </summary>
        /// <returns>Bool stipulating if record meets these conditions.</returns>
        private bool IsPatchedOSVersionCheck()
        {
            bool retVal = false;
            if ((!_patched) && (_osVersion.IsVersionValid)) //If not patched and OS is 12+ then true
            {
                retVal = true;
            }
            return retVal;
        }
        /// <summary>
        /// Wraps note value in square brackets if it exists.
        /// </summary>
        /// <param name="inp">String of note value.</param>
        /// <returns>String representing note for printing.</returns>
        private string GetNote(string inp)
        {
            string retVal = "";
            inp = inp.Trim(); //Remove trailing whitespace.
            if (!String.IsNullOrEmpty(inp)) //Check string has value, else return empty string
            {
                retVal = string.Format("[{0}]", inp); //Wrap inp in square brackets.
            }
            return retVal;
        }
        /// <summary>
        /// Returns bool value representing if the record has a patch or not
        /// </summary>
        /// <param name="inp">String representation of Patched?</param>
        /// <returns>Bool representing if the the record is patched</returns>
        private bool GetPatched(string inp)
        {
            bool retVal = false;
            inp = inp.Trim(); //Trim whitespace
            //Uses LINQ to determine if the value of inp is within the patchedTrueValues array
            //which would indicate that the record has a Patched? value equivalent to yes.
            if (patchedTrueValues.Contains(inp)) 
            {
                retVal = true;
            }
            return retVal;
        }
        /// <summary>
        /// Property returning _patchNeeded bool
        /// Used by printing function to determine if record satisfies all crieteria.
        /// </summary>
        public bool PatchRequired
        {
            get
            {
                return _patchNeeded;
            }
            set
            {
                _patchNeeded = value;
            }
        }
        /// <summary>
        /// Returns string value of _ipAddress.
        /// </summary>
        public string IPAddress
        {
            get
            {
                return _ipAddress.ValidValue;
            }
        }
        /// <summary>
        /// Returns string value of _hostname.
        /// </summary>
        public string Hostname
        {
            get
            {
                return _hostname.ValidValue;
            }
        }
        /// <summary>
        /// Override of ToString() method to print details in desired format.
        /// </summary>
        /// <returns>String in desired output format.</returns>
        public override string ToString()
        {
            return string.Format("{0} ({1}), OS version {2} {3}", _hostname.ToString(), _ipAddress.ToString(), _osVersion.ToString(), _notes).Trim();
        }
    }
}
