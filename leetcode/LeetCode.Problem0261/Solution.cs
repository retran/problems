public class Solution
{
    public bool ValidTree(int n, int[][] edges)
    {
        var graph = new Dictionary<int, IList<int>>();

        for (int k = 0; k < edges.Length; k++)
        {
            int i = edges[k][0];
            int j = edges[k][1];
            if (!graph.TryGetValue(i, out var list))
            {
                list = new List<int>();
                graph[i] = list;
            }

            list.Add(j);

            if (!graph.TryGetValue(j, out list))
            {
                list = new List<int>();
                graph[j] = list;
            }

            list.Add(i);
        }

        var visited = new HashSet<int>();
        var queue = new Queue<(int, int)>();

        queue.Enqueue((0, 0));
        visited.Add(0);

        while (queue.Count > 0)
        {
            var (current, parent) = queue.Dequeue();

            if (graph.TryGetValue(current, out var list))
            {
                foreach (var next in list)
                {
                    if (next == parent)
                    {
                        continue;
                    }

                    if (!visited.Add(next))
                    {
                        return false;
                    }

                    queue.Enqueue((next, current));
                }
            }
        }

        return visited.Count == n;
    }
}