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
        private Tuple<string, string>[] _wordMeanings;

        private Trie _trie;

        public TrieDictionary(string filePath)
        {
            string[] lines = File.ReadAllLines(@"D:\gitrepos\myrepos\resources\EnglishDictionary-Processed.txt");
            _wordMeanings = new Tuple<string, string>[lines.Length];
            int index = 0;
            foreach (string l in lines)
            {
                string[] parts = l.Split(new string[] { "======" }, StringSplitOptions.None);
                string word = parts[0].Trim();
                if (Regex.IsMatch(word, @"^[a-zA-Z]+$"))
                {
                    _wordMeanings[index++] = new Tuple<string, string>(word, parts[1].Trim());
                }
            }
        }

        public Trie Trie
        {
            get
            {
                return _trie;
            }
        }
    }
}
