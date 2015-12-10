using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Libs.Utils
{
    public class RandomBool
    {
        private int _boundary;

        private RandomImpl _random = new RandomImpl(0, 1000);

        public RandomBool(short probOfFalse)
        {
            _boundary = probOfFalse * 10;
        }

        public bool Next()
        {
            bool ret = true;
            int next = _random.Next();
            if (next < _boundary)
            {
                ret = false;
            }

            return ret;
        }
    }
}
