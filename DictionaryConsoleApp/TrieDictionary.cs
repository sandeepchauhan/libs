using Learning.Libs.DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DictionaryConsoleApp
{
    class TrieDictionary
    {
        public static Trie<string> Populate()
        {
            Trie<string> trie = new Trie<string>();
            int insertions = 0;
            string[] lines = File.ReadAllLines(@"D:\gitrepos\myrepos\resources\EnglishDictionary1.txt");
            foreach (string l in lines)
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

            return trie;
        }
    }
}
