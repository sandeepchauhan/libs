using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Learning.Libs.DataStructures.Interfaces;
using Learning.Libs.Utils;
using System.Diagnostics;

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
            /// Locations in the source text where word corresponding to this node
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
            word = word.ToLowerInvariant();
            List<int> retVal = new List<int>();
            if (_rootNode != null)
            {
                Node currNode = _rootNode;
                foreach (char c in word)
                {
                    if (currNode.childNodes == null)
                    {
                        currNode = null;
                        break;
                    }

                    int index = c - 'a';
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

        public Tuple<IEnumerable<int>, FunctionPerfData> GetCorrections(string word)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            FunctionPerfData fpd = new FunctionPerfData();

            #region Get corrections with edit distance 1
            FunctionPerfData stagePerfData = new FunctionPerfData();
            fpd.StagesPerfData.Add("GetCandidates1", stagePerfData);
            HashSet<int> ret = new HashSet<int>();
            Stopwatch sws = new Stopwatch();
            sws.Start();
            List<CandidateWord> candidates = GetCorrectionsWithEditDistanceOne(new CandidateWord(word, 0)).ToList();
            sws.Stop();
            stagePerfData.TimeTaken = sws.ElapsedMilliseconds;
            stagePerfData.OtherData.Add("Num Actual Candidates", candidates.Count);
            stagePerfData.OtherData.Add("Num Candidates Formula", "52N + 25");
            stagePerfData.OtherData.Add("Num Candidates by Formula", (52 * word.Length + 25));
            #endregion
            #region Get corrections with edit distance 2
            sws = new Stopwatch();
            sws.Start();
            stagePerfData = new FunctionPerfData();
            fpd.StagesPerfData.Add("GetCandidates2", stagePerfData);
            List<CandidateWord> copy = new List<CandidateWord>(candidates);
            foreach (CandidateWord c in copy)
            {
                candidates.AddRange(GetCorrectionsWithEditDistanceOne(c));
            }
            sws.Stop();
            stagePerfData.TimeTaken = sws.ElapsedMilliseconds;
            stagePerfData.OtherData.Add("Num Actual Candidates", candidates.Count);
            stagePerfData.OtherData.Add("Num Candidates Formula", "(52N + 25)(52N + 25)");
            stagePerfData.OtherData.Add("Num Candidates by Formula", (52 * word.Length + 25) * (52 * word.Length + 25));
            #endregion
            
            #region Get unique candidates
            sws = new Stopwatch();
            sws.Start();
            stagePerfData = new FunctionPerfData();
            fpd.StagesPerfData.Add("GetCandidatesUnique", stagePerfData);
            //HashSet<string> hs = new HashSet<string>(candidates.Select(x => x.Word));
            IEnumerable<string> hs = candidates.Select(x => x.Word).Distinct();
            stagePerfData.OtherData.Add("Unique Candidates", hs.Count());
            sws.Stop();
            stagePerfData.TimeTaken = sws.ElapsedMilliseconds;
            #endregion
            
            #region Filter valid candidates
            sws = new Stopwatch();
            sws.Start();
            stagePerfData = new FunctionPerfData();
            fpd.StagesPerfData.Add("FilterValidCandidates", stagePerfData);
            foreach(string c in hs)
            {
                var positions = this.FindWord(c);
                if(positions.Any())
                {
                    ret.Add(positions.First());
                }
            }
            sws.Stop();
            stagePerfData.TimeTaken = sws.ElapsedMilliseconds;
            #endregion

            fpd.TimeTaken = sw.ElapsedMilliseconds;
            return new Tuple<IEnumerable<int>,FunctionPerfData>(ret.AsEnumerable(), fpd);
        }

        // Assuming N as word length. Output can contain upto (26(N+1) - N + 25N + N + (N-1)) = 52N + 25 words.
        private HashSet<CandidateWord> GetCorrectionsWithEditDistanceOne(CandidateWord word)
        {
            HashSet<CandidateWord> ret = new HashSet<CandidateWord>();
            for (int i = word.StartIndex; i <= word.Word.Length; i++)
            {
                // Insertions
                foreach(char c in Constants.EnglishAlphabet)
                {
                    ret.Add(new CandidateWord(word.Word.Insert(i, c + ""), i+1));
                }
                // Replacements
                if (i != word.Word.Length)
                {
                    char[] arr = word.Word.ToCharArray();
                    foreach (char c in Constants.EnglishAlphabet)
                    {
                        if (c != word.Word[i])
                        {
                            arr[i] = c;
                            ret.Add(new CandidateWord(new string(arr), i+1));
                        }
                    }
                }
                // Deletions
                if (i != word.Word.Length)
                {
                    ret.Add(new CandidateWord(word.Word.Remove(i, 1), i));
                }
                // Transposes
                if (i < word.Word.Length - 1)
                {
                    char[] arr = word.Word.ToCharArray();
                    char tmp = arr[i];
                    arr[i] = arr[i + 1];
                    arr[i + 1] = tmp;
                    ret.Add(new CandidateWord(new string(arr), 0));
                }
            }

            return ret;
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

        private class CandidateWord
        {
            public string Word { get; set; }

            public int StartIndex { get; set; }

            public CandidateWord(string w, int i)
            {
                this.Word = w;
                this.StartIndex = i;
            }

            public override int GetHashCode()
            {
                return Word.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                if (obj != null && (obj is CandidateWord))
                {
                    return Word.Equals((obj as CandidateWord).Word);
                }

                return false;
            }
        }
    }
}
