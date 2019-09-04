using System.Collections.Generic;
using System.Linq;
using Answer;
using NUnit.Framework;

namespace Tests
{
    public class SolutionTests
    {
        [TestCase(
            new[] { 1, 4, 5 },
            new[] { 1, 3, 4 },
            new[] { 2, 6 },
            ExpectedResult = new[] { 1, 1, 2, 3, 4, 4, 5, 6 }
        )]
        [TestCase(
            ExpectedResult = new int[0]
        )]
        [TestCase(
            new int[0],
            ExpectedResult = new int[0]
        )]
        [TestCase(
            new int[0],
            new int[] { 1 },
            ExpectedResult = new int[] { 1 }
        )]
        public int[] MergeKLists(params int[][] lists)
        {
            var sut = new Solution();
            var input = lists.Select(list => GenerateLinkedList(list)).ToArray();
            return GenerateArray(sut.MergeKLists(input));
        }

        public ListNode GenerateLinkedList(int[] list)
        {
            ListNode head = null;
            ListNode last = null;

            foreach (int num in list)
            {
                var node = new ListNode(num);
                if (head == null)
                {
                    head = node;
                    last = node;
                }
                last.next = node;
                last = node;
            }

            return head;
        }

        public int[] GenerateArray(ListNode head)
        {
            List<int> result = new List<int>();

            var temp = head;
            while (temp != null)
            {
                result.Add(temp.val);
                temp = temp.next;
            }

            return result.ToArray();
        }
    }
}