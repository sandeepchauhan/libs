using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Learning.Libs.ExtensionMethods;
using Learning.Libs.DataStructures.Interfaces;

namespace Learning.Libs.DataStructures
{
    /// <summary>
    /// Size coming around 46 MB with 266091 nodes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class Trie<T> : IStringDictionary<T>
    {
        private List<Tuple<string, T>> _dataList = new List<Tuple<string, T>>();

        /// <summary>
        /// Avg size coming as 174 bytes.
        /// </summary>
        [Serializable]
        public class Node
        {
            public static int InstanceCount = 0;

            public char Value { get; set; }

            public int ListIndex { get; set; }

            public Node[] childNodes { get; set; }

            public bool IsRootNode { get; set; }

            public Node(bool IsRootNode = false)
            {
                this.IsRootNode = IsRootNode;
                this.ListIndex = -1;
                InstanceCount++;
            }
        }

        private Node _rootNode;

        public Trie()
        {
            _rootNode = new Node(true);
        }

        public T GetData(string word)
        {
            Node currNode = _rootNode;
            char[] arr = word.ToArray();
            T retVal = default(T);
            foreach (char c in arr)
            {
                if (currNode.childNodes == null)
                {
                    currNode = null;
                    break;
                }

                char lowerCaseChar = Char.ToLowerInvariant(c);
                int index = lowerCaseChar - 'a';
                if (currNode.childNodes[index] == null)
                {
                    currNode = null;
                    break;
                }

                currNode = currNode.childNodes[index];
            }

            if (currNode != null && currNode.ListIndex != -1)
            {
                retVal = _dataList[currNode.ListIndex].Item2;
            }

            return retVal;
        }

        public IEnumerable<string> GetSuggestions(string word)
        {
            Node currNode = _rootNode;
            char[] arr = word.ToArray();
            foreach (char c in arr)
            {
                if (currNode.childNodes == null)
                {
                    currNode = null;
                    break;
                }

                char lowerCaseChar = Char.ToLowerInvariant(c);
                int index = lowerCaseChar - 'a';
                if (currNode.childNodes[index] == null)
                {
                    currNode = null;
                    break;
                }

                currNode = currNode.childNodes[index];
            }

            return GetWords(currNode);
        }

        private List<string> GetWords(Node node)
        {
            if (node == null)
            {
                return new List<string>();
            }

            List<string> words = new List<string>();
            if (node.ListIndex != -1)
            {
                words.Add(_dataList[node.ListIndex].Item1);
            }

            if (node.childNodes != null)
            {
                foreach(Node cn in node.childNodes)
                {   
                    words.AddRange(GetWords(cn));
                }
            }

            return words;
        }

        public bool TryAddWord(string word, T data)
        {
            Node currNode = _rootNode;
            char[] arr = word.ToArray();
            bool wordAlreadyPresent = true;
            foreach(char c in arr)
            {
                if (currNode.childNodes == null)
                {
                    currNode.childNodes = new Node[26];
                }

                char lowerCaseChar = Char.ToLowerInvariant(c);
                int index = lowerCaseChar - 'a';
                if (currNode.childNodes[index] == null)
                {
                    currNode.childNodes[index] = new Node();
                    currNode.childNodes[index].Value = lowerCaseChar;
                    wordAlreadyPresent = false;
                }

                currNode = currNode.childNodes[index];
            }

            if (wordAlreadyPresent && currNode.ListIndex == -1)
            {
                wordAlreadyPresent = false;
            }

            if (!wordAlreadyPresent)
            {
                _dataList.Add(new Tuple<string, T>(word, data));
                currNode.ListIndex = _dataList.Count - 1;
            }

            return !wordAlreadyPresent;
        }

        public int GetNumNodes()
        {
            return Node.InstanceCount;
        }

        public int GetNumWords()
        {
            return _dataList.Count;
        }

        public string GetStats()
        {
            string stats = "Trie";
            stats += "\n" + "Number of words: " + GetNumWords();
            stats += "\n" + "Number of nodes: " + GetNumNodes();
            return stats;
        }
    }
}
