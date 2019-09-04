using System.Collections.Generic;

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

            var minHeap = new MinHeap(lists);
            ListNode min = minHeap.PopMin();
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
                min = minHeap.PopMin();
            }

            return result;
        }

        private class MinHeap
        {
            private List<ListNode> _minheap = new List<ListNode>();
            public MinHeap(ListNode[] lists)
            {
                foreach (ListNode node in lists)
                {
                    if (node != null)
                    {
                        Add(node);
                    }
                }
            }

            private void Add(ListNode node)
            {
                if (_minheap.Count == 0)
                {
                    _minheap.Add(node);
                }
                else
                {
                    var index = _minheap.Count;
                    _minheap.Add(node);
                    var parentIndex = GetParentIndex(index);

                    while (parentIndex != null)
                    {
                        if (_minheap[parentIndex.Value].val > _minheap[index].val)
                        {
                            Swap(index, parentIndex.Value);
                            index = parentIndex.Value;
                            parentIndex = GetParentIndex(index);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            public ListNode PopMin()
            {
                if (_minheap.Count == 0)
                {
                    return null;
                }
                var min = _minheap[0];

                if (min.next != null)
                {
                    _minheap[0] = min.next;
                }
                else if (_minheap.Count == 1)
                {
                    _minheap.Clear();
                }
                else
                {
                    var temp = _minheap[_minheap.Count - 1];
                    _minheap.RemoveAt(_minheap.Count - 1);
                    _minheap[0] = temp;
                }

                if (_minheap.Count > 1)
                {
                    (int, int) GetChildIndexes(int parentIndex) =>
                        ((parentIndex * 2) + 1, (parentIndex * 2) + 2);

                    (int?, int?) GetChildValues(int parentIndex)
                    {
                        (int a, int b) = GetChildIndexes(parentIndex);
                        return (
                            a < _minheap.Count ? _minheap[a].val : (int?)null,
                            b < _minheap.Count ? _minheap[b].val : (int?)null
                        );
                    };

                    var indexToBubbleDown = 0;
                    var valueToBubbleDown = _minheap[indexToBubbleDown].val;

                    (int leftChildIndex, int rightChildIndex) = GetChildIndexes(indexToBubbleDown);
                    (int? leftVal, int? rightVal) = GetChildValues(indexToBubbleDown);

                    while (leftVal.HasValue && leftVal < valueToBubbleDown ||
                        rightVal.HasValue && rightVal < valueToBubbleDown)
                    {
                        int indexToSwap = rightVal.HasValue && rightVal < leftVal ?
                            rightChildIndex : leftChildIndex;

                        Swap(indexToSwap, indexToBubbleDown);
                        indexToBubbleDown = indexToSwap;

                        (leftChildIndex, rightChildIndex) = GetChildIndexes(indexToBubbleDown);
                        (leftVal, rightVal) = GetChildValues(indexToBubbleDown);
                    }
                }

                return min;
            }

            private void Swap(int indexA, int indexB)
            {
                var temp = _minheap[indexA];
                _minheap[indexA] = _minheap[indexB];
                _minheap[indexB] = temp;
            }

            private int? GetParentIndex(int index) =>
                index == 0 ? (int?)null : (index - 1) / 2;
        }
    }
}
