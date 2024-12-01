public class Solution
{
    enum Sides : int
    {
        Top = 0,
        Right = 1,
        Bottom = 2,
        Left = 3
    }

    public int RegionsBySlashes(string[] grid)
    {
        int n = grid.Length;
        UnionFind sets = new UnionFind(4 * n * n);

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                int root = 4 * (i * n + j);
                char c = grid[i][j];

                if (c == '/')
                {
                    // Union the top-right to bottom-left
                    sets.Union(root + (int)Sides.Top, root + (int)Sides.Left);
                    sets.Union(root + (int)Sides.Right, root + (int)Sides.Bottom);
                }
                else if (c == '\\')
                {
                    // Union the top-left to bottom-right
                    sets.Union(root + (int)Sides.Top, root + (int)Sides.Right);
                    sets.Union(root + (int)Sides.Left, root + (int)Sides.Bottom);
                }
                else
                {
                    // Union all sides within the same cell
                    sets.Union(root + (int)Sides.Top, root + (int)Sides.Right);
                    sets.Union(root + (int)Sides.Right, root + (int)Sides.Bottom);
                    sets.Union(root + (int)Sides.Bottom, root + (int)Sides.Left);
                }

                // Union with the right cell
                if (j + 1 < n)
                {
                    sets.Union(root + (int)Sides.Right, 4 * (i * n + (j + 1)) + (int)Sides.Left);
                }

                // Union with the bottom cell
                if (i + 1 < n)
                {
                    sets.Union(root + (int)Sides.Bottom, 4 * ((i + 1) * n + j) + (int)Sides.Top);
                }
            }
        }

        int regions = 0;
        for (int i = 0; i < 4 * n * n; i++)
        {
            if (sets.Find(i) == i)
            {
                regions++;
            }
        }

        return regions;
    }

    public class UnionFind
    {
        private int[] parents;
        private int[] ranks;

        public UnionFind(int size)
        {
            parents = new int[size];
            ranks = new int[size];
            for (int i = 0; i < size; i++)
            {
                parents[i] = i;
                ranks[i] = 1;
            }
        }

        public int Find(int x)
        {
            if (parents[x] != x)
            {
                parents[x] = Find(parents[x]); // Path compression
            }
            return parents[x];
        }

        public void Union(int x, int y)
        {
            int rootX = Find(x);
            int rootY = Find(y);

            if (rootX != rootY)
            {
                if (ranks[rootX] > ranks[rootY])
                {
                    parents[rootY] = rootX;
                }
                else if (ranks[rootX] < ranks[rootY])
                {
                    parents[rootX] = rootY;
                }
                else
                {
                    parents[rootY] = rootX;
                    ranks[rootX]++;
                }
            }
        }
    }
}
