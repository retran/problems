public class Solution
{
    private record Entry(int Value, int ListIndex, int Index);

    public int[] SmallestRange(IList<IList<int>> nums)
    {
        var minHeap = new PriorityQueue<Entry, int>();
        int currentMax = int.MinValue;

        for (int listIndex = 0; listIndex < nums.Count; listIndex++)
        {
            int value = nums[listIndex][0];
            var entry = new Entry(value, listIndex, 0);
            minHeap.Enqueue(entry, value);
            currentMax = Math.Max(currentMax, value);
        }

        int bestLeft = minHeap.Peek().Value;
        int bestRight = currentMax;

        while (minHeap.Count == nums.Count)
        {
            Entry entry = minHeap.Dequeue();
            int listIndex = entry.ListIndex;
            int nextIndex = entry.Index + 1;

            if (nextIndex < nums[listIndex].Count)
            {
                int nextValue = nums[listIndex][nextIndex];
                var nextEntry = new Entry(nextValue, listIndex, nextIndex);
                minHeap.Enqueue(nextEntry, nextValue);
                currentMax = Math.Max(currentMax, nextValue);
                int currentMin = minHeap.Peek().Value;

                if (currentMax - currentMin < bestRight - bestLeft)
                {
                    bestLeft = currentMin;
                    bestRight = currentMax;
                }
            }
        }

        return [bestLeft, bestRight];
    }
}
