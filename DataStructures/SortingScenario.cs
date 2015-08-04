using Learning.Libs.DataStructures.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Libs.DataStructures
{
    public class SortingScenario
    {
        public SortingAlgorithm SortingAlgorithm;

        public int NumElements;

        public SortInputType SortInputType;

        public SortingScenario(SortingAlgorithm sortingAlgorithm, int numElements, SortInputType sortInputType)
        {
            this.SortingAlgorithm = sortingAlgorithm;
            this.NumElements = numElements;
            this.SortInputType = sortInputType;
        }

        public override string ToString()
        {
            return string.Format("Algorithm: {0}, NumElements: {1}, InputType: {2}.", SortingAlgorithm, NumElements, SortInputType);
        }
    }
}