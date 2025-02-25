public class Solution
{
    public int ShortestDistance(int n, int[][] edges, int source, int destination) 
    {
        IDictionary<int, int>[] graph = new IDictionary<int, int>[n];
        for (int i = 0; i < n; i++)
        {
            graph[i] = new Dictionary<int, int>();
        }

        foreach (var edge in edges)
        {
            if (edge[2] != -1)
            {
                graph[edge[0]].Add(edge[1], edge[2]);
                graph[edge[1]].Add(edge[0], edge[2]);
            }
        }

        int[,] distances = new int[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                distances[i, j] = i == j ? 0 : int.MaxValue;
            }
        }

        var visited = new HashSet<int>();
        var queue = new PriorityQueue<(int, int), int>();
        queue.Enqueue((source, 0), 0);
        while (queue.Count > 0)
        {
            var (currentNode, currentDistance) = queue.Dequeue();
            if (visited.Contains(currentNode))
            {
                continue;
            }

            visited.Add(currentNode);

            foreach (var (neighbor, weight) in graph[currentNode])
            {
                if (currentDistance + weight < distances[source, neighbor])
                {
                    distances[source, neighbor] = currentDistance + weight;
                    queue.Enqueue((neighbor, currentDistance + weight), currentDistance + weight);
                }
            }
        }

        return distances[source, destination];
    }

    public int[][] ModifiedGraphEdges(int n, int[][] edges, int source, int destination, int target)
    {
        var currentShortestDistance = ShortestDistance(n, edges, source, destination);

        if (currentShortestDistance < target) {
            return Array.Empty<int[]>();
        }

        bool matchesTarget = currentShortestDistance == target;

        foreach (var edge in edges) 
        {
            if (edge[2] >= 0)
            {
                continue;
            }

            edge[2] = matchesTarget ? target + 1 : 1;

            if (matchesTarget)
            {
                continue;
            }

            currentShortestDistance = ShortestDistance(n, edges, source, destination);

            if (currentShortestDistance <= target)
            {
                edge[2] = target - currentShortestDistance + 1;
                matchesTarget = true;
            }
        }

        return matchesTarget ? edges : Array.Empty<int[]>();
    }
}