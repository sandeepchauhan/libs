using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemSolutions
{
    public class KthSmallestInUnionOfSortedArrays
    {
        private static void PrintKthSmallestInUnionOfTwoArrays()
        {
            int[] arr1 = new int[] { 3, 5, 5, 5, 8, 8, 100, 125 };
            int[] arr2 = new int[] { 1, 6, 6, 7, 7, 9 };
            int k = 3;
            string s1 = "";
            arr1.ToList().ForEach(x => { s1 = s1 + x + " "; });
            string s2 = "";
            arr2.ToList().ForEach(x => { s2 = s2 + x + " "; });
            Console.WriteLine(s1);
            Console.WriteLine(s2);
            int element = -1, length1 = arr1.Length, length2 = arr2.Length, elementCounter = 0;
            if (length1 == 0 || length2 == 0)
            {
                if (length1 == 0 && length2 == 0)
                {
                    throw new Exception("Both arrays can not be empty.");
                }
                if (length1 == 0)
                {
                    PrintKthSmallestInASortedArray(arr2, k);
                }
                else
                {
                    PrintKthSmallestInASortedArray(arr1, k);
                }
            }
            else
            {
                int ptr1 = 0, ptr2 = 0;
                element = (arr1[ptr1] <= arr2[ptr2]) ? arr1[ptr1++] : arr2[ptr2++];
                elementCounter++;
                while (elementCounter < k && (ptr1 < length1 || ptr2 < length2))
                {
                    if (ptr1 < length1 && (ptr2 >= length2 || arr1[ptr1] <= arr2[ptr2]))
                    {
                        if (arr1[ptr1] != element)
                        {
                            element = arr1[ptr1];
                            elementCounter++;
                        }
                        ptr1++;
                    }
                    else if (ptr2 < length2)
                    {
                        if (arr2[ptr2] != element)
                        {
                            element = arr2[ptr2];
                            elementCounter++;
                        }
                        ptr2++;
                    }
                }

                if (elementCounter < k)
                {
                    Console.WriteLine("No kth element found.");
                }
                else
                {
                    Console.WriteLine(k + "th smallest element: " + element);
                }
            }
        }

        private static void PrintKthSmallestInASortedArray(int[] arr, int k)
        {
            int ptr = 0, elementCounter = 0, length = arr.Length;
            int element = arr[ptr++];
            elementCounter++;
            while (ptr < length && elementCounter < k)
            {
                if (arr[ptr] == element)
                {
                    ptr++;
                    continue;
                }
                else
                {
                    element = arr[ptr++];
                    elementCounter++;
                }
            }
            if (elementCounter < k)
            {
                Console.WriteLine("No kth element found.");
            }
            else
            {
                //Console.WriteLine(actualK + "th smallest element: " + element);
            }
        }

    }
}
