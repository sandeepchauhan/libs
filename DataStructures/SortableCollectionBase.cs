using Learning.Libs.DataStructures.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Learning.Libs.DataStructures
{
    public abstract class SortableCollectionBase<T> : ISortableCollection<T> where T : IComparable<T>
    {
        public int Count
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public abstract void Add(T item);
        public void AddMany(List<T> items)
        {
            foreach(T item in items)
            {
                this.Add(item);
            }
        }

        public abstract IEnumerator<T> GetEnumerator();
        public abstract void Sort(SortingAlgorithm sortingAlgorithm = SortingAlgorithm.NA);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }
    }
}
