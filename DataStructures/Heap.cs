using Learning.Libs.DataStructures.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Learning.Libs.DataStructures
{
    public class Heap<T> : ISortableCollection<T> where T : IComparable<T>
    {
        private T[] _array;

        private int _size;

        public int Count
        {
            get
            {
                return ((ICollection<T>)_array).Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return ((ICollection<T>)_array).IsReadOnly;
            }
        }

        public Heap()
        {
            _array = new T[512];
        }

        public void Add(T data)
        {
            _array[_size++] = data;
        }

        public void Sort(SortingAlgorithm sortingAlgorithm = SortingAlgorithm.NA)
        {
            int s = _size;
            BuildHeap();
            while(_size > 1)
            {
                Swap(0, --_size);
                Heapify(0);
            }

            _size = s;
        }

        private void BuildHeap()
        {
            int currParent = GetParent(_size - 1);
            while(currParent >= 0)
            {
                Heapify(currParent);
                currParent--;
            }
        }

        private void Heapify(int node)
        {
            while (node != -1)
            {
                int lc = GetLeftChild(node);
                int rc = GetRightChild(node);
                if (lc < _size && (_array[lc].CompareTo(_array[rc]) < 0) && (_array[node].CompareTo(_array[lc]) > 0))
                {
                    // Left child is smaller
                    Swap(node, lc);
                    node = lc;
                }
                else if (rc < _size && (_array[node].CompareTo(_array[rc]) > 0))
                {
                    // Right child is smaller
                    Swap(node, rc);
                    node = rc;
                }
                else
                {
                    node = -1;
                }
            }
        }

        private void Swap(int x, int y)
        {
            T tmp = _array[x];
            _array[x] = _array[y];
            _array[y] = tmp;
        }

        private int GetParent(int index)
        {
            return (int) Math.Floor(((double)index - 1) / 2);
        }

        private int GetLeftChild(int index)
        {
            return (index * 2) + 1;
        }

        private int GetRightChild(int index)
        {
            return (index * 2) + 2;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _array.Take(_size).AsEnumerable().GetEnumerator();
        }

        public void Clear()
        {
            ((ICollection<T>)_array).Clear();
        }

        public bool Contains(T item)
        {
            return ((ICollection<T>)_array).Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ((ICollection<T>)_array).CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return ((ICollection<T>)_array).Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((ICollection<T>)_array).GetEnumerator();
        }
    }
}
