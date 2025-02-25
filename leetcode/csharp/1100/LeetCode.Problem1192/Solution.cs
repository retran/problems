public class Solution
{
    public IList<IList<int>> CriticalConnections(int n, IList<IList<int>> connections)
    {
        var graph = new ISet<int>[n];
        for (int i = 0; i < n; i++)
        {
            graph[i] = new HashSet<int>();
        }

        foreach (var connection in connections)
        {
            int u = connection[0], v = connection[1];
            graph[u].Add(v);
            graph[v].Add(u);
        }

        var discovered = new int[n];
        var lowlink = new int[n];
        var visited = new bool[n];

        int time = 0;
        var bridges = new List<IList<int>>();

        void DFS(int node, int parent)
        {
            time++;
            discovered[node] = time;
            lowlink[node] = time;
            visited[node] = true;
            
            foreach (var neighbor in graph[node])
            {
                if (neighbor == parent)
                    continue;
                
                if (!visited[neighbor])
                {
                    DFS(neighbor, node);

                    lowlink[node] = Math.Min(lowlink[node], lowlink[neighbor]);
                    
                    if (lowlink[neighbor] > discovered[node])
                    {
                        bridges.Add(new List<int> { node, neighbor });
                    }
                }
                else
                {
                    lowlink[node] = Math.Min(lowlink[node], discovered[neighbor]);
                }
            }
        }

        for (int i = 0; i < n; i++)
        {
            if (!visited[i])
            {
                DFS(i, -1);
            }
        }

        return bridges;
    }
}
