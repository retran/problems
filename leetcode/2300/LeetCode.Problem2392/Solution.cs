public class Solution
{
    public int[] TopologicalSort(int n, int[][] edges)
    {
        var graph = new Dictionary<int, List<int>>();
        var inDegree = new int[n];
        
        foreach (var edge in edges)
        {
            if (!graph.ContainsKey(edge[0]))
            {
                graph[edge[0]] = new List<int>();
            }

            graph[edge[0]].Add(edge[1]);
            inDegree[edge[1] - 1]++;
        }

        var queue = new Queue<int>();
        for (int i = 0; i < n; i++)
        {
            if (inDegree[i] == 0)
            {
                queue.Enqueue(i + 1);
            }
        }

        var result = new List<int>();
        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            result.Add(node);
            if (graph.ContainsKey(node))
            {
                foreach (var neighbor in graph[node])
                {
                    inDegree[neighbor - 1]--;
                    if (inDegree[neighbor - 1] == 0)
                    {
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        return result.Count == n ? result.ToArray() : new int[0];
    }

    public int[][] BuildMatrix(int k, int[][] rowConditions, int[][] colConditions)
    {
        int[] rowNumbers = TopologicalSort(k, rowConditions);
        int[] colNumbers = TopologicalSort(k, colConditions);

        if (rowNumbers.Length != k || colNumbers.Length != k)
        {
            return new int[0][];
        }

        int[][] matrix = new int[k][];

        for (int i = 0; i < k; i++)
        {
            matrix[i] = new int[k];
        }

        Dictionary<int, int> rowMap = new Dictionary<int, int>();
        Dictionary<int, int> colMap = new Dictionary<int, int>();

        for (int i = 0; i < k; i++)
        {
            rowMap[rowNumbers[i]] = i;
            colMap[colNumbers[i]] = i;
        }

        for (int i = 0; i < k; i++)
        {
            int row = rowMap[i + 1];
            int col = colMap[i + 1];
            matrix[row][col] = i + 1;
        }

        return matrix;
    }
}