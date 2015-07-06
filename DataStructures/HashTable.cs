using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Libs.DataStructures
{
    public class HashTable<T1, T2>
    {
        private class Node
        {
            public T1 Key { get; set; }

            public T2 Data { get; set; }

            public Node(T1 key, T2 data)
            {
                this.Key = key;
                this.Data = data;
            }
        }

        private List<Node>[] _buckets;

        private int[] BUCKET_COUNTS;

        private int _size;

        private int _bucketCountsArrayIndex;

        private bool _resizing = false;

        public HashTable()
        {
            BUCKET_COUNTS = new int[] { 17, 61, 131, 211, 461, 971, 1973, 4111 };
            _bucketCountsArrayIndex = 7;
            _buckets = new List<Node>[CurrentBucketsCount];
        }

        private int CurrentBucketsCount
        {
            get
            {
                return BUCKET_COUNTS[_bucketCountsArrayIndex];
            }
        }

        private int GetTargetBucket(T1 key)
        {
            int hashCode = key.GetHashCode();
            if (hashCode < 0)
            {
                hashCode *= -1;
            }

            return hashCode % CurrentBucketsCount;
        }

        private void Resize(bool increaseSpace)
        {
            if (!_resizing)
            {
                _resizing = true;
                if (increaseSpace)
                {
                    if (_bucketCountsArrayIndex != (BUCKET_COUNTS.Length - 1))
                    {
                        _bucketCountsArrayIndex++;
                        var temp = _buckets;
                        _buckets = new List<Node>[CurrentBucketsCount];
                        foreach (List<Node> list in temp)
                        {
                            if (list != null)
                            {
                                foreach (Node n in list)
                                {
                                    this.TryAdd(n.Key, n.Data);
                                }
                            }
                        }
                    }
                }
                _resizing = false;
            }
        }

        public bool TryAdd(T1 key, T2 data)
        {
            if (LoadFactor > 2)
            {
                Resize(true);
            }

            bool retVal = true;
            int targetBucket = GetTargetBucket(key);
            if (_buckets[targetBucket] == null)
            {
                _buckets[targetBucket] = new List<Node>();
                _buckets[targetBucket].Add(new Node(key, data));
            }
            else
            {
                foreach(Node n in _buckets[targetBucket])
                {
                    if (n.Key.Equals(key))
                    {
                        retVal = false;
                        break;
                    }
                }

                if (retVal)
                {
                    _buckets[targetBucket].Add(new Node(key, data));
                }
            }

            if (retVal)
            {
                _size++;
            }

            return retVal;
        }

        public T2 GetData(T1 key)
        {
            T2 retVal = default(T2);
            int targetBucket = GetTargetBucket(key);
            if (_buckets[targetBucket] != null)
            {
                foreach (Node n in _buckets[targetBucket])
                {
                    if (n.Key.Equals(key))
                    {
                        retVal = n.Data;
                        break;
                    }
                }
            }

            return retVal;
        }

        public int Size
        {
            get
            {
                return _size;
            }
        }

        public float LoadFactor
        {
            get
            {
                return _size / CurrentBucketsCount;
            }
        }
    }
}
