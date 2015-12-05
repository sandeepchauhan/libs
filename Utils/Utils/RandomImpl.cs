using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Learning.Libs.Utils
{
    public class RandomImpl
    {
        Stopwatch stopwatch;

        private int _min;

        private int _diff;

        public RandomImpl(int min, int max)
        {
            _min = min;
            _diff = max - min;
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        public int Next()
        {
            long ticks = stopwatch.ElapsedTicks % 1000;
            double fff = ((double) ticks / 1000);
            int c = 0;
            while (c++ < Int16.MaxValue) { };
            return _min + (int)(fff * _diff);
        }
    }
}
