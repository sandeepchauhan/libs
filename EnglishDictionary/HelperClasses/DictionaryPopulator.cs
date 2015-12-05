using Learning.Libs.DataStructures;
using Learning.Libs.DataStructures.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;

namespace EnglishDictionary.HelperClasses
{
    public class DictionaryPopulator
    {
        public static void Populate(IStringDictionary<string> dictionary)
        {
            int insertions = 0;
            DictionarySanitizer.WriteSanitizedDictionary();
            string[] lines = File.ReadAllLines(WebConfigurationManager.AppSettings[Constants.DICTIONARY_FILE__PROCESSED]);
            foreach (string l in lines)
            {
                string[] parts = l.Split(new string[] { "======" }, StringSplitOptions.None);
                string word = parts[0].Trim().ToLowerInvariant();
                if (Regex.IsMatch(word, @"^[a-zA-Z]+$"))
                {
                    if (dictionary.TryAddWord(word, parts[1].Trim()))
                    {
                        insertions++;
                    }
                    if (dictionary.TryAddWord(word + "x", parts[1].Trim()))
                    {
                        insertions++;
                    }
                    if (dictionary.TryAddWord(word + "xx", parts[1].Trim()))
                    {
                        insertions++;
                    }
                }
            }
        }
    }
}