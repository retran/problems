public class Solution
{
    public int FurthestBuilding(int[] heights, int bricks, int ladders)
    {
        var heap = new PriorityQueue<int, int>();

        for (int i = 1; i < heights.Length; i++)
        {
            var height = heights[i] - heights[i - 1];

            if (height <= 0)
            {
                continue;
            }

            heap.Enqueue(height, height);
            if (heap.Count > ladders)
            {
                var lowest = heap.Dequeue();
                bricks -= lowest;
                if (bricks < 0)
                {
                    return i - 1;
                }
            }
        }

        return heights.Length - 1;
    }
}