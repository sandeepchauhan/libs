using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Libs.DataStructures
{
    public class FunctionPerfData
    {
        public long TimeTaken { get; set; }

        public Dictionary<string, object> OtherData { get; set; }
        
        public Dictionary<string, FunctionPerfData> StagesPerfData { get; set; }

        public FunctionPerfData()
        {
            this.OtherData = new Dictionary<string, object>();
            this.StagesPerfData = new Dictionary<string, FunctionPerfData>();
        }
    }
}
