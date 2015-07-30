using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryConsoleApp
{
    class DictionarySanitizer
    {
        private static Dictionary<string, string> dict = new Dictionary<string, string>();

        public static void WriteSanitizedDictionary()
        {
            if (File.Exists(@"D:\gitrepos\myrepos\resources\EnglishDictionary-Sanitized.txt"))
            {
                string content = File.ReadAllText(@"D:\gitrepos\myrepos\resources\EnglishDictionary.txt");
                string[] words = content.Split(null);
                bool lookingForWord = true;
                bool lookingForMeaning = false;
                bool wordInProgress = false;
                bool meaningInProgress = false;
                string currWord = string.Empty;
                string currMeaning = string.Empty;
                foreach (string w in words)
                {
                    if (wordInProgress)
                    {
                        currWord += " " + w;
                        if (w.ToLower().Contains("</h1>"))
                        {
                            wordInProgress = false;
                            lookingForMeaning = true;
                        }
                    }
                    else if (meaningInProgress)
                    {
                        currMeaning += " " + w;
                        if (w.ToLower().Contains("</def>"))
                        {
                            AddToDict(currWord, currMeaning);
                            currWord = string.Empty;
                            currMeaning = string.Empty;
                            meaningInProgress = false;
                            lookingForWord = true;
                        }
                    }
                    else if (lookingForWord && w.ToLower().Contains("<h1>"))
                    {
                        currWord = w;
                        if (!w.ToLower().Contains("</h1>"))
                        {
                            wordInProgress = true;
                        }
                        else
                        {
                            lookingForMeaning = true;
                        }

                        lookingForWord = false;
                    }
                    else if (lookingForMeaning && w.ToLower().Contains("<def>"))
                    {
                        currMeaning = w;
                        if (w.ToLower().Contains("</def>"))
                        {
                            AddToDict(currWord, currMeaning);
                            currWord = string.Empty;
                            currMeaning = string.Empty;
                            lookingForWord = true;
                        }
                        else
                        {
                            meaningInProgress = true;
                        }

                        lookingForMeaning = false;
                    }
                }

                WriteDictToFile();
            }
        }

        private static void WriteDictToFile()
        {
            // Example #3: Write only some strings in an array to a file. 
            // The using statement automatically closes the stream and calls  
            // IDisposable.Dispose on the stream object. 
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\gitrepos\myrepos\resources\EnglishDictionary-Sanitized.txt"))
            {
                foreach (var kvp in dict)
                {
                    file.WriteLine(kvp.Key + " ====== " + kvp.Value);
                }
            }
        }

        private static void AddToDict(string w, string m)
        {
            w = w.Replace("<h1>", "");
            w = w.Replace("</h1>", "");
            m = m.Replace("<def>", "");
            m = m.Replace("</def>", "");
            if (!dict.ContainsKey(w))
            {
                dict.Add(w, m);
            }
        }
    }
}
