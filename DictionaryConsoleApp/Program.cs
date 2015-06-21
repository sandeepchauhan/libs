using Learning.Lib.DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Learning.Lib.ExtensionMethods;

namespace DictionaryConsoleApp
{
    class Program
    {
        private static Dictionary<string, string> dict = new Dictionary<string, string>();

        private static Trie<string> trie = new Trie<string>();

        static void Main(string[] args)
        {
            Trie<string>.Node n = new Trie<string>.Node();
            Trie<string>.Node[] arr = new Trie<string>.Node[1000];
            long m1 = GC.GetTotalMemory(true);
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = new Trie<string>.Node();
            }

            long m2 = GC.GetTotalMemory(true);
            long d = m2 - m1;
            Console.WriteLine(d);
            foreach(var x in arr)
            {
                Console.WriteLine(x);
            }
            //PopulateDictionary();
            //WriteDictToFile();
            int numWordsPopulated = PopulateTrie();
            Console.WriteLine("Trie Populated with " + numWordsPopulated + " words!");
            int numNodesInTrie = trie.GetNumNodes();
            Console.WriteLine("Number of nodes in Trie: " + numNodesInTrie);
            long trieSize = trie.SizeOfObject();
            Console.WriteLine("Size of Trie: " + trieSize);
            Console.WriteLine("Avg size of each node: " + trieSize/numNodesInTrie);
            while (true)
            {
                string word = Console.ReadLine();
                string meaning = trie.GetData(word);
                if (string.IsNullOrWhiteSpace(meaning))
                {
                    meaning = "Word not found!";
                }

                Console.WriteLine(meaning);
            }

            //while (true)
            //{
            //    string word = Console.ReadLine();
            //    List<string> suggestions = trie.GetSuggestions(word);
            //    if (suggestions.Count > 0)
            //    {
            //        foreach (string s in suggestions)
            //        {
            //            Console.WriteLine(s);
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine("No suggestions found!");
            //    }
            //}
        }

        private static int PopulateTrie()
        {
            int insertions = 0;
            string[] lines = File.ReadAllLines(@"D:\gitrepos\myrepos\resources\EnglishDictionary1.txt");
            foreach(string l in lines)
            {
                string[] parts = l.Split(new string[] { "======" }, StringSplitOptions.None);
                string word = parts[0].Trim();
                if (Regex.IsMatch(word, @"^[a-zA-Z]+$"))
                {
                    if (trie.TryAddWord(word, parts[1].Trim()))
                    {
                        insertions++;
                    }
                }
            }

            return insertions;
        }

        private static void WriteDictToFile()
        {
            // Example #3: Write only some strings in an array to a file. 
            // The using statement automatically closes the stream and calls  
            // IDisposable.Dispose on the stream object. 
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\gitrepos\myrepos\resources\EnglishDictionary1.txt"))
            {
                foreach (var kvp in dict)
                {
                    file.WriteLine(kvp.Key + " ====== " + kvp.Value);
                }
            }
        }

        private static void PopulateDictionary()
        {
            string content = File.ReadAllText(@"D:\gitrepos\myrepos\resources\EnglishDictionary.txt");
            string[] words = content.Split(null);
            bool lookingForWord = true;
            bool lookingForMeaning = false;
            bool wordInProgress = false;
            bool meaningInProgress = false;
            string currWord = string.Empty;
            string currMeaning = string.Empty;
            foreach(string w in words)
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
