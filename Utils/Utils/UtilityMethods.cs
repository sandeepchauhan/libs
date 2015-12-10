using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Learning.Libs.Utils
{
    public static class UtilityMethods
    {
        public static int[] ShuffledArrayOfFirstNNaturalNumbers(int n)
        {
            int[] arr = new int[n];
            int i = 0;
            while(i < n)
            {
                arr[i] = i + 1;
                i++;
            }

            RandomImpl r = new RandomImpl(0, n);
            int c = 0;
            while (c++ <= n)
            {
                int i1 = r.Next();
                int i2 = r.Next();
                SwapArrayElements(arr, i1, i2);
            }

            return arr;
        }

        public static Tuple<string, string>[] GetEnglishDictionary()
        {
            string[] lines = File.ReadAllLines(@"D:\gitrepos\myrepos\resources\EnglishDictionary-Processed.txt");
            Tuple<string, string> [] wordMeanings = new Tuple<string, string>[lines.Length];
            int index = 0;
            foreach (string l in lines)
            {
                string[] parts = l.Split(new string[] { "======" }, StringSplitOptions.None);
                string word = parts[0].Trim();
                if (Regex.IsMatch(word, @"^[a-zA-Z]+$"))
                {
                    wordMeanings[index] = new Tuple<string, string>(word, parts[1].Trim());
                }
            }

            return wordMeanings;
        }

        private static void SwapArrayElements(int[] arr, int i1, int i2)
        {
            int temp = arr[i1];
            arr[i1] = arr[i2];
            arr[i2] = temp;
        }
        
        public static long[] GenerateRandomLongs(int numLongsToGenerate)
        {
            long[] arr = new long[numLongsToGenerate];
            int index = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            RandomBool rb = new RandomBool(80);
            long curr = long.MinValue;
            int skipped = 0;
            while (curr < long.MaxValue && index < numLongsToGenerate)
            {
                if (rb.Next())
                {
                    arr[index++] = curr;
                    if (index % 1000 == 0)
                    {
                        Console.WriteLine("Index: " + index + " Time: " + sw.ElapsedMilliseconds + " Curr: " + curr);
                    }

                    skipped = 0;
                }
                else
                {
                    skipped++;
                }

                curr += 4000000000000;
            }

            return arr;
        }
    }
}
