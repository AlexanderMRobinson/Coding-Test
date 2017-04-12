using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSVGenerator
{
    public class strboostruct
    {
        public strboostruct(bool use, string val)
        {
            Valid = use;
            Value = val;
        }
        public bool Valid;
        public string Value;
    }
    class Program
    {
        private static Random _rand = new Random();

        private static char[] _alphabet = new char[37] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
                                                        'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
                                                        'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3',
                                                        '4', '5', '6', '7', '8', '9', '-'};
        private static string[] validTLD = new string[6] { "com", "uk", "org", "net", "info", "biz" };
        private static bool RandomBool()
        {
            return (_rand.Next(0, 100) >= 50) ? false : true;
        }
        private static List<strboostruct> GenerateNHostOrIP(int n, Func<string> fn)
        {
            List<strboostruct> retVal = new List<strboostruct>();
            strboostruct temp;
            bool useOld;
            for (int i = 0; i < n; ++i)
            {
                useOld = RandomBool();
                if (useOld && (retVal.Count > 0))
                {
                    temp = retVal[_rand.Next(retVal.Count)];
                    temp.Valid = true;
                    retVal.Add(new strboostruct(temp.Valid, temp.Value));
                }
                else
                {
                    retVal.Add(new strboostruct(false, fn.Invoke()));
                }
            }
            return retVal;
        }
        private static List<strboostruct> GenerateNPatched(int n)
        {
            List<strboostruct> retVal = new List<strboostruct>();
            bool isPatched;
            string val;
            for (int i = 0; i < n; ++i)
            {
                isPatched = RandomBool();
                if (isPatched)
                {
                    val = "Yes";
                }
                else
                {
                    val = "No";
                }
                retVal.Add(new strboostruct(isPatched,val));
                val = "";
            }
            return retVal;
        }
        private static List<strboostruct> GenerateNOSVersions(int n)
        {
            List<strboostruct> retVal = new List<strboostruct>();
            int vers;
            string val;
            for (int i = 0; i < n; ++i)
            {
                vers = _rand.Next(0, 20);
                val = osVersionGenerator(vers);
                retVal.Add(new strboostruct((vers < 12), val));
            }
            return retVal;
        }
        private static string osVersionGenerator(int vers)
        {
            return string.Format("{0}.{1}", vers, _rand.Next(0, 1000));
        }
        private static string ipAddressGenerator()
        {
            return string.Format("{0}.{1}.{2}.{3}", _rand.Next(0, 255), _rand.Next(0, 255), _rand.Next(0, 255), _rand.Next(0, 255));
        }
        private static string hostnameGenerator()
        {
            string output = "";
            int tagcount = _rand.Next(2, 5);
            int ran;
            string temp = "";
            for (int i = 0; i < tagcount; ++i)
            {
                ran = _rand.Next(4, 20);
                for (int i2 = 0; i2 < ran; ++i2)
                {
                    temp += _alphabet[_rand.Next(37)];
                }
                temp = temp + ".";
                output += temp;
                temp = "";
            }
            output = output + validTLD[_rand.Next(6)];
            return output;
        }
        static void Main(string[] args)
        {
            if (args.Length == 2) //Number of records and name of file to create
            {
                int count;
                string file = args[1];
                string outVal;
                if (Int32.TryParse(args[0], out count))
                {
                    List<strboostruct> hosts = GenerateNHostOrIP(count,hostnameGenerator);
                    List<strboostruct> ip = GenerateNHostOrIP(count,ipAddressGenerator);
                    List<strboostruct> ispatched = GenerateNPatched(count);
                    List<strboostruct> os = GenerateNOSVersions(count);
                    StreamWriter sw = new StreamWriter(file);
                    for(int i = 0; i < count; ++i)
                    {
                        outVal = string.Format("{0},{1},{2},{3},", hosts[i].Value, ip[i].Value, ispatched[i].Value, os[i].Value);
                        if ((!hosts[i].Valid) && (!ip[i].Valid) && (!ispatched[i].Valid) && (!os[i].Valid))
                        {
                            Console.WriteLine(string.Format("{0} ({1}), OS Version {2}", hosts[i].Value, ip[i].Value, os[i].Value));
                        }
                        sw.WriteLine(outVal);
                    }
                    sw.Close();
                }
                else
                {
                    Console.WriteLine("Unable to parse count input.");
                }
            }
            else
            {
                Console.WriteLine("Please pass number of records and output file name as arguments.");
            }
        }
    }
}
