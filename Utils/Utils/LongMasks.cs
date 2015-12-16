using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Libs.Utils
{
    public class LongMasks
    {
        private long[] _masks = new long[64];

        private static LongMasks _instance = null;

        public static LongMasks Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LongMasks();
                }

                return _instance;
            }
        }

        private LongMasks()
        {
            int bitNumber = 64;
            long mask = 1;
            while (bitNumber >= 1)
            {
                _masks[bitNumber - 1] = mask;
                mask = mask << 1;
                bitNumber--;
            }
        }

        public long[] Masks
        {
            get
            {
                return _masks;
            }
        }
    }
}
