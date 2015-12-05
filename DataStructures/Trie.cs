using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Learning.Libs.DataStructures.Interfaces;

namespace Learning.Libs.DataStructures
{
    /// <summary>
    /// Size coming around 46 MB with 266091 nodes.
    /// </summary>
    [Serializable]
    public class Trie
    {
        /// <summary>
        /// Avg size coming as 174 bytes.
        /// </summary>
        [Serializable]
        private class Node
        {
            public static int InstanceCount = 0;

            public char Value { get; set; }

            /// <summary>
            /// Starting indexes in the source text where word corresponding to this node
            /// exists.
            /// </summary>
            public List<int> SourceIndexes { get; set; }

            public Node[] childNodes { get; set; }

            public bool IsRootNode { get; set; }

            public Node(Trie trie, bool IsRootNode = false)
            {
                this.IsRootNode = IsRootNode;
                trie._numNodes++;
            }
        }

        private Node _rootNode;

        private int _numNodes;

        public IEnumerable<int> FindWord(string word)
        {
            List<int> retVal = new List<int>();
            if (_rootNode != null)
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

                if (currNode != null && currNode.SourceIndexes != null)
                {
                    retVal = currNode.SourceIndexes;
                }
            }

            return retVal;
        }

        public IEnumerable<int> GetSuggestions(string word)
        {
            if (_rootNode == null)
            {
                return new List<int>();
            }

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

            return GetAllWordsInTree(currNode);
        }

        private IEnumerable<int> GetAllWordsInTree(Node node)
        {
            List<int> retVal = new List<int>();
            if (node != null)
            {
                if (node.SourceIndexes != null)
                {
                    retVal.AddRange(node.SourceIndexes);
                }

                if (node.childNodes != null)
                {
                    foreach (Node cn in node.childNodes)
                    {
                        retVal.AddRange(GetAllWordsInTree(cn));
                    }
                }
            }

            return retVal;
        }

        public void AddWord(string word, int indexInSourceText)
        {
            if (_rootNode == null)
            {
                _rootNode = new Node(this, true);
            }

            Node currNode = _rootNode;
            char[] arr = word.ToArray();
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
                    currNode.childNodes[index] = new Node(this);
                    currNode.childNodes[index].Value = lowerCaseChar;
                }

                currNode = currNode.childNodes[index];
            }

            if (currNode.SourceIndexes == null)
            {
                currNode.SourceIndexes = new List<int>();
            }

            currNode.SourceIndexes.Add(indexInSourceText);
        }

        public int GetNumNodes()
        {
            return _numNodes;
        }

        public int GetNumWords()
        {
            return GetAllWordsInTree(_rootNode).Count();
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
