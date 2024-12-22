public class Solution
{
    public class MaxHeapComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return y.CompareTo(x);
        }
    }

    public int LongestSubarray(int[] nums, int limit)
    {
        var minHeap = new PriorityQueue<int, int>();
        var maxHeap = new PriorityQueue<int, int>(new MaxHeapComparer());
        var numCounts = new Dictionary<int, int>();

        int maxLength = 0;
        int i = 0;

        for (int j = 0; j < nums.Length; j++)
        {
            if (!numCounts.ContainsKey(nums[j]))
            {
                numCounts[nums[j]] = 0;
            }
            numCounts[nums[j]]++;
            
            maxHeap.Enqueue(nums[j], nums[j]);
            minHeap.Enqueue(nums[j], nums[j]);

            while (maxHeap.Peek() - minHeap.Peek() > limit)
            {
                numCounts[nums[i]]--;
                if (numCounts[nums[i]] == 0)
                {
                    while (maxHeap.Count > 0 && numCounts[maxHeap.Peek()] == 0)
                    {
                        maxHeap.Dequeue();
                    }
                    while (minHeap.Count > 0 && numCounts[minHeap.Peek()] == 0)
                    {
                        minHeap.Dequeue();
                    }
                }
                i++;
            }

            maxLength = Math.Max(maxLength, j - i + 1);
        }

        return maxLength;
    }
}
