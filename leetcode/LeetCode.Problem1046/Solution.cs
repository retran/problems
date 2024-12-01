class MaxHeapComparer : IComparer<int>
{
    public int Compare(int x, int y)
    {
        return y.CompareTo(x);
    }
}

public class Solution
{
    public int LastStoneWeight(int[] stones)
    {
        var heap = new PriorityQueue<int, int>(new MaxHeapComparer());

        foreach (var stone in stones)
        {
            heap.Enqueue(stone, stone);
        }

        while (heap.Count > 1)
        {
            var x = heap.Dequeue();
            var y = heap.Dequeue();

            if (x == y)
            {
                continue;
            }

            int min = Math.Min(x, y);
            int max = Math.Max(x, y);
            int newStone = max - min;
            heap.Enqueue(newStone, newStone);
        }

        if (heap.Count == 0)
        {
            return 0;
        }
        
        return heap.Dequeue();
    }
}