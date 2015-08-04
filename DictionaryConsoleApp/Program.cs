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
using System.Diagnostics;

namespace DictionaryConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //int numElements = 20000;
            int numElements = 80;
            SortingAlgorithm sortingAlgorithm;
            ISortableCollection<int> collection;
            List<SortInputType> sortingCases = new List<SortInputType>()
            {
                //SortInputType.BestCase
                //SortInputType.WorstCase
                SortInputType.Random
            };

            #region Selection Sort
            sortingAlgorithm = SortingAlgorithm.QuickSort;
            foreach (SortInputType sit in sortingCases)
            {
                for (int i = 0; i < 1; i++)
                {
                    SortingScenario sortingScenario = new SortingScenario(sortingAlgorithm, numElements, sit);
                    Console.WriteLine(sortingScenario);
                    collection = new ArrayImpl<int>(numElements);
                    collection.AddMany(GetInput<int>(sortingAlgorithm, numElements, sit));
                    collection.Sort(sortingAlgorithm);
                    Console.WriteLine("====================================================================");
                }
            }
            #endregion

            Console.WriteLine();
            Console.WriteLine("Press any key to exit....");
            Console.ReadKey();
        }

        static List<T> GetInput<T>(SortingAlgorithm sortingAlgorithm, int numElements, SortInputType sortInputType)
        {
            switch(sortInputType)
            {
                case SortInputType.BestCase:
                    return DataProviderForSortAlgorithms.GenerateBestCaseInput<T>(sortingAlgorithm, numElements);
                case SortInputType.Random:
                    return DataProviderForSortAlgorithms.GenerateRandomInput<T>(numElements);
                case SortInputType.WorstCase:
                    return DataProviderForSortAlgorithms.GenerateWorstCaseInput<T>(sortingAlgorithm, numElements);
            }

            throw new NotSupportedException();
        }

        static void Code()
        {
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
