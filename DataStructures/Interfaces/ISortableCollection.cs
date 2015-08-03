using Learning.Libs.DataStructures.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Libs.DataStructures.Interfaces
{
    public interface ISortableCollection<T> : IEnumerable<T>, ICollection<T> where T : IComparable<T>
    {
        void Sort(SortingAlgorithm sortingAlgorithm, SortingAlgorithmType sortingAlgorithmType);

        void AddMany(List<T> items);
    }
}
