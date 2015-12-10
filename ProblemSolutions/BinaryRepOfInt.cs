using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemSolutions
{
    class BinaryRepOfInt
    {
        private static string GetBits(int x)
        {
            string ret = "";
            while (x != 0)
            {
                int rem = x % 2;
                ret = rem + " " + ret;
                x = x / 2;
            }

            return ret;
        }
    }
}
