using Learning.Libs.DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Learning.Libs.ExtensionMethods;
using Learning.Libs.DataStructures.Interfaces;

namespace DictionaryConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ISortableCollection<int> collection = new ArrayImpl<int>();
            collection.Add(2);
            collection.Add(6);
            collection.Add(4);
            collection.Add(8);
            collection.Add(1);
            collection.Add(5);
            collection.Add(3);
            collection.Add(7);
            //list.Sort(SortingAlgorithm.MergeSort);
            //list.Sort(SortingAlgorithm.QuickSort);
            collection.Sort(SortingAlgorithm.MergeSort);
            foreach(int i in collection)
            {
                Console.WriteLine(i);
            }

            bool isPrime = IsPrime(4111);
            DictionarySanitizer.WriteSanitizedDictionary();
            Trie<string> trieDictionary = TrieDictionary.Populate();
            while (true)
            {
                string word = Console.ReadLine();
                string meaning = trieDictionary.GetData(word);
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

        public static bool IsPrime(int number)
        {
            int boundary = (int) Math.Floor(Math.Sqrt(number));

            if (number == 1) return false;
            if (number == 2) return true;

            for (int i = 2; i <= boundary; ++i)
            {
                if (number % i == 0) return false;
            }

            return true;
        }
    }
}
