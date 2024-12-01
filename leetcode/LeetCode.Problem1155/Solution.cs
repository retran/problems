public class Solution
{
    private readonly Dictionary<(int, int), int> _cache = new();

    public int NumRollsToTarget(int n, int k, int target)
    {
        if (n == 0)
        {
            return target == 0 
                ? 1
                : 0;
        };

        if (_cache.TryGetValue((n, target), out var cached))
        {
            return cached;
        }

        int count = 0;
        for (int i = 1; i <= k; i++)
        {
            if (i <= target)
            {
                count = (count + NumRollsToTarget(n - 1, k, target - i)) % 1000000007;
            }
        }

        _cache[(n, target)] = count;

        return count;
    }
}