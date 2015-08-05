using Learning.Libs.DataStructures.Enums;
using Learning.Libs.DataStructures.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Libs.DataStructures
{
    public class ArrayImpl<T> : SortableCollectionBase<T> where T : IComparable<T>
    {
        private T[] _array;

        private T[] _auxArray;

        private int _size;

        private int _capacity;

        public ArrayImpl(int capacity = 512)
        {
            _capacity = capacity;
            _array = new T[capacity];
        }

        public override void Add(T data)
        {
            if (_size >= _capacity)
            {
                throw new Exception("Array is full.");
            }

            _array[_size++] = data;
        }

        protected override void SortInternal(SortingAlgorithm sortingAlgorithm, SortingAlgorithmType sortingAlgorithmType)
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
                        QuickSortIterative(0, _size - 1);
                        break;
                    case SortingAlgorithm.HeapSort:
                        HeapSort();
                        break;
                    default:
                        throw new Exception("Sorting algorithm: " + sortingAlgorithm + " not supported.");
                }
            }
        }

        override public IEnumerator<T> GetEnumerator()
        {
            return _array.Take(_size).GetEnumerator();
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
                SortingStatistics.Instance.IncrementCurrentRecursionDepth();
                MergeSort(start, mid);
                SortingStatistics.Instance.DecrementRecursionDepth();
                SortingStatistics.Instance.IncrementCurrentRecursionDepth();
                MergeSort(mid + 1, end);
                SortingStatistics.Instance.DecrementRecursionDepth();
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

        /// <summary>
        /// Sorts the array in-place using quick sort algorithim (recursive version).
        /// </summary>
        /// <param name="start">Index of the first element in array to sort.</param>
        /// <param name="end">Index of the last element in array to sort.</param>
        private void QuickSort(int start, int end)
        {
            #region Exception cases
            if (start > end)
            {
                throw new Exception("Start can not be greater than end.");
            }
            #endregion
            #region Trivial cases
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
            #endregion
            #region Non-Trivial cases
            else
            {
                T pivotData = _array[start];
                #region Partition elements based on pivot
                int leftArrayTail = -1, rightArrayHead = -1, rightArrayTail = -1, current = start + 1;
                while(current <= end)
                {
                    if (ComparatorImpl<T>.Instance.Compare(_array[current], pivotData) <= 0)
                    {
                        if (rightArrayHead != -1 && current > rightArrayHead)
                        {
                            leftArrayTail = rightArrayHead;
                            Swap(rightArrayHead++, current);
                        }
                        else
                        {
                            leftArrayTail = current;
                        }
                    }
                    else
                    {
                        if (rightArrayHead == -1)
                        {
                            rightArrayHead = current;
                        }
                        rightArrayTail = current;
                    }
                    current++;
                }
                #endregion
                #region Recursively sort left and right sub parts.
                if (leftArrayTail != -1)
                {
                    Swap(start, leftArrayTail);
                    SortingStatistics.Instance.IncrementCurrentRecursionDepth();
                    QuickSort(start, leftArrayTail - 1);
                    SortingStatistics.Instance.DecrementRecursionDepth();
                }
                if (rightArrayTail != -1 && (rightArrayTail - rightArrayHead) > 0)
                {
                    SortingStatistics.Instance.IncrementCurrentRecursionDepth();
                    QuickSort(rightArrayHead, rightArrayTail);
                    SortingStatistics.Instance.DecrementRecursionDepth();
                }
                #endregion
            }
            #endregion
        }

        /// <summary>
        /// Sorts the array in-place using quick sort algorithim (iterative version).
        /// </summary>
        /// <param name="start">Index of the first element in array to sort.</param>
        /// <param name="end">Index of the last element in array to sort.</param>
        private void QuickSortIterative(int start, int end)
        {
            Queue<Tuple<int, int>> arraysToCallPartitionOn = new Queue<Tuple<int, int>>();
            arraysToCallPartitionOn.Enqueue(new Tuple<int, int>(start, end));
            while(arraysToCallPartitionOn.Any())
            {
                Tuple<int, int> array = arraysToCallPartitionOn.Dequeue();
                int p = QuickSortPartition(array.Item1, array.Item2);
                if (array.Item1 < p)
                {
                    arraysToCallPartitionOn.Enqueue(new Tuple<int, int>(array.Item1, p - 1));
                }
                if (array.Item2 > p)
                {
                    arraysToCallPartitionOn.Enqueue(new Tuple<int, int>(p + 1, array.Item2));
                }
            }
        }

        private int QuickSortPartition(int start, int end)
        {
            #region Exception cases
            if (start > end)
            {
                throw new Exception("Start can not be greater than end.");
            }
            #endregion
            int pivotPosition = start;
            #region Trivial cases
            if (end - start == 1)
            {
                pivotPosition = start;
                if (ComparatorImpl<T>.Instance.Compare(_array[start], _array[end]) > 0)
                {
                    Swap(start, end);
                    pivotPosition = end;
                }
            }
            #endregion
            #region Non-Trivial cases
            else if (end - start > 1)
            {
                T pivotData = _array[start];
                #region Partition elements based on pivot
                int leftArrayTail = -1, rightArrayHead = -1, rightArrayTail = -1, current = start + 1;
                while (current <= end)
                {
                    if (ComparatorImpl<T>.Instance.Compare(_array[current], pivotData) <= 0)
                    {
                        if (rightArrayHead != -1 && current > rightArrayHead)
                        {
                            leftArrayTail = rightArrayHead;
                            Swap(rightArrayHead++, current);
                        }
                        else
                        {
                            leftArrayTail = current;
                        }
                    }
                    else
                    {
                        if (rightArrayHead == -1)
                        {
                            rightArrayHead = current;
                        }
                        rightArrayTail = current;
                    }
                    current++;
                }
                #endregion
                #region Put pivot at right location.
                if (leftArrayTail != -1)
                {
                    Swap(start, leftArrayTail);
                    pivotPosition = leftArrayTail;
                }
                #endregion
            }
            #endregion
            return pivotPosition;
        }

        private void HeapSort()
        {
            int s = _size;
            BuildHeap();
            while (_size > 1)
            {
                Swap(0, --_size);
                Heapify(0);
            }

            _size = s;
        }

        private void BuildHeap()
        {
            int currParent = GetParent(_size - 1);
            while (currParent >= 0)
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
                if (lc < _size && rc < _size && (ComparatorImpl<T>.Instance.Compare(_array[lc], _array[rc]) > 0) && ComparatorImpl<T>.Instance.Compare(_array[node], _array[lc]) < 0)
                {
                    // Left child is bigger
                    Swap(node, lc);
                    node = lc;
                }
                else if (rc < _size && ComparatorImpl<T>.Instance.Compare(_array[node], _array[rc]) < 0)
                {
                    // Right child is bigger
                    Swap(node, rc);
                    node = rc;
                }
                else
                {
                    node = -1;
                }
            }
        }

        private int GetParent(int index)
        {
            return (int)Math.Floor(((double)index - 1) / 2);
        }

        private int GetLeftChild(int index)
        {
            return (index * 2) + 1;
        }

        private int GetRightChild(int index)
        {
            return (index * 2) + 2;
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

        override public string ToString()
        {
            StringBuilder strBuilder = new StringBuilder();
            foreach(T item in this)
            {
                strBuilder.Append(item.ToString());
            }
            return strBuilder.ToString();
        }
    }
}
