public class Solution
{
    public int[][] MinScore(int[][] grid)
    {
        int n = grid.Length;
        int m = grid[0].Length;
        var inDegree = new int[n * m];
        var graph = new List<int>[n * m];

        for (int i = 0; i < n * m; i++)
        {
            graph[i] = new List<int>();
        }

        void AddConnection(int from, int to)
        {
            graph[from].Add(to);
            inDegree[to]++;
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                int key = i * m + j;

                for (int k = 0; k < n; k++)
                {
                    if (k != i)
                    {
                        int nextKey = k * m + j;
                        if (grid[i][j] < grid[k][j])
                        {
                            AddConnection(key, nextKey);
                        }
                    }
                }

                for (int k = 0; k < m; k++)
                {
                    if (k != j)
                    {
                        int nextKey = i * m + k;
                        if (grid[i][j] < grid[i][k])
                        {
                            AddConnection(key, nextKey);
                        }
                    }
                }
            }
        }

        var queue = new Queue<int>();
        for (int i = 0; i < n * m; i++)
        {
            if (inDegree[i] == 0)
            {
                queue.Enqueue(i);
            }
        }

        int count = 0;

        while (queue.Count > 0)
        {
            count++;
            int size = queue.Count;

            while (size > 0)
            {
                int current = queue.Dequeue();
                int i = current / m;
                int j = current % m;
                grid[i][j] = count;

                foreach (var neighbor in graph[current])
                {
                    inDegree[neighbor]--;
                    if (inDegree[neighbor] == 0)
                    {
                        queue.Enqueue(neighbor);
                    }
                }

                size--;
            }
        }

        return grid;
    }
}
