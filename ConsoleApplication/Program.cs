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
using Learning.Libs.DataStructures.Enums;
using System.Threading;
using Learning.Libs.Utils;

namespace DictionaryConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = UtilityMethods.ShuffledArrayOfFirstNNaturalNumbers(10);
            arr.PrintArray();
            Heap<int> heap = new Heap<int>(arr);
            heap.Sort(SortingAlgorithm.HeapSort, SortingAlgorithmType.Iterative);
            int i = 0;
            foreach(int x in heap)
            {
                arr[i++] = x;
            }
            arr.PrintArray();
            //int x = -6;
            //Console.WriteLine(GetBits(x));
            Console.ReadKey();
        }
        
        private static string GetBits(int x)
        {
            string ret = "";
            while (x != 0)
            {
                int rem = x % 2;
                ret = rem + " " + ret;
                x = x / 2;
            }

            return ret;
        }

        static void Main1(string[] args)
        {
            List<LinkedListNodeImpl<Guid>> l = new List<LinkedListNodeImpl<Guid>>();
            while(true)
            {
                l.Add(new LinkedListNodeImpl<Guid>(Guid.NewGuid()));
                if (l.Count % 1000 == 0)
                {
                    Console.WriteLine(l.Count);
                }
                //Thread.Sleep(1);
            }

            //PrintKthSmallestInUnionOfTwoArrays();
            //ArrayImpl<char> strArr = new ArrayImpl<char>();
            //strArr.AddMany("cat".ToList());
            //strArr.Sort(SortingAlgorithm.QuickSort, SortingAlgorithmType.Iterative);
            //List<string> permutations = ArrayString.GetPermutationsIterative("abcdefghij");
            //permutations.ForEach(x => Console.WriteLine(x));
            //Console.WriteLine("Press any key to exit....");
            //Console.ReadKey();
        }

        private static void AddOneToInfinitelyLongNumber()
        {
            LinkedListNodeImpl<int> head = null;
            LinkedListNodeImpl<int> tail = null;
            int[] arr = new int[] { 9, 9, 9 };
            foreach (int i in arr)
            {
                if (head == null)
                {
                    head = new LinkedListNodeImpl<int>(i);
                    tail = head;
                }
                else
                {
                    tail.Next = new LinkedListNodeImpl<int>(i);
                    tail = tail.Next;
                }
            }
            Console.WriteLine(head);
            LinkedListNodeImpl<int> lastNonNine = null;
            LinkedListNodeImpl<int> current = head;
            while (current != null)
            {
                if (current.Data != 9)
                {
                    lastNonNine = current;
                }
                current = current.Next;
            }
            if (lastNonNine == null)
            {
                LinkedListNodeImpl<int> newHead = new LinkedListNodeImpl<int>(lastNonNine.Data);
                newHead.Next = head;
                head = newHead;
                lastNonNine = head;
            }
            lastNonNine.Data++;
            current = lastNonNine.Next;
            while (current != null)
            {
                current.Data = 0;
                current = current.Next;
            }
            Console.WriteLine(head);
        }

        private static void PrintKthSmallestInUnionOfTwoArrays()
        {
            int[] arr1 = new int[] { 3, 5, 5, 5, 8, 8, 100, 125 };
            int[] arr2 = new int[] { 1, 6, 6, 7, 7, 9 };
            int k = 3;
            string s1 = "";
            arr1.ToList().ForEach(x => { s1 = s1 + x + " "; });
            string s2 = "";
            arr2.ToList().ForEach(x => { s2 = s2 + x + " "; });
            Console.WriteLine(s1);
            Console.WriteLine(s2);
            int element = -1, length1 = arr1.Length, length2 = arr2.Length, elementCounter = 0;
            if (length1 == 0 || length2 == 0)
            {
                if (length1 == 0 && length2 == 0)
                {
                    throw new Exception("Both arrays can not be empty.");
                }
                if (length1 == 0)
                {
                    PrintKthSmallestInASortedArray(arr2, k);
                }
                else
                {
                    PrintKthSmallestInASortedArray(arr1, k);
                }
            }
            else
            {
                int ptr1 = 0, ptr2 = 0;
                element = (arr1[ptr1] <= arr2[ptr2]) ? arr1[ptr1++] : arr2[ptr2++];
                elementCounter++;
                while (elementCounter < k && (ptr1 < length1 || ptr2 < length2))
                {
                    if (ptr1 < length1 && (ptr2 >= length2 || arr1[ptr1] <= arr2[ptr2]))
                    {
                        if (arr1[ptr1] != element)
                        {
                            element = arr1[ptr1];
                            elementCounter++;
                        }
                        ptr1++;
                    }
                    else if (ptr2 < length2)
                    {
                        if (arr2[ptr2] != element)
                        {
                            element = arr2[ptr2];
                            elementCounter++;
                        }
                        ptr2++;
                    }
                }

                if (elementCounter < k)
                {
                    Console.WriteLine("No kth element found.");
                }
                else
                {
                    Console.WriteLine(k + "th smallest element: " + element);
                }
            }
        }

        private static void PrintKthSmallestInASortedArray(int[] arr, int k)
        {
            int ptr = 0, elementCounter = 0, length = arr.Length;
            int element = arr[ptr++];
            elementCounter++;
            while (ptr < length && elementCounter < k)
            {
                if (arr[ptr] == element)
                {
                    ptr++;
                    continue;
                }
                else
                {
                    element = arr[ptr++];
                    elementCounter++;
                }
            }
            if (elementCounter < k)
            {
                Console.WriteLine("No kth element found.");
            }
            else
            {
                //Console.WriteLine(actualK + "th smallest element: " + element);
            }
        }

        private static void Sorting()
        {
            int numElements = 20000;
            //int numElements = 80;
            SortingAlgorithm sortingAlgorithm;
            ISortableCollection<int> collection;
            List<SortInputType> sortingCases = new List<SortInputType>()
            {
                //SortInputType.BestCase
                SortInputType.WorstCase
                //SortInputType.Random
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
                    collection.Sort(sortingAlgorithm, SortingAlgorithmType.Iterative);
                    Console.WriteLine("====================================================================");
                }
            }
            #endregion
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
            TrieDictionary trieDictionary = new TrieDictionary(@"D:\gitrepos\myrepos\resources\EnglishDictionary-Processed.txt");
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
