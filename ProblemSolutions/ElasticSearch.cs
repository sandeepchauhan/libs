using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Learning.Libs.ExtensionMethods;
using System.Diagnostics;
using Learning.Libs.Utils;

namespace ProblemSolutions
{
    // POC implementation of ES for 1M text documents.
    public class ElasticSearch
    {
        private class InvertedIndex
        {
            // Dictionary which for every word keeps a bit vector (as array of longs) to tell whether
            // this word occurs in document number X or not.
            private Dictionary<string, long[]> _index = new Dictionary<string, long[]>();

            private int _numLongs;

            private int _numDocuments;

            public InvertedIndex(int numDocuments)
            {
                _numDocuments = numDocuments;
                _numLongs = numDocuments / 64;
            }

            internal void AddWord(string w, long[] bitVector)
            {
                _index.Add(w, bitVector);
            }

            public List<int> Query(string wordRequired, string wordRequiredToBeMissing)
            {
                List<int> ret = new List<int>();
                long[] bv1 = _index[wordRequired];
                long[] bv2 = _index[wordRequiredToBeMissing];
                long[] masks = LongMasks.Instance.Masks;
                for (int i = 0; i < _numLongs; i++)
                {
                    long bv = (bv1[i] ^ bv2[i]) & bv1[i]; 
                    bv.GetOnePositionsV5(ret, (i << 6), masks);
                    //foreach(int p in bv.GetOnePositionsV2())
                    //{
                    //    ret.Add((i << 6) + p);
                    //}
                }

                return ret;
            }
        }

        private int _numDocuments;

        private InvertedIndex _invertedIndex;

        public ElasticSearch(int numDocuments)
        {
            _numDocuments = numDocuments;
            _invertedIndex = new InvertedIndex(_numDocuments);
        }
        
        public static void ExecuteScenario()
        {
            ElasticSearch es = new ElasticSearch(1 << 26);
            long[] randomLongs1 = File.ReadAllLines(@"D:\gitrepos\myrepos\resources\RandomLongs3.txt").Select(x => long.Parse(x)).ToArray();
            long[] randomLongs2 = File.ReadAllLines(@"D:\gitrepos\myrepos\resources\RandomLongs4.txt").Select(x => long.Parse(x)).ToArray();
            es._invertedIndex.AddWord("Red", randomLongs1);
            es._invertedIndex.AddWord("Blue", randomLongs2);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var list = es._invertedIndex.Query("Red", "Blue");
            sw.Stop();
            Console.WriteLine("Elapsed time: " + sw.ElapsedMilliseconds);
        }
    }
}
