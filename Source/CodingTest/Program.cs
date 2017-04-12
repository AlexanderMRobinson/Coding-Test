using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace CodingTest
{
    class Program
    {
        /// <summary>
        /// Method to select and print all records that have a PatchRequired property set
        /// to TRUE, which indicates that they have passed all checks.
        /// </summary>
        /// <param name="recs">List of all records.</param>
        private static void printValidRecords(List<Record> recs)
        {
            recs.RemoveAll(x => x.PatchRequired == false); //Remove all records from list with FALSE PatchRequired properties.
            int len = recs.Count;
            for (int i = 0; i < len; ++i) //Iterate over remaining records and print to screen.
            {
                if (i == (len - 1))
                {
                    Console.Write(recs[i].ToString()); //Necessary to remove additional new line after last record is output.
                }
                else
                {
                    Console.WriteLine(recs[i].ToString());
                }
            }
        }
        /// <summary>
        /// Main function of the program.
        /// </summary>
        /// <param name="args">Only argument is path of CSV file to parse.</param>
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                string inputFile = args[0];
                if (File.Exists(inputFile)) //Checks that the file at the path exists
                {
                    if (Path.GetExtension(inputFile) == ".csv")
                    {
                        List<Record> recordList = new List<Record>();
                        DictionaryWrapper ipdw = new DictionaryWrapper();
                        DictionaryWrapper hndw = new DictionaryWrapper();
                        Record rec;
                        string line;
                        string[] temp;
                        bool print = true;
                        StreamReader reader = new StreamReader(inputFile);
                        while ((line = reader.ReadLine()) != null)
                        {
                            line = line.Trim();
                            if(!String.IsNullOrEmpty(line))
                            {
                                
                                temp = line.Split(','); //Split on comma as the file is a comma seperated value file.
                                try
                                {
                                    rec = new Record(temp); //Record instanc created
                                    recordList.Add(rec); //Valid record added to list of all records.
                                    ipdw.Add(rec.IPAddress, rec); //Add to IP address dictionary, which if there is the same ip present will change record classes PatchRequired property to false for both
                                    hndw.Add(rec.Hostname, rec); //Add to hostname dictionary, which performs same action.
                                }
                                catch (Exception e) 
                                {
                                    Console.WriteLine(e.Message); 
                                    Console.WriteLine("Exiting gracefully.");
                                    print = false; //Switch off printing of records as exception is thrown
                                    break;
                                }
                            }
                        }
                        reader.Close();
                        if (print)
                        {
                            printValidRecords(recordList); //Print output of valid records.
                        }
                    }
                    else
                    {
                        Console.Write("Please ensure you are passing a CSV file.");
                    }
                }
                else
                {
                    Console.Write("File {0} does not exist", inputFile);
                }
            }
            else
            {
                Console.Write("Please ensure you pass the CSV file as a parameter.");
            }
        }
    }
}
