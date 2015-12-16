using ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishDictionary.Models
{
    public class ModelWrapper
    {
        private static TrieDictionary _trieDictionary = new TrieDictionary(@"D:\gitrepos\myrepos\resources\EnglishDictionary-Processed.txt");

        public static TrieDictionary TrieDictionary
        {
            get
            {
                return _trieDictionary;
            }
        }
    }
}