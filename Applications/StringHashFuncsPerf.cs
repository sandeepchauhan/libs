using ApplicationModels;
using Learning.Libs.DataStructures;
using Learning.Libs.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications
{
    public static class StringHashFuncsPerf
    {
        public static List<StringHashFuncPerfModel> GetPerformanceOfStringHashingAlgos()
        {
            List<StringHashFuncPerfModel> ret = new List<StringHashFuncPerfModel>();
            List<string> englishWords = ResourceAccessor.GetEnglishWords();
            LinkedList<string>[] hashTable = new LinkedList<string>[Int32.MaxValue];
            foreach(string w in englishWords)
            {
                ArrayString w1 = new ArrayString(w);
                Int32 i = w1.GetHashCode();

            }

            return ret;
        }
    }
}
