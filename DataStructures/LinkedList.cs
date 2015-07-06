using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Libs.DataStructures
{
    public class LinkedList<T> : IEnumerable<T>
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

        public void Add(T data)
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

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
