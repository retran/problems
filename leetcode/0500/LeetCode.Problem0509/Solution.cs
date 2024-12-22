public class Solution
{
    private Dictionary<int, int> _cache = new Dictionary<int, int>();

    public int Fib(int n)
    {
        if (n == 0)
        {
            return 0;
        }

        if (n == 1)
        {
            return 1;
        }

        if (!_cache.TryGetValue(n, out var answer))
        {
            answer = Fib(n - 2) + Fib(n - 1);
            _cache[n] = answer;
        }

        return answer;
    }
}