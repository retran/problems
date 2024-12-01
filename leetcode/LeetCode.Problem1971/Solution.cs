public class Solution
{
    public bool ValidPath(int n, int[][] edges, int source, int destination)
    {
        if (source == destination)
        {
            return true;
        }

        if (n < 2)
        {
            return false;
        }

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

        var queue = new Queue<int>();
        queue.Enqueue(source);

        var visited = new HashSet<int>();
        visited.Add(source);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (graph.TryGetValue(current, out var adjancencyList))
            {
                foreach (var next in adjancencyList)
                {
                    if (next == destination)
                    {
                        return true;
                    }

                    if (visited.Add(next))
                    {
                        queue.Enqueue(next);
                    }
                }

            }
        }

        return false;
    }
}
