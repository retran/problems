public class Solution
{
    private const int INF = 1000000;

    public int FindTheCity(int n, int[][] edges, int distanceThreshold)
    {
        int [,] graph = new int[n, n];

        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
            {
                if (i == j)
                {
                    continue;
                }

                graph[i, j] = INF;
            }

        foreach (var edge in edges)
        {
            graph[edge[0], edge[1]] = edge[2];
            graph[edge[1], edge[0]] = edge[2];
        }

        for (int k = 0; k < n; k++)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    graph[i, j] = Math.Min(graph[i, j], graph[i, k] + graph[k, j]);
                }
            }
        }

        int maxCount = INF;
        int index = -1;
        for (int i = 0; i < n; i++)
        {
            int count = 0;
            for (int j = 0; j < n; j++)
            {
                if (graph[i, j] <= distanceThreshold)
                {
                    count++;
                }
            }
            if (count <= maxCount)
            {
                maxCount = count;
                index = i;
            }
        }

        return index;
    }
}