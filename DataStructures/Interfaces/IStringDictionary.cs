using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Libs.DataStructures.Interfaces
{
    public interface IStringDictionary<T>
    {
        bool TryAddWord(string word, T data);

        T GetData(string input);

        IEnumerable<string> GetSuggestions(string input);

        string GetStats();
    }
}
