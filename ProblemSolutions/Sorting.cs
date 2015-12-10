using Learning.Libs.DataStructures;
using Learning.Libs.DataStructures.Enums;
using Learning.Libs.DataStructures.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemSolutions
{
    class Sorting
    {
        private static void DoSorting()
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

        private static List<T> GetInput<T>(SortingAlgorithm sortingAlgorithm, int numElements, SortInputType sortInputType)
        {
            switch (sortInputType)
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
    }
}
