public class Solution
{
    private const long MOD = 1000000007;

    private readonly Dictionary<int, long> _cache = new();

    public long CountGoodStrings(int low, int high, int zero, int one, int length)
    {
        if (length > high)
        {
            return 0;
        }

        if (_cache.TryGetValue(length, out var cached))
        {
            return cached;
        }

        long count = 0;

        if (length >= low)
        {
            count++;
        };

        count = (count + CountGoodStrings(low, high, zero, one, length + zero)) % MOD;
        count = (count + CountGoodStrings(low, high, zero, one, length + one)) % MOD;

        _cache[length] = count;
        return count;
    }


    public int CountGoodStrings(int low, int high, int zero, int one)
    {
        return (int)CountGoodStrings(low, high, zero, one, 0);
    }
}