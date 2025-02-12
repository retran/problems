public class Solution
{
    public int MagnificentSets(int n, int[][] edges)
    {
        var graph = BuildGraph(n, edges);

        int answer = 0;
        int[] colors = new int[n + 1];
        for (int i = 1; i <= n; i++)
        {
            colors[i] = -1;
        }

        for (int i = 1; i <= n; i++)
        {
            if (colors[i] == -1)
            {
                var (isBipartite, component) = GetBipartiteComponent(graph, colors, i);
                if (!isBipartite)
                {
                    return -1;
                }

                answer += ComputeMaxPartitionOfCopmponent(n, graph, component);
            }
        }
        return answer;
    }

    private int ComputeMaxPartitionOfCopmponent(int n, List<int>[] graph, IEnumerable<int> component)
    {
        int maxGroups = 0;
        foreach (int start in component)
        {
            int groups = GetMaxDistance(start, graph, n) + 1;
            maxGroups = Math.Max(maxGroups, groups);
        }

        return maxGroups;
    }

    private static (bool, IEnumerable<int>) GetBipartiteComponent(List<int>[] graph, int[] colors, int start)
    {
        var component = new List<int>();
        var queue = new Queue<int>();
        queue.Enqueue(start);
        colors[start] = 0;
        component.Add(start);
        while (queue.Count > 0)
        {
            int cur = queue.Dequeue();
            foreach (int neighbour in graph[cur])
            {
                if (colors[neighbour] == -1)
                {
                    colors[neighbour] = 1 - colors[cur];
                    queue.Enqueue(neighbour);
                    component.Add(neighbour);
                }
                else if (colors[neighbour] == colors[cur])
                {
                    return (false, Enumerable.Empty<int>());
                }
            }
        }

        return (true, component);
    }

    private static List<int>[] BuildGraph(int n, int[][] edges)
    {
        var graph = new List<int>[n + 1];
        for (int i = 1; i <= n; i++)
        {
            graph[i] = new List<int>();
        }

        foreach (int[] edge in edges)
        {
            int u = edge[0], v = edge[1];
            graph[u].Add(v);
            graph[v].Add(u);
        }

        return graph;
    }

    private int GetMaxDistance(int start, List<int>[] graph, int n)
    {
        int[] distances = new int[n + 1];
        for (int i = 1; i <= n; i++)
        {
            distances[i] = -1;
        }

        Queue<int> queue = new Queue<int>();

        queue.Enqueue(start);

        distances[start] = 0;
        int maxDistance = 0;

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            maxDistance = Math.Max(maxDistance, distances[current]);
            foreach (int neighbour in graph[current])
            {
                if (distances[neighbour] == -1)
                {
                    distances[neighbour] = distances[current] + 1;
                    queue.Enqueue(neighbour);
                }
            }
        }
        return maxDistance;
    }
}
