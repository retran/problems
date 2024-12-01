class MaxHeapComparer : IComparer<(int, int)>
{
    public int Compare((int, int) x, (int, int) y)
    {
        if (x.Item2 == y.Item2)
        {
            return x.Item1.CompareTo(y.Item1);
        }

        return x.Item2.CompareTo(y.Item2);
    }
}

public class Solution 
{
    public int[] KWeakestRows(int[][] mat, int k) 
    {
        var heap = new PriorityQueue<int, (int, int)>(new MaxHeapComparer());

        for (int i = 0; i < mat.Length; i++)
        {
            int soldiers = 0;
            for (int j = 0; j < mat[i].Length; j++)
            {
                if (mat[i][j] == 1)
                {
                    soldiers++;
                }
            }
            heap.Enqueue(i, (i, soldiers));
        }

        var weakestRows = new int[k];

        for (int i = 0; i < k; i++)
        {
            weakestRows[i] = heap.Dequeue();
        }

        return weakestRows;
    }
}