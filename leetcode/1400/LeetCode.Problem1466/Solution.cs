using System.Net.NetworkInformation;

public class Solution
{
    public int MinReorder(int n, int[][] connections)
    {
        var graph = new Dictionary<int, HashSet<int>>();
        var reverseGraph = new Dictionary<int, HashSet<int>>();

        for (int i = 0; i < connections.Length; i++)
        {
            if (!graph.TryGetValue(connections[i][0], out var cities))
            {
                cities = new HashSet<int>();
                graph[connections[i][0]] = cities;
            }

            cities.Add(connections[i][1]);

            if (!reverseGraph.TryGetValue(connections[i][1], out cities))
            {
                cities = new HashSet<int>();
                reverseGraph[connections[i][1]] = cities;
            }

            cities.Add(connections[i][0]);
        }

        int count = 0;
        var stack = new Stack<int>();
        bool[] visited = new bool[n];

        stack.Push(0);
        visited[0] = true;

        while (stack.Count > 0)
        {
            var current = stack.Pop();

            if (reverseGraph.TryGetValue(current, out var cities))
            {
                foreach (var city in cities)
                {
                    if (!visited[city])
                    {
                        stack.Push(city);
                        visited[city] = true;
                    }
                }
            }

            if (graph.TryGetValue(current, out cities))
            {
                foreach (var city in cities)
                {
                    if (!visited[city])
                    {
                        stack.Push(city);
                        visited[city] = true;
                        count++;
                    }
                }
            }
        }

        return count;
    }
}