public class DisjointSet
{
    private int[] _parents;
    private int[] _ranks;

    private int _count = 0;

    public int Count => _count;

    public DisjointSet(int n)
    {
        _parents = new int[n];
        _ranks = new int[n];

        for (int i = 0; i < n; i++)
        {
            _parents[i] = i;
        }

        _count = n;
    }

    public int Find(int value)
    {
        if (_parents[value] != value)
        {
            _parents[value] = Find(_parents[value]);
        }

        return _parents[value];
    }

    public void Union(int x, int y)
    {
        int rootX = Find(x);
        int rootY = Find(y);

        if (rootX != rootY)
        {
            if (_ranks[rootX] > _ranks[rootY])
            {
                _parents[rootY] = rootX;
            }
            else if (_ranks[rootX] < _ranks[rootY])
            {
                _parents[rootX] = rootY;
            }
            else
            {
                _parents[rootY] = rootX;
                _ranks[rootX]++;
            }

            _count--;
        }
    }
}

public class Solution
{
    public int CountComponents(int n, int[][] edges)
    {
        var set = new DisjointSet(n);
        var graph = new Dictionary<int, IList<int>>();

        for (int k = 0; k < edges.Length; k++)
        {
            int i = edges[k][0];
            int j = edges[k][1];

            set.Union(i, j);
        }

        return set.Count;
    }
}