using Learning.Libs.DataStructures;
using System;
using System.Collections.Generic;
using Learning.Libs.DataStructures.Interfaces;
using Learning.Libs.DataStructures.Enums;
using Learning.Libs.Utils;
using System.Collections;
using System.IO;
using System.Linq;
using Learning.Libs.ExtensionMethods;
using ProblemSolutions;

namespace DictionaryConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //ElasticSearch.ExecuteScenario();
            var longs = UtilityMethods.GenerateRandomLongs(1 << 20);
            var list = longs.Select(a => a.ToString());
            File.WriteAllLines(@"D:\gitrepos\myrepos\resources\RandomLongs4.txt", list);
            Console.ReadKey();
        }
    }
}
