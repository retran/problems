public class Solution
{
    private void DFS(Dictionary<string, List<string>> graph, string node, List<string> result)
    {
        if (graph.ContainsKey(node))
        {
            var neighbors = graph[node];
            neighbors.Sort();
            while (neighbors.Count > 0)
            {
                var neighbor = neighbors[0];
                neighbors.RemoveAt(0);
                DFS(graph, neighbor, result);
            }
        }

        result.Insert(0, node);
    }

    public IList<string> FindItinerary(IList<IList<string>> tickets)
    {
        var graph = new Dictionary<string, List<string>>();
        foreach (var ticket in tickets)
        {
            if (!graph.ContainsKey(ticket[0]))
            {
                graph[ticket[0]] = new List<string>();
            }

            graph[ticket[0]].Add(ticket[1]);
        }

        var result = new List<string>();
        DFS(graph, "JFK", result);
        return result;
    }
}