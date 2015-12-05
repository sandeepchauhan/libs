using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Libs.Utils
{
    public static class UtilityMethods
    {
        public static int[] ShuffledArrayOfFirstNNaturalNumbers(int n)
        {
            int[] arr = new int[n];
            int i = 0;
            while(i < n)
            {
                arr[i] = i + 1;
                i++;
            }

            RandomImpl r = new RandomImpl(0, n);
            int c = 0;
            while (c++ <= n)
            {
                int i1 = r.Next();
                int i2 = r.Next();
                SwapArrayElements(arr, i1, i2);
            }

            return arr;
        }

        private static void SwapArrayElements(int[] arr, int i1, int i2)
        {
            int temp = arr[i1];
            arr[i1] = arr[i2];
            arr[i2] = temp;
        }
    }
}
