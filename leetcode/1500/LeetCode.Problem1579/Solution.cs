public class Solution
{
    public int MaxNumEdgesToRemove(int n, int[][] edges)
    {
        var alice = new DisjointSetUnion(n);
        var bob = new DisjointSetUnion(n);
        int removed = 0;

        foreach (int[] edge in edges)
        {
            if (edge[0] == 3)
            {
                bool unionAlice = alice.Union(edge[1] - 1, edge[2] - 1);
                bool unionBob = bob.Union(edge[1] - 1, edge[2] - 1);
                if (!unionAlice && !unionBob)
                {
                    removed++;
                }
            }
        }

        foreach (int[] edge in edges)
        {
            if (edge[0] == 1)
            {
                if (!alice.Union(edge[1] - 1, edge[2] - 1))
                {
                    removed++;
                }
            }
        }

        foreach (int[] edge in edges)
        {
            if (edge[0] == 2)
            {
                if (!bob.Union(edge[1] - 1, edge[2] - 1))
                {
                    removed++;
                }
            }
        }

        if (alice.Count != 1 || bob.Count != 1)
        {
            return -1;
        }

        return removed;
    }

    private class DisjointSetUnion
    {
        public int[] parent;
        public int[] rank;
        public int Count { get; private set; }

        public DisjointSetUnion(int n)
        {
            parent = new int[n];
            rank = new int[n];
            Count = n;
            for (int i = 0; i < n; i++)
            {
                parent[i] = i;
                rank[i] = 0;
            }
        }

        public int Find(int x)
        {
            if (parent[x] != x)
            {
                parent[x] = Find(parent[x]);
            }
            return parent[x];
        }

        public bool Union(int x, int y)
        {
            int rootX = Find(x);
            int rootY = Find(y);
            if (rootX == rootY)
                return false;

            if (rank[rootX] < rank[rootY])
            {
                parent[rootX] = rootY;
            }
            else if (rank[rootX] > rank[rootY])
            {
                parent[rootY] = rootX;
            }
            else
            {
                parent[rootY] = rootX;
                rank[rootX]++;
            }

            Count--;
            return true;
        }
    }
}
