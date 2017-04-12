using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTest
{
    /// <summary>
    /// Class that wraps a dictionary of string and Record and provides functionality
    /// to Add to dict if a key is not already present, elsewise the 'Added' and the 
    /// existing Record instances PatchRequired properties are set to false to represent 
    /// two Records with the same hostname or IP address being present within the data set.
    /// -----------------------------------------------------------------------------------
    /// This method works as .NET passes object types by reference, meaning the dictionary 
    /// contains references to the original object instead of new instances each time.
    /// </summary>
    public class DictionaryWrapper
    {
        private Dictionary<string, Record> _dictionary;
        public DictionaryWrapper()
        {
            _dictionary = new Dictionary<string, Record>();
        }
        /// <summary>
        /// Function to add to the dictionary or set PatchRequired 
        /// property to false.
        /// </summary>
        /// <param name="key">Key value (IP address or hostname)</param>
        /// <param name="val">Record instance associated with that key.</param>
        public void Add(string key, Record val)
        {
            if (key != string.Empty) //Don't consider invalid values with empty properties.
            {
                Record current = GetKeyValue(key); //Attempt to retrieve value from dictionary with provided key
                //If no record is associated then add the record to the dictionary
                if (current == null) 
                {
                    _dictionary.Add(key, val);
                }
                //Elsewise set both patchrequired values to false (so neither are printed) 
                else
                {
                    val.PatchRequired = false;
                    current.PatchRequired = false;
                }
            }
        }
        /// <summary>
        /// Method to attempt to retieve a Value from dictionary
        /// based on a provided key. Returns null if no value is
        /// found.
        /// </summary>
        /// <param name="key">Key to find in dictionary</param>
        /// <returns>Null or valid record.</returns>
        private Record GetKeyValue(string key)
        {
            Record retVal = null;
            _dictionary.TryGetValue(key, out retVal);
            return retVal;
        }

    }
}
