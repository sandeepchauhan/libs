using Learning.Libs.DataStructures.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishDictionary.Models
{
    public class ListDictionary : IStringDictionary<string>
    {
        private List<Tuple<string, string>> _dictionary = new List<Tuple<string, string>>();

        public bool TryAddWord(string word, string data)
        {
            bool retVal = true;
            if (_dictionary.Where(x => x.Item1.Equals(word.ToLowerInvariant())).Any())
            {
                retVal = false;
            }
            else
            {
                Tuple<string, string> t = new Tuple<string, string>(word.ToLowerInvariant(), data);
                _dictionary.Add(new Tuple<string, string>(word.ToLowerInvariant(), data));
                _dictionary.Add(t);
            }

            return retVal;
        }

        public string GetData(string input)
        {
            input = input.ToLowerInvariant();
            return _dictionary.Find(x => x.Item1.Equals(input)).Item1;
        }

        public IEnumerable<string> GetSuggestions(string input)
        {
            input = input.ToLowerInvariant();
            return _dictionary.FindAll(x => x.Item1.StartsWith(input)).Select(x => x.Item1);
        }

        public string GetStats()
        {
            string stats = "ListDictionary";
            stats += "\n" + "Number of words: " + _dictionary.Count;
            return stats;
        }

        private class EqualityComparer : IEqualityComparer<Tuple<string, string>>
        {
            public bool Equals(Tuple<string, string> x, Tuple<string, string> y)
            {
                return x.Item1.Equals(y.Item1);
            }

            public int GetHashCode(Tuple<string, string> x)
            {
                return x.Item1.GetHashCode();
            }
        }
    }
}