using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Libs.DataStructures
{
    public class CustomString
    {
        private readonly string _value;

        private readonly int _length;

        private const int PRIME = 17;

        public CustomString(string value)
        {
            this._value = value.ToLowerInvariant();
            this._length = value.Length;
        }

        public override int GetHashCode()
        {
            uint hashCode = 5381;
            for (int i = 0; i < _length; i++)
            {
                char c = _value[i];
                uint asciiVal = (uint) c;
                hashCode += (hashCode << 5) + asciiVal;
            }

            return (int) hashCode;
        }

        public override bool Equals(object obj)
        {
            bool ret = false;
            CustomString castedObj = obj as CustomString;
            if (castedObj != null && castedObj._length == this._length)
            {
                ret = true;
                for (int i = 0; i < _length; i++)
                {
                    if (this._value[i] != castedObj._value[i])
                    {
                        ret = false;
                        break;
                    }
                }
            }

            return ret;
        }
    }
}
