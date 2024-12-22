public class Solution
{
    private Dictionary<int, int> _cache = new Dictionary<int, int>();

    public int ClimbStairs(int n)
    {
        if (n == 1)
        {
            return 1;
        }

        if (n == 2)
        {
            return 2;
        }

        if (!_cache.TryGetValue(n, out var answer))
        {
            answer = ClimbStairs(n - 1) + ClimbStairs(n - 2);
            _cache[n] = answer;
        }

        return answer;
    }
}