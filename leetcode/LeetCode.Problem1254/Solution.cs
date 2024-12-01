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
            Count = n;
        }

        public int Count { get; private set; }

        public int Find(int x)
        {
            if (_parents[x] == -1)
            {
                return -1;
            }
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
                Count--;
            }
        }

        public void Remove(int key)
        {
            if (key == 0)
            {
                return;
            }
            _parents[key] = -1;
            Count--;
        }
    }

    public int ClosedIsland(int[][] grid)
    {
        int n = grid.Length;
        int m = grid[0].Length;

        int ToKey(int i, int j)
        {
            return i * m + j;
        }

        var directions = new (int i, int j)[]
        {
            (-1,  0),
            ( 1,  0),
            ( 0, -1),
            ( 0,  1)
        };

        var set = new UnionFind(n * m);

        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
            {
                int key = ToKey(i, j);

                if (grid[i][j] != 0)
                {
                    set.Remove(key);
                    continue;
                }

                foreach (var direction in directions)
                {
                    var adjanced = (i: i + direction.i, j: j + direction.j);
                    if (adjanced.i < 0 || adjanced.i > n - 1 || adjanced.j < 0 || adjanced.j > m - 1)
                    {
                        set.Union(0, key);
                        continue;
                    }

                    if (grid[adjanced.i][adjanced.j] == 0)
                    {
                        set.Union(key, ToKey(adjanced.i, adjanced.j));
                    }
                }
            }

        return set.Count - 1;
    }
}