public class Solution
{
    private const int INF = 10000000;

    public long MinimumCost(string source, string target, char[] original, char[] changed, int[] cost)
    {
        if (source == target)
        {
            return 0;
        }

        if (string.IsNullOrEmpty(source))
        {
            return 0;
        }

        int n = 26;

        int[,] graph = new int[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
            {
                if (i != j)
                {
                    graph[i, j] = INF;
                }
            }

        for (int k = 0; k < original.Length; k++)
        {
            var i = original[k] - 'a';
            var j = changed[k] - 'a';

            // Input data can contain repeating pairs with different costs, so choose minimum
            graph[i, j] = Math.Min(cost[k], graph[i, j]);
        }

        // calculate all possible costs with Floyd-Warshall
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

        long totalCost = 0;
        for (int k = 0; k < source.Length; k++)
        {
            int i = source[k] - 'a';
            int j = target[k] - 'a';

            if (graph[i, j] == INF)
            {
                // there are no possible routes
                return -1;
            }

            totalCost += graph[i, j];
        }

        return totalCost;
    }
}