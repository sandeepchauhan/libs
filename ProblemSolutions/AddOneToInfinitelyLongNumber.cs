using Learning.Libs.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemSolutions
{
    public static class AddOneToInfinitelyLongNumber
    {
        public static void Do()
        {
            LinkedListNodeImpl<int> head = null;
            LinkedListNodeImpl<int> tail = null;
            int[] arr = new int[] { 9, 9, 9 };
            foreach (int i in arr)
            {
                if (head == null)
                {
                    head = new LinkedListNodeImpl<int>(i);
                    tail = head;
                }
                else
                {
                    tail.Next = new LinkedListNodeImpl<int>(i);
                    tail = tail.Next;
                }
            }
            Console.WriteLine(head);
            LinkedListNodeImpl<int> lastNonNine = null;
            LinkedListNodeImpl<int> current = head;
            while (current != null)
            {
                if (current.Data != 9)
                {
                    lastNonNine = current;
                }
                current = current.Next;
            }
            if (lastNonNine == null)
            {
                LinkedListNodeImpl<int> newHead = new LinkedListNodeImpl<int>(lastNonNine.Data);
                newHead.Next = head;
                head = newHead;
                lastNonNine = head;
            }
            lastNonNine.Data++;
            current = lastNonNine.Next;
            while (current != null)
            {
                current.Data = 0;
                current = current.Next;
            }
            Console.WriteLine(head);
        }
    }
}
