public class Solution
{
    public int[] ClosestNode(int n, int[][] edges, int[][] queries)
    {
        var graph = new List<int>[n];
        for (int i = 0; i < n; i++)
        {
            graph[i] = new List<int>();
        }

        foreach (var edge in edges)
        {
            graph[edge[0]].Add(edge[1]);
            graph[edge[1]].Add(edge[0]);
        }

        var distances = new int[n][];
        var result = new int[queries.Length];

        for (int i = 0; i < queries.Length; i++)
        {
            int start = queries[i][0];
            int end = queries[i][1];
            int node = queries[i][2];

            if (distances[node] == null)
            {
                distances[node] = ComputeDistances(n, graph, node);
            }

            int minDistance = int.MaxValue;
            int closestNode = -1;

            foreach (int pathNode in GetPath(graph, start, end))
            {
                int distance = distances[node][pathNode];
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestNode = pathNode;
                }
            }

            result[i] = closestNode;
        }

        return result;
    }

    private static int[] ComputeDistances(int n, List<int>[] graph, int start)
    {
        var dist = new int[n];
        Array.Fill(dist, int.MaxValue);
        var queue = new Queue<int>();

        dist[start] = 0;
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            int node = queue.Dequeue();
            foreach (int neighbor in graph[node])
            {
                if (dist[neighbor] == int.MaxValue)
                {
                    dist[neighbor] = dist[node] + 1;
                    queue.Enqueue(neighbor);
                }
            }
        }

        return dist;
    }

    private static List<int> GetPath(List<int>[] graph, int start, int end)
    {
        var path = new List<int>();
        var queue = new Queue<(int Node, List<int> Path)>();
        var visited = new HashSet<int>();

        queue.Enqueue((start, new List<int> { start }));

        while (queue.Count > 0)
        {
            var (currentNode, currentPath) = queue.Dequeue();

            if (currentNode == end)
            {
                return currentPath;
            }

            if (visited.Contains(currentNode)) continue;
            visited.Add(currentNode);

            foreach (var neighbor in graph[currentNode])
            {
                if (!visited.Contains(neighbor))
                {
                    var newPath = new List<int>(currentPath) { neighbor };
                    queue.Enqueue((neighbor, newPath));
                }
            }
        }

        return path;
    }
}
