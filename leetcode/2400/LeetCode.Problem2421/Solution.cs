public class Solution
{
    public int NumberOfGoodPaths(int[] vals, int[][] edges)
    {
        int n = vals.Length;
        
        List<int>[] graph = new List<int>[n];
        for (int i = 0; i < n; i++)
            graph[i] = new List<int>();

        foreach (var edge in edges)
        {
            int u = edge[0], v = edge[1];
            graph[u].Add(v);
            graph[v].Add(u);
        }

        var valToNodes = new SortedDictionary<int, List<int>>();
        for (int i = 0; i < n; i++)
        {
            if (!valToNodes.ContainsKey(vals[i]))
                valToNodes[vals[i]] = new List<int>();
            valToNodes[vals[i]].Add(i);
        }

        DisjointSetUnion dsu = new DisjointSetUnion(n);

        int ans = 0;
        ans += n;

        foreach (var kv in valToNodes)
        {
            int v = kv.Key;
            List<int> nodes = kv.Value;

            foreach (int node in nodes)
            {
                foreach (int nei in graph[node])
                {
                    if (vals[nei] <= v)
                        dsu.Union(node, nei);
                }
            }

            var count = new Dictionary<int, int>();
            foreach (int node in nodes)
            {
                int root = dsu.Find(node);
                if (!count.ContainsKey(root))
                    count[root] = 0;
                count[root]++;
            }

            foreach (var kv2 in count)
            {
                int c = kv2.Value;
                ans += (c * (c - 1)) / 2;
            }
        }

        return ans;
    }

    public class DisjointSetUnion
    {
        public int[] parent;
        public int[] rank;
        public DisjointSetUnion(int n)
        {
            parent = new int[n];
            rank = new int[n];
            for (int i = 0; i < n; i++)
            {
                parent[i] = i;
                rank[i] = 0;
            }
        }
        public int Find(int x)
        {
            if (parent[x] != x)
                parent[x] = Find(parent[x]);
            return parent[x];
        }
        public void Union(int x, int y)
        {
            int rx = Find(x), ry = Find(y);
            if (rx == ry)
                return;
            if (rank[rx] < rank[ry])
                parent[rx] = ry;
            else if (rank[rx] > rank[ry])
                parent[ry] = rx;
            else
            {
                parent[ry] = rx;
                rank[rx]++;
            }
        }
    }
}
