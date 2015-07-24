using Learning.Libs.DataStructures.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Libs.DataStructures
{
    public class ArrayImpl<T> : ISortableCollection<T> where T : IComparable<T>
    {
        private T[] _array;

        private T[] _auxArray;

        private int _size;

        private int _capacity;

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

        public ArrayImpl(int capacity = 512)
        {
            _capacity = capacity;
            _array = new T[capacity];
        }

        public void Add(T data)
        {
            if (_size >= _capacity)
            {
                throw new Exception("Array is full.");
            }

            _array[_size++] = data;
        }

        public void Sort(SortingAlgorithm sortingAlgorithm)
        {
            if (this._size > 1)
            {
                switch (sortingAlgorithm)
                {
                    case SortingAlgorithm.SelectionSort:
                        SelectionSort();
                        break;
                    case SortingAlgorithm.InsertionSort:
                        InsertionSort();
                        break;
                    case SortingAlgorithm.MergeSort:
                        _auxArray = new T[_size];
                        MergeSort(0, _size - 1);
                        break;
                    case SortingAlgorithm.QuickSort:
                        QuickSort(0, _size - 1);
                        break;
                    default:
                        throw new Exception("Sorting algorithm: " + sortingAlgorithm + " not supported.");
                }
            }
            SortingStatistics.Instance.Print();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _array.Take(_size).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void SelectionSort()
        {
            int nextSortedElementPos = 0, unsortedHead = 0;
            while (unsortedHead < (_size - 1))
            {
                int minElement = unsortedHead, current = unsortedHead + 1; 
                while(current < _size)
                {
                    if (ComparatorImpl<T>.Instance.Compare(_array[current], _array[minElement]) < 0)
                    {
                        minElement = current;
                    }
                    current++;
                }
                Swap(nextSortedElementPos++, minElement);
                unsortedHead++;
            }
        }

        private void InsertionSort()
        {
            int unsortedHead = 1;
            while(unsortedHead < _size)
            {
                int current = unsortedHead;
                while(current > 0 && ComparatorImpl<T>.Instance.Compare(_array[current], _array[current - 1]) < 0)
                {
                    Swap(current, current - 1);
                    current--;
                }
                unsortedHead++;
            }
        }

        private void MergeSort(int start, int end)
        {
            if (start > end)
            {
                throw new Exception("Start can not be greater than end.");
            }
            if (start == end)
            {
                return;
            }
            else if (end - start == 1)
            {
                if (ComparatorImpl<T>.Instance.Compare(_array[start], _array[end]) > 0)
                {
                    Swap(start, end);
                }
            }
            else
            {
                int mid = (start + end) / 2;
                MergeSort(start, mid);
                MergeSort(mid + 1, end);
                #region Merge
                int curr1 = start, curr2 = mid + 1, sortedCurr = 0;
                while(curr1 <= mid && curr2 <= end)
                {
                    if (ComparatorImpl<T>.Instance.Compare(_array[curr1], _array[curr2]) <= 0)
                    {
                        _auxArray[sortedCurr++] = _array[curr1++];
                    }
                    else
                    {
                        _auxArray[sortedCurr++] = _array[curr2++];
                    }
                }
                if (curr1 <= mid)
                {
                    while(curr1 <= mid)
                    {
                        _auxArray[sortedCurr++] = _array[curr1++];
                    }
                }
                else if (curr2 <= end)
                {
                    while (curr2 <= end)
                    {
                        _auxArray[sortedCurr++] = _array[curr2++];
                    }
                }
                // Copy back to the original array
                for(int i = 0; i < sortedCurr; i++)
                {
                    _array[start + i] = _auxArray[i];
                }
                #endregion
            }
        }

        private void QuickSort(int start, int end)
        {
            if (start > end)
            {
                throw new Exception("Start can not be greater than end.");
            }
            if (start == end)
            {
                return;
            }
            else if (end - start == 1)
            {
                if (ComparatorImpl<T>.Instance.Compare(_array[start], _array[end]) > 0)
                {
                    Swap(start, end);
                }
            }
            else
            {
                int pivot = start;
                #region Put pivot in final position

                #endregion
            }
        }

        private void Swap(int x, int y)
        {
            if (x != y)
            {
                T tmp = _array[y];
                _array[y] = _array[x];
                _array[x] = tmp;
                SortingStatistics.Instance.NumSwaps++;
            }
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
    }
}
