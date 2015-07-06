using Learning.Libs.DataStructures.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Libs.DataStructures
{
    public class StringHashTable<T> : HashTable<CustomString, T>, IStringDictionary<T>
    {
        public bool TryAddWord(string word, T data)
        {
            return TryAdd(new CustomString(word), data);
        }

        public T GetData(string input)
        {
            return base.GetData(new CustomString(input));
        }

        public IEnumerable<string> GetSuggestions(string input)
        {
            return new List<string>();
        }

        public string GetStats()
        {
            string stats = "HashTable";
            stats += "\n" + "Num of words: " + Size;
            stats += "\n" + "Load factor: " + LoadFactor;
            return stats;
        }
    }
}
