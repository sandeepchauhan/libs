using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Libs.DataStructures
{
    public class ComparatorImpl<T> : IComparer<T> where T : IComparable<T>
    {
        private static ComparatorImpl<T> _instance;

        public static ComparatorImpl<T> Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ComparatorImpl<T>();
                }

                return _instance;
            }
        }

        public int Compare(T x, T y)
        {
            SortingStatistics.Instance.NumComparisons++;
            return x.CompareTo(y);
        }
    }
}
