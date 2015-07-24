using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Libs.DataStructures
{
    public class FunctionCost
    {
        public int NumComparisons;

        public int NumPropertyAccesses;
    }

    public class SortingStatistics
    {
        public List<FunctionCost> MergeSortCosts = new List<FunctionCost>();

        public List<FunctionCost> MidCosts = new List<FunctionCost>();

        public List<FunctionCost> MergeCosts = new List<FunctionCost>();

        public int NumComparisons;

        public int NumSwaps;

        private static SortingStatistics _instance;

        public static SortingStatistics Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SortingStatistics();
                }

                return _instance;
            }
        }

        public void Print()
        {
            Console.WriteLine("Number of comparisons: " + NumComparisons);
            Console.WriteLine("Number of swaps: " + NumSwaps);
        }
    }
}
