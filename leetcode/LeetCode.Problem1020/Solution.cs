public class Solution
{
    private class UnionFind
    {
        private int[] _parents;

        public UnionFind(int n)
        {
            _parents = new int[n];
            for (int i = 0; i < n; i++)
            {
                _parents[i] = i;
            }
        }

        public int Find(int x)
        {
            if (_parents[x] == x)
            {
                return x;
            }

            _parents[x] = Find(_parents[x]);
            return _parents[x];
        }

        public void Union(int x, int y)
        {
            int rootX = Find(x);
            int rootY = Find(y);

            if (rootX == -1 || rootY == -1)
            {
                return;
            }

            if (rootX != rootY)
            {
                _parents[rootY] = rootX;
            }
        }
    }

    public int NumEnclaves(int[][] grid)
    {
        int n = grid.Length;
        int m = grid[0].Length;

        if (n < 3 || m < 3)
        {
            return 0;
        }

        int ToKey(int i, int j)
        {
            return i * m + j;
        }

        var sets = new UnionFind(n * m);

        for (int i = 0; i < n; i++)
        {
            sets.Union(0, ToKey(i, 0));
            sets.Union(0, ToKey(i, m - 1));
        }

        for (int j = 0; j < m; j++)
        {
            sets.Union(0, ToKey(0, j));
            sets.Union(0, ToKey(n - 1, j));
        }

        var directions = new (int i, int j)[] {
            (-1, 0),
            (1, 0),
            (0, -1),
            (0, 1)
        };

        for (int i = 1; i < n - 1; i++)
        {
            for (int j = 1; j < m - 1; j++)
            {
                int key = ToKey(i, j);
                if (grid[i][j] == 0)
                {
                    sets.Union(0, key);
                }
                else
                {
                    foreach (var direction in directions)
                    {
                        var k = i + direction.i;
                        var l = j + direction.j;

                        if (grid[k][l] == 0)
                        {
                            continue;
                        }

                        if (k < 1 || k > n - 2 || j < 1 || j > m - 2)
                        {
                            sets.Union(0, key);
                            continue;
                        }

                        sets.Union(key, ToKey(k, l));
                    }
                }
            }
        }

        var numberOfEnclaves = n * m;
        var zeroSet = sets.Find(0);
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (sets.Find(ToKey(i, j)) == zeroSet)
                {
                    numberOfEnclaves--;
                }
            }
        }

        return numberOfEnclaves;
    }
}