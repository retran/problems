public class Solution 
{
    public int MinCostToSupplyWater(int n, int[] wells, int[][] pipes) 
    {
        List<int[]> edges = new List<int[]>();

        for (int i = 0; i < n; i++) {
            edges.Add(new int[]{0, i + 1, wells[i]});
        }

        foreach (var pipe in pipes) {
            edges.Add(pipe);
        }

        edges.Sort((a, b) => a[2].CompareTo(b[2]));

        UnionFind uf = new UnionFind(n + 1);
        int minCost = 0;

        foreach (var edge in edges) {
            int house1 = edge[0];
            int house2 = edge[1];
            int cost = edge[2];

            if (uf.Union(house1, house2)) {
                minCost += cost;
            }
        }

        return minCost;
    }
}

public class UnionFind {
    private int[] parent;
    private int[] rank;

    public UnionFind(int size) {
        parent = new int[size];
        rank = new int[size];
        for (int i = 0; i < size; i++) {
            parent[i] = i;
            rank[i] = 1;
        }
    }

    public int Find(int x) {
        if (x != parent[x]) {
            parent[x] = Find(parent[x]);
        }
        return parent[x];
    }

    public bool Union(int x, int y) {
        int rootX = Find(x);
        int rootY = Find(y);

        if (rootX != rootY) {
            if (rank[rootX] > rank[rootY]) {
                parent[rootY] = rootX;
            } else if (rank[rootX] < rank[rootY]) {
                parent[rootX] = rootY;
            } else {
                parent[rootY] = rootX;
                rank[rootX]++;
            }
            return true;
        }

        return false;
    }
}
