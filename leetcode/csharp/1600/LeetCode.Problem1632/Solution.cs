public class Solution
{
    public int[][] MatrixRankTransform(int[][] matrix)
    {
        int m = matrix.Length, n = matrix[0].Length;
        int[][] answer = new int[m][];
        for (int i = 0; i < m; i++)
        {
            answer[i] = new int[n];
        }

        int[] rank = new int[m + n];

        SortedDictionary<int, List<(int, int)>> groups = new SortedDictionary<int, List<(int, int)>>();
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                int val = matrix[i][j];
                if (!groups.ContainsKey(val))
                    groups[val] = new List<(int, int)>();
                groups[val].Add((i, j));
            }
        }

        foreach (var kv in groups)
        {
            List<(int i, int j)> cells = kv.Value;

            DisjointSetUnion uf = new DisjointSetUnion(m + n);

            foreach (var cell in cells)
            {
                int i = cell.i, j = cell.j;
                uf.Union(i, j + m);
            }

            Dictionary<int, int> groupRank = new Dictionary<int, int>();
            foreach (var cell in cells)
            {
                int i = cell.i, j = cell.j;
                int root = uf.Find(i);
                int candidate = Math.Max(rank[i], rank[j + m]);
                if (!groupRank.ContainsKey(root))
                    groupRank[root] = candidate;
                else
                    groupRank[root] = Math.Max(groupRank[root], candidate);
            }

            foreach (var cell in cells)
            {
                int i = cell.i, j = cell.j;
                int newRank = groupRank[uf.Find(i)] + 1;
                answer[i][j] = newRank;
                rank[i] = newRank;
                rank[j + m] = newRank;
            }
        }

        return answer;
    }

    public class DisjointSetUnion
    {
        public int[] parent;
        public DisjointSetUnion(int n)
        {
            parent = new int[n];
            for (int i = 0; i < n; i++)
            {
                parent[i] = i;
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
            if (rx != ry)
                parent[rx] = ry;
        }
    }
}
