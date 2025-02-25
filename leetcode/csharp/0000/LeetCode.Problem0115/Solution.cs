public class Solution
{
    private readonly Dictionary<(int, int), int> _cache = new Dictionary<(int, int), int>();

    public int NumDistinct(string s, string t, int i, int j) 
    {
        if (j < 0)
        {
            return 1;
        }

        if (i < 0)
        {
            return 0;
        }

        if (_cache.TryGetValue((i, j), out var cached))
        {
            return cached;
        }

        int count = 0;
        if (s[i] == t[j])
        {
            count += NumDistinct(s, t, i - 1, j - 1);
        }

        count += NumDistinct(s, t, i - 1, j);

        _cache[(i, j)] = count;
        return count;
    }


    public int NumDistinct(string s, string t)
    {
        return NumDistinct(s, t, s.Length - 1, t.Length - 1);
    }
}