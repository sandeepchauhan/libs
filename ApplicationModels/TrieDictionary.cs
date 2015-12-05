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
    public class TrieDictionary
    {
        private Tuple<string, string>[] _wordMeanings;

        private Trie _trie = new Trie();

        public TrieDictionary(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            _wordMeanings = new Tuple<string, string>[lines.Length];
            int index = 0;
            foreach (string l in lines)
            {
                string[] parts = l.Split(new string[] { "======" }, StringSplitOptions.None);
                string word = parts[0].Trim();
                if (Regex.IsMatch(word, @"^[a-zA-Z]+$"))
                {
                    _wordMeanings[index] = new Tuple<string, string>(word, parts[1].Trim());
                    _trie.AddWord(word, index++);
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

        public string GetMeaning(string word)
        {
            string ret = null;
            IEnumerable<int> indexes = _trie.FindWord(word);
            if (indexes.Any())
            {
                ret = _wordMeanings[indexes.First()].Item2;
            }

            return ret;
        }

        public List<string> GetSuggestions(string word)
        {
            List<string> ret = new List<string>();
            foreach(int i in _trie.GetSuggestions(word))
            {
                ret.Add(_wordMeanings[i].Item1);
            }

            return ret;
        }
    }
}
