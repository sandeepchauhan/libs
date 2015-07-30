using Learning.Libs.DataStructures.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Libs.DataStructures
{
    public class DataProviderForSortAlgorithms
    {
        public static List<T> GenerateBestCaseInput<T>(SortingAlgorithm sortingAlorithm, int size)
        {
            switch(sortingAlorithm)
            {
                case SortingAlgorithm.SelectionSort:
                case SortingAlgorithm.InsertionSort:
                case SortingAlgorithm.MergeSort:
                    if (typeof(T) == typeof(int))
                    {
                        List<int> retList = new List<int>();
                        for (int i = 1; i <= size; i++)
                        {
                            retList.Add(i);
                        }

                        return (List<T>)(object)retList;
                    }
                    break;
            }

            throw new NotSupportedException();
        }

        public static List<T> GenerateRandomInput<T>(int size)
        {
            if (typeof(T) == typeof(int))
            {
                List<int> retList = new List<int>();
                for (int i = 1; i <= size; i++)
                {
                    retList.Add(i);
                }

                return (List<T>)(object)retList;
            }
            else if (typeof(T) == typeof(Guid))
            {
                List<Guid> retList = new List<Guid>();
                for (int i = 1; i <= size; i++)
                {
                    retList.Add(Guid.NewGuid());
                }

                return (List<T>)(object)retList;
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public static List<T> GenerateWorstCaseInput<T>(SortingAlgorithm sortingAlorithm, int size)
        {
            switch (sortingAlorithm)
            {
                case SortingAlgorithm.SelectionSort:
                case SortingAlgorithm.InsertionSort:
                case SortingAlgorithm.QuickSort:
                case SortingAlgorithm.HeapSort:
                    if (typeof(T) == typeof(int))
                    {
                        List<int> retList = new List<int>();
                        for (int i = size; i >= 1; i--)
                        {
                            retList.Add(i);
                        }

                        return (List<T>)(object)retList;
                    }
                    break;
            }

            throw new NotSupportedException();
        }
    }
}
