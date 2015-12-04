using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Libs.DataStructures
{
    public class ArrayString : ArrayImpl<char>
    {
        public ArrayString(string s) : base(s.Length)
        {
            foreach(char c in s)
            {
                Add(c);
            }
        }

        public override int GetHashCode()
        {
            int hash = 0;
            unchecked
            {
                foreach (char c in this)
                {
                    hash = (17 * hash) + c;
                }
            }

            return hash;
        }

        public override bool Equals(object obj)
        {
            bool retVal = false;
            if (obj != null && obj.GetType() == typeof(ArrayString))
            {
                ArrayString s1 = obj as ArrayString;
                if (s1.Size() == this.Size())
                {
                    int i = 0;
                    for (i = 0; i < this.Size(); i++)
                    {
                        if (this._array[i] != s1._array[i])
                        {
                            break;
                        }
                    }
                    if (i == this.Size())
                    {
                        retVal = true;
                    }
                }
            }

            return retVal;
        }

        public static List<string> GetPermutations(string str)
        {
            List<string> permutations = new List<string>();
            if (str.Length > 0)
            {
                if (str.Length == 1)
                {
                    permutations.Add(str);
                }
                else
                {
                    for(int i = 0; i < str.Length; i++)
                    {
                        List<string> subPermutations = GetPermutations(str.Remove(i, 1));
                        permutations.AddRange(subPermutations.Select(x => str[i] + x));
                    }
                }
            }
            return permutations;
        }

        public static List<string> GetPermutationsIterative(string str)
        {
            List<string> permutations = new List<string>();
            if (str.Length > 0)
            {
                if (str.Length == 1)
                {
                    permutations.Add(str);
                }
                else
                {
                    List<Item> items = new List<Item>();
                    Item rootItem = new Item(items, null);
                    rootItem.Suffix = str;
                    rootItem.Length = str.Length;
                    items.Add(rootItem);
                    while(items.Any())
                    {
                        Item item = items.First();
                        if (item.ToBeUnfolded)
                        {
                            item.Unfold();
                        }
                        if (item.ToBeFolded)
                        {
                            item.FoldIntoParent();
                            items.Remove(item);
                        }
                        items.Sort();
                    }
                    permutations.AddRange(rootItem.SuffixPermutations);
                }
            }
            return permutations;
        }

        private class Item : IComparable
        {
            private List<Item> _itemsList;

            public char Prefix;

            public int Length;

            public Item ParentItem;

            public string Suffix;

            public bool ToBeUnfolded;

            public bool ToBeFolded;

            public int UnfoldCount;

            public List<string> SuffixPermutations;

            public Item(List<Item> itemsList, Item parentItem)
            {
                _itemsList = itemsList;
                this.ParentItem = parentItem;
                ToBeUnfolded = true;
                SuffixPermutations = new List<string>();
            }

            public void FoldChild(Item childItem)
            {
                this.SuffixPermutations.AddRange(childItem.SuffixPermutations.Select(x => this.Prefix + x));
                if (--this.UnfoldCount == 0)
                {
                    this.ToBeFolded = true;
                }
            }

            public void FoldIntoParent()
            {
                this.ToBeUnfolded = false;
                this.ToBeFolded = false;
                if (this.ParentItem != null)
                {
                    this.ParentItem.FoldChild(this);
                }
            }

            public void Unfold()
            {
                if (this.Suffix.Length == 0)
                {
                    this.SuffixPermutations.Add(new string(new char[] { this.Prefix }));
                    this.ToBeFolded = true;
                }
                else
                {
                    for (int i = 0; i < this.Suffix.Length; i++)
                    {
                        Item it = new Item(_itemsList, this);
                        it.Prefix = this.Suffix[i];
                        it.Suffix = this.Suffix.Remove(i, 1);
                        it.Length = it.Suffix.Length;
                        _itemsList.Add(it);
                        this.UnfoldCount++;
                    }
                }
                this.ToBeUnfolded = false;
            }

            public int CompareTo(object obj)
            {
                Item objItem = obj as Item;
                if (objItem == null)
                {
                    throw new Exception();
                }

                return this.Length.CompareTo(objItem.Length);
            }
        }
    }
}
