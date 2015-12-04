using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Libs.Utils
{
    public static class ResourceAccessor
    {
        public static List<string> GetEnglishWords()
        {
            List<string> englishWords = new List<string>();
            string[] lines = File.ReadAllLines(@"D:\gitrepos\myrepos\resources\english-words.txt");
            foreach(string l in lines)
            {
                englishWords.Add(l.Trim());
            }

            return englishWords;
        }
    }
}
