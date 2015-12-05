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
    }
}
