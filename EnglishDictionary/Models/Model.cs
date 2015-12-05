using EnglishDictionary.HelperClasses;
using Learning.Libs.DataStructures;
using Learning.Libs.DataStructures.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishDictionary.Models
{
    public class Model
    {
        public static IStringDictionary<string> Dictionary;
        
        public static void Populate()
        {
            //Dictionary = new Trie<string>();
            //Dictionary = new ListDictionary();
            Dictionary = new StringHashTable<string>();
            DictionaryPopulator.Populate(Dictionary);
        }
    }
}