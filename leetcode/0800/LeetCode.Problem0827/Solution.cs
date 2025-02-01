public class Solution
{
    private readonly (int, int)[] Directions =
    [
        (-1, 0), (1, 0), (0, -1), (0, 1)
    ];

    private readonly (int, int)[] DirectionsForUnion =
    [
        (1, 0), (0, 1)
    ];

    public int LargestIsland(int[][] grid)
    {
        int rows = grid.Length;
        int cols = grid[0].Length;

        var unionFind = new UnionFind(rows * cols);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (grid[i][j] == 1)
                {
                    foreach (var (dx, dy) in DirectionsForUnion)
                    {
                        int newX = i + dx, newY = j + dy;
                        if (IsValidCell(newX, newY, rows, cols) && grid[newX][newY] == 1)
                        {
                            unionFind.Union(i * cols + j, newX * cols + newY);
                        }
                    }
                }
            }
        }

        int maxIslandSize = 0;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (grid[i][j] == 1)
                {
                    maxIslandSize = Math.Max(maxIslandSize, unionFind.GetSetSize(i * cols + j));
                }
                else
                {
                    var uniqueParents = new HashSet<int>();
                    int potentialSize = 1;

                    foreach (var (dx, dy) in Directions)
                    {
                        int newX = i + dx, newY = j + dy;
                        if (IsValidCell(newX, newY, rows, cols) && grid[newX][newY] == 1)
                        {
                            int parent = unionFind.Find(newX * cols + newY);
                            if (uniqueParents.Add(parent))
                            {
                                potentialSize += unionFind.GetSetSize(parent);
                            }
                        }
                    }
                    maxIslandSize = Math.Max(maxIslandSize, potentialSize);
                }
            }
        }

        return maxIslandSize;
    }

    private bool IsValidCell(int x, int y, int rows, int cols) => x >= 0 && x < rows && y >= 0 && y < cols;

    private class UnionFind
    {
        private int[] parent;
        private int[] size;

        public UnionFind(int n)
        {
            parent = new int[n];
            size = new int[n];
            for (int i = 0; i < n; i++)
            {
                parent[i] = i;
                size[i] = 1;
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

        public void Union(int x, int y)
        {
            int rootX = Find(x);
            int rootY = Find(y);

            if (rootX != rootY)
            {
                if (size[rootX] < size[rootY])
                {
                    parent[rootX] = rootY;
                    size[rootY] += size[rootX];
                }
                else
                {
                    parent[rootY] = rootX;
                    size[rootX] += size[rootY];
                }
            }
        }

        public int GetSetSize(int x) => size[Find(x)];
    }
}
