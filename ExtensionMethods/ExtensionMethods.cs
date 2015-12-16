using Learning.Libs.Utils;
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

        // 400 ms
        public static List<short> GetOnePositionsV5(this long l, List<int> list, int baseVal, long[] masks)
        {
            short bitNumber = 1;
            while (bitNumber <= 64)
            {
                if ((l & masks[bitNumber - 1]) != 0)
                {
                    //list.Add(baseVal + bitNumber);
                }

                bitNumber++;
            }

            return null;
        }

        // 60 ms
        public static List<short> GetOnePositionsV4(this long l, List<int> list, int baseVal)
        {
            long[] masks = LongMasks.Instance.Masks;
            //short bitNumber = 1;
            //long mask = masks[bitNumber - 1];
            //while (bitNumber <= 64)
            //{
            //    mask = masks[bitNumber - 1];
            //    if ((l & mask) != 0)
            //    {
            //        //list.Add(baseVal + bitNumber);
            //    }

            //    bitNumber++;
            //}

            return null;
        }
        
        // 23 ms
        public static List<short> GetOnePositionsV3(this long l, List<int> list, int baseVal)
        {
            //long[] masks = LongMasks.Instance.Masks;
            //short bitNumber = 1;
            //long mask = masks[bitNumber - 1];
            //while (bitNumber <= 64)
            //{
            //    mask = masks[bitNumber - 1];
            //    if ((l & mask) != 0)
            //    {
            //        //list.Add(baseVal + bitNumber);
            //    }

            //    bitNumber++;
            //}

            return null;
        }

        public static List<short> GetOnePositionsV2(this long l)
        {
            List<short> ret = new List<short>();
            long[] masks = LongMasks.Instance.Masks;
            short bitNumber = 1;
            while (bitNumber <= 64)
            {
                long mask = masks[bitNumber - 1];
                if ((l & mask) != 0)
                {
                    ret.Add(bitNumber);
                }

                bitNumber++;
            }

            return ret;
        }

        public static List<short> GetOnePositionsV1(this long l)
        {
            List<short> ret = new List<short>();
            var ins = LongMasks.Instance;
            short bitNumber = 1;
            while (bitNumber <= 64)
            {
                long mask = ins.Masks[bitNumber - 1];
                if ((l & mask) != 0)
                {
                    ret.Add(bitNumber);
                }

                bitNumber++;
            }

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
