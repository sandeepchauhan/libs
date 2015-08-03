using Learning.Libs.DataStructures.Enums;
using Learning.Libs.DataStructures.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Learning.Libs.DataStructures
{
    public class LinkedList<T> : SortableCollectionBase<T> where T : IComparable<T>
    {
        private class Node
        {
            public T Data { get; set; }

            public Node Next { get; set; }

            public Node(T data)
            {
                this.Data = data;
            }
        }

        private class Enumerator : IEnumerator<T>
        {
            private LinkedList<T> _list;

            private Node _current;

            private Node _fakeNode;

            public Enumerator(LinkedList<T> list)
            {
                _list = list;
                _fakeNode = new Node(default(T));
                _fakeNode.Next = _list._head;
                _current = _fakeNode;
            }

            public T Current
            {
                get
                {
                    return _current.Data;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            // Summary:
            //     Advances the enumerator to the next element of the collection.
            //
            // Returns:
            //     true if the enumerator was successfully advanced to the next element; false
            //     if the enumerator has passed the end of the collection.
            //
            // Exceptions:
            //   System.InvalidOperationException:
            //     The collection was modified after the enumerator was created.
            public bool MoveNext()
            {
                bool retVal = false;
                if (_current.Next != null)
                {
                    _current = _current.Next;
                    retVal = true;
                }

                return retVal;
            }

            //
            // Summary:
            //     Sets the enumerator to its initial position, which is before the first element
            //     in the collection.
            //
            // Exceptions:
            //   System.InvalidOperationException:
            //     The collection was modified after the enumerator was created.
            public void Reset()
            {
                _current = _fakeNode;
            }

            // Summary:
            //     Performs application-defined tasks associated with freeing, releasing, or
            //     resetting unmanaged resources.
            public void Dispose()
            {

            }
        }

        private Node _head;

        private Node _tail;

        private int _length;

        private SortingStatistics _sortingStats;

        override public void Add(T data)
        {
            if (_head == null)
            {
                _head = _tail = new Node(data);
            }
            else
            {
                _tail.Next = new Node(data);
                _tail = _tail.Next;
            }
            _length++;
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        protected override void SortInternal(SortingAlgorithm sortingAlgorithm, SortingAlgorithmType sortingAlgorithmType)
        {
            if (this._head != null && this._head.Next != null)
            {
                switch (sortingAlgorithm)
                {
                    case SortingAlgorithm.SelectionSort:
                        this._head = SelectionSort(this._head);
                        break;
                    case SortingAlgorithm.InsertionSort:
                        this._head = InsertionSort(this._head);
                        break;
                    case SortingAlgorithm.MergeSort:
                        this._head = MergeSort(this._head);
                        break;
                    case SortingAlgorithm.QuickSort:
                        this._head = QuickSort(this._head).Item1;
                        break;
                }
            }
        }

        private Tuple<Node, Node> QuickSort(Node head)
        {
            Node pivotNode = head;
            Node current = head.Next;
            Node leftChainHead = null, leftChainTail = null;
            Node rightChainHead = null, rightChainTail = null;
            #region while loop
            while (current != null)
            {
                Node next = current.Next;
                current.Next = null;
                if (current.Data.CompareTo(pivotNode.Data) < 0)
                {
                    if (leftChainTail == null)
                    {
                        leftChainHead = leftChainTail = current;
                    }
                    else
                    {
                        leftChainTail.Next = current;
                        leftChainTail = leftChainTail.Next;
                    }
                }
                else
                {
                    if (rightChainTail == null)
                    {
                        rightChainHead = rightChainTail = current;
                    }
                    else
                    {
                        rightChainTail.Next = current;
                        rightChainTail = rightChainTail.Next;
                    }
                }
                current = next;
            }
            #endregion
            Node retHead = null, retTail = null;
            Tuple<Node, Node> leftChainSorted = null;
            Tuple<Node, Node> rightChainSorted = null;
            pivotNode.Next = null;
            if (leftChainTail != null)
            {
                leftChainTail.Next = null;
                leftChainSorted = QuickSort(leftChainHead);
            }
            if (rightChainTail != null)
            {
                rightChainTail.Next = null;
                rightChainSorted = QuickSort(rightChainHead);
            }
            if (leftChainSorted != null)
            {
                retHead = leftChainSorted.Item1;
                leftChainSorted.Item2.Next = pivotNode;
                retTail = pivotNode;
            }
            else
            {
                retHead = pivotNode;
                retTail = pivotNode;
            }

            if (rightChainSorted != null)
            {
                retTail.Next = rightChainSorted.Item1;
                retTail = rightChainSorted.Item2;
            }

            //string s = "Sorted sequence: ";
            //Node c = retHead;
            //while (c != null)
            //{
            //    s += c.Data;
            //    s += "*";
            //    c = c.Next;
            //}
            //Console.WriteLine(s);
            return new Tuple<Node, Node>(retHead, retTail);
        }

        private Node SelectionSort(Node head)
        { 
            Node retListHead = null;
            Node retListTail = null;
            Node remListHead = head;
            while (remListHead != null)
            {
                Node minDataNode = remListHead;
                Node minDataNodePrev = null;
                Node current = remListHead;
                while (current.Next != null)
                {
                    if (current.Next.Data.CompareTo(minDataNode.Data) < 0)
                    {
                        minDataNode = current.Next;
                        minDataNodePrev = current;
                    }
                    current = current.Next;
                }
                if (minDataNodePrev == null)
                {
                    remListHead = minDataNode.Next;
                }
                else
                {
                    minDataNodePrev.Next = minDataNode.Next;
                }
                minDataNode.Next = null;
                if (retListTail == null)
                {
                    retListHead = retListTail = minDataNode;
                }
                else
                {
                    retListTail.Next = minDataNode;
                    retListTail = retListTail.Next;
                }
            }
            return retListHead;
        }

        private Node InsertionSort(Node head)
        {
            Node retListHead = head;
            Node current = head.Next, currentPrevious = head;
            int retListLen = 1;
            while (current != null)
            {
                Node iterationPtr = retListHead, iterationPrevPtr = null;
                bool alreadyCorrectPos = true;
                for (int i = 0; i < retListLen; i++)
                {
                    if (iterationPtr.Data.CompareTo(current.Data) > 0)
                    {
                        currentPrevious.Next = current.Next;
                        current.Next = iterationPtr;
                        if (iterationPrevPtr == null)
                        {
                            retListHead = current;
                        }
                        else
                        {
                            iterationPrevPtr.Next = current;
                        }
                        alreadyCorrectPos = false;
                        break;
                    }
                    iterationPrevPtr = iterationPtr;
                    iterationPtr = iterationPtr.Next;
                }
                if (alreadyCorrectPos)
                {
                    currentPrevious = current;
                    current = current.Next;
                }
                else
                {
                    current = currentPrevious.Next;
                }
                retListLen++;
            }
            return retListHead;
        }

        private Node MergeSort(Node head)
        {
            FunctionCost funcCost = new FunctionCost();
            funcCost.NumComparisons++;
            _sortingStats.MergeSortCosts.Add(funcCost);
            if (head.Next != null)
            {
                Node mid = Mid(head);
                Node l1 = head;
                if (l1.Next != null)
                {
                    l1 =  MergeSort(head);
                }
                Node l2 = mid;
                if (l2.Next != null)
                {
                    l2 = MergeSort(mid);
                }
                return Merge(l1, l2);
            }
            else
            {
                return head;
            }
        }

        private Node Mid(Node head)
        {
            FunctionCost funcCost = new FunctionCost();
            Node slowPtr = head;
            Node fastPtr = head.Next.Next;
            funcCost.NumPropertyAccesses += 2;
            funcCost.NumComparisons++;
            if (fastPtr != null)
            {
                while (true)
                {
                    funcCost.NumPropertyAccesses += 3;
                    funcCost.NumComparisons += 2;
                    if (fastPtr.Next == null || fastPtr.Next.Next == null)
                    {
                        break;
                    }
                    slowPtr = slowPtr.Next;
                    fastPtr = fastPtr.Next.Next;
                    funcCost.NumPropertyAccesses += 3;
                }
            }

            Node tmp = slowPtr;
            slowPtr = slowPtr.Next;
            tmp.Next = null;
            funcCost.NumPropertyAccesses += 2;
            _sortingStats.MidCosts.Add(funcCost);
            return slowPtr;
        }

        private Node Merge(Node head1, Node head2)
        {
            FunctionCost funcCost = new FunctionCost();
            _sortingStats.MergeCosts.Add(funcCost);
            funcCost.NumComparisons++;
            if (head2 == null)
            {
                return head1;
            }
            funcCost.NumComparisons++;
            if (head1 == null)
            {
                return head2;
            }

            Node retListHead = head1;
            funcCost.NumPropertyAccesses++;
            Node current1 = head1.Next;
            Node current2 = head2;
            bool onList1 = true;
            funcCost.NumComparisons++;
            if (head2.Data.CompareTo(head1.Data) < 0)
            {
                retListHead = head2;
                current1 = head1;
                funcCost.NumPropertyAccesses++;
                current2 = head2.Next;
                onList1 = false;
            }
            Node retListTail = retListHead;
            while(current1 != null && current2 != null)
            {
                funcCost.NumComparisons += 3;
                if (onList1)
                {
                    funcCost.NumComparisons++;
                    if (current2.Data.CompareTo(current1.Data) < 0)
                    {
                        retListTail.Next = current2;
                        retListTail = current2;
                        funcCost.NumPropertyAccesses++;
                        current2 = current2.Next;
                        onList1 = false;
                    }
                    else
                    {
                        retListTail = current1;
                        funcCost.NumPropertyAccesses++;
                        current1 = current1.Next;
                    }
                }
                else
                {
                    funcCost.NumComparisons++;
                    if (current1.Data.CompareTo(current2.Data) < 0)
                    {
                        retListTail.Next = current1;
                        retListTail = current1;
                        funcCost.NumPropertyAccesses++;
                        current1 = current1.Next;
                        onList1 = true;
                    }
                    else
                    {
                        retListTail = current2;
                        funcCost.NumPropertyAccesses++;
                        current2 = current2.Next;
                    }
                }
            }
            if (current1 != null || current2 != null)
            {
                if (current1 != null)
                {
                    retListTail.Next = current1;
                }
                else
                {
                    retListTail.Next = current2;
                }
            }

            return retListHead;
        }
    }
}
