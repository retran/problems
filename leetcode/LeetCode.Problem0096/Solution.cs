public class Solution
{
    private Dictionary<int, int> _cache = new Dictionary<int, int>();

    public int NumTrees(int n)
    {
        if (n == 0)
        {
            return 1;
        }

        if (n == 1)
        {
            return 1;
        }

        if (_cache.TryGetValue(n, out var cached))
        {
            return cached;
        }

        int count = 0;

        for (int i = 0; i < n; i++)
        {
            count += NumTrees(i) * NumTrees(n - i - 1);
        }

        _cache[n] = count;
        return count;
    }
}