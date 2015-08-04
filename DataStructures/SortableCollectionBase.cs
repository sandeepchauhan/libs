using Learning.Libs.DataStructures.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Diagnostics;

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
        public void Sort(SortingAlgorithm sortingAlgorithm = SortingAlgorithm.NA)
        {
            SortingStatistics.Instance.Reset();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            SortInternal(sortingAlgorithm);
            sw.Stop();
            int j = 0;
            foreach (T i in this)
            {
                Console.WriteLine(i);
                if (++j == 20)
                {
                    break;
                }
            }
            Console.WriteLine("Time taken: " + sw.ElapsedMilliseconds);
            SortingStatistics.Instance.Print();
        }

        protected abstract void SortInternal(SortingAlgorithm sortingAlgorithm);

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
