public class DisjointSet
{
    private int[] _parents;

    public DisjointSet(int size)
    {
        _parents = new int[size];
        for (int i = 0; i < size; i++)
        {
            _parents[i] = i;
        }
    }

    public int Find(int value)
    {        
        if (_parents[value] != value)
        {
            _parents[value] = Find(_parents[value]);
        }

        return _parents[value];
    }

    public void Union(int a, int b)
    {
        int parentA = Find(a);
        int parentB = Find(b);

        if (parentA != parentB)
        {
            _parents[parentB] = parentA;
        }
    }
}

public class Solution
{
    public string SmallestStringWithSwaps(string s, IList<IList<int>> pairs)
    {
        int n = s.Length;
        var set = new DisjointSet(n);

        foreach (var pair in pairs)
        {
            set.Union(pair[0], pair[1]);
        }

        Dictionary<int, List<char>> components = new Dictionary<int, List<char>>();

        for (int i = 0; i < n; i++)
        {
            int root = set.Find(i);
            if (!components.ContainsKey(root))
            {
                components[root] = new List<char>();
            }
            components[root].Add(s[i]);
        }

        foreach (var component in components)
        {
            component.Value.Sort();
        }

        char[] result = new char[n];
        Dictionary<int, int> index = new Dictionary<int, int>();

        for (int i = 0; i < n; i++)
        {
            int root = set.Find(i);
            if (!index.ContainsKey(root))
            {
                index[root] = 0;
            }
            result[i] = components[root][index[root]];
            index[root]++;
        }

        return new string(result);
    }
}
