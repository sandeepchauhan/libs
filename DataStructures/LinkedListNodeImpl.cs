using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Libs.DataStructures
{
    public class LinkedListNodeImpl<T>
    {
        public T Data { get; set; }

        public T Data1 { get; set; }
        public T Data2 { get; set; }
        public T Data3 { get; set; }
        public T Data4 { get; set; }
        public T Data5 { get; set; }
        public T Data6 { get; set; }
        public T Data7 { get; set; }
        public T Data8 { get; set; }
        public T Data9 { get; set; }
        public T Data10 { get; set; }
        public T Data11 { get; set; }
        public T Data12 { get; set; }
        public T Data13 { get; set; }
        public T Data14 { get; set; }
        public T Data15 { get; set; }
        public T Data16 { get; set; }
        public T Data17 { get; set; }
        public T Data18 { get; set; }
        public T Data19 { get; set; }
        public T Data20 { get; set; }

        private string x;

        public LinkedListNodeImpl<T> Next { get; set; }

        public LinkedListNodeImpl(T data)
        {
            this.Data = data;
            this.x = data.ToString() + data.ToString() + data.ToString() + data.ToString() + data.ToString() + data.ToString();
            this.x += data.ToString() + data.ToString() + data.ToString() + data.ToString() + data.ToString() + data.ToString();
        }

        override public string ToString()
        {
            string s = Data.ToString();
            LinkedListNodeImpl<T> Current = Next;
            while(Current != null)
            {
                s += Current.Data;
                Current = Current.Next;
            }
            return s;
        }
    }
}
