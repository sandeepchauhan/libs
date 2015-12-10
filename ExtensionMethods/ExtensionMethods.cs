using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Libs.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static long SizeOfObject(this Object obj)
        {
            using (Stream s = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(s, obj);
                return s.Length;
            }
        }

        public static void PrintArray(this int[] arr)
        {
            StringBuilder sb = new StringBuilder("");
            int i = 0;
            while(i < arr.Length)
            {
                sb.Append(arr[i++] + "  ");
            }

            Console.WriteLine(sb.ToString());
        }

        public static List<short> GetOnePositions(this long l)
        {
            List<short> ret = new List<short>();
            short pos = 64;
            long mask = 1;
            while(pos >= 1)
            {
                if ((l & mask) != 0)
                {
                    ret.Add(pos);
                }
                pos--;
                mask = mask << 1;
            }

            ret.Reverse();
            return ret;
        }

        public static string GetBinaryRepresentation(this int num)
        {
            string s = string.Empty;
            int mask = 1;
            int currBit = 1;
            while(currBit <= 32)
            {
                if ((num & mask) != 0)
                {
                    s += '1';
                }
                else
                {
                    s += '0';
                }

                mask = mask << 1;
                currBit++;
            }

            return new string(s.Reverse().ToArray());
        }

        public static string GetBinaryRepresentation(this long num)
        {
            string s = string.Empty;
            int mask = 1;
            int currBit = 1;
            while (currBit <= 64)
            {
                if ((num & mask) != 0)
                {
                    s += '1';
                }
                else
                {
                    s += '0';
                }

                mask = mask << 1;
                currBit++;
            }

            return new string(s.Reverse().ToArray());
        }
    }
}
