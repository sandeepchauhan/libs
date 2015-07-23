using Learning.Libs.DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Learning.Libs.ExtensionMethods;

namespace DictionaryConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Learning.Libs.DataStructures.LinkedList<int> list = new Learning.Libs.DataStructures.LinkedList<int>();
            Heap<int> heap = new Heap<int>();
            heap.Add(3);
            heap.Add(1);
            heap.Add(11);
            heap.Add(10);
            heap.Add(111);
            heap.Add(101);
            heap.Add(91);
            heap.Add(19);
            heap.Add(-1);
            heap.Add(-11);
            heap.Add(789);
            heap.Add(234);
            heap.Add(67);
            heap.Add(890);
            heap.Add(900);
            heap.Add(1);
            heap.Add(901);
            //list.Sort(SortingAlgorithm.MergeSort);
            //list.Sort(SortingAlgorithm.QuickSort);
            heap.Sort();
            foreach(int i in heap)
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
