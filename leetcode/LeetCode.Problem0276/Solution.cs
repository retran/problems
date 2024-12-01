public class Solution
{
    private Dictionary<int, int> _cache = new Dictionary<int, int>();

    public int NumWays(int n, int k)
    {
        if (n == 1)
        {
            return k;
        }

        if (n == 2)
        {
            return k * k;
        }

        if (_cache.TryGetValue(n, out var cached))
        {
            return cached;
        }

        var count = (k - 1) * NumWays(n - 1, k) + (k - 1) * NumWays(n - 2, k);
        _cache[n] = count;
        return count;
    }
}