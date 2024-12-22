public class Solution
{
    private readonly Dictionary<int, int> _cache = new Dictionary<int, int>();

    public int NumSquares(int n)
    {
        if (n == 0)
        {
            return 0;
        }

        if (_cache.TryGetValue(n, out var cached))
        {
            return cached;
        }

        int min = int.MaxValue;
        int i = 1;
        while (i * i <= n)
        {
            int count = NumSquares(n - i * i);
            min = Math.Min(min, count + 1);
            i += 1;
        }

        _cache[n] = min;

        return min;
    }
}
