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
        public int NumComparisons;

        public int NumSwaps;

        public int MaxRecursionDepth;

        public long TimeTaken;

        private int _currentRecursionDepth;

        private static SortingStatistics _instance;

        private SortingStatistics()
        {

        }

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
            Console.WriteLine(string.Format("Comparisons: {0}, Swaps: {1}, MaxRecursionDepth: {2}.", NumComparisons, NumSwaps, MaxRecursionDepth));
        }

        public void Reset()
        {
            _instance = new SortingStatistics();
        }

        public void IncrementCurrentRecursionDepth()
        {
            if (++this._currentRecursionDepth > MaxRecursionDepth)
            {
                MaxRecursionDepth = this._currentRecursionDepth;
                if (MaxRecursionDepth > 500)
                {

                }
            }
        }

        public void DecrementRecursionDepth()
        {
            this._currentRecursionDepth--;
        }
    }
}
