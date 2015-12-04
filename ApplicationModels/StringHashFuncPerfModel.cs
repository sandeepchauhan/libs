using Learning.Libs.DataStructures.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationModels
{
    public class StringHashFuncPerfModel
    {
        public StringHashAlgo HashAlgo { get; set; }

        public short NumCollisions { get; set; }
    }
}
