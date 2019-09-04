namespace Answer
{
    /*
    Merge k sorted linked lists and return it as one sorted list.
    Analyze and describe its complexity.
    */
    public class Solution
    {
        public ListNode MergeKLists(ListNode[] lists)
        {
            ListNode result = null;
            ListNode last = null;

            ListNode min = PopMin(lists);
            while (min != null)
            {
                if (result == null)
                {
                    result = min;
                    last = min;
                }
                else
                {
                    last.next = min;
                    last = min;
                    last.next = null;
                }
                min = PopMin(lists);
            }

            return result;
        }

        private ListNode PopMin(ListNode[] lists)
        {
            int? minIndex = null;

            for (int i = 0; i < lists.Length; i++)
            {
                if (lists[i] == null)
                {
                    continue;
                }
                if (!minIndex.HasValue || lists[i].val < lists[minIndex.Value].val)
                {
                    minIndex = i;
                }
            }

            if (minIndex.HasValue)
            {
                var minNode = lists[minIndex.Value];
                lists[minIndex.Value] = minNode?.next;
                return minNode;
            }
            return null;
        }
    }
}
