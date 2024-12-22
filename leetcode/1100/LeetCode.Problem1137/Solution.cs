public class Solution
{
    private Dictionary<int, int> _cache = new Dictionary<int, int>();

    public int Tribonacci(int n)
    {
        if (n == 0)
        {
            return 0;
        }

        if (n == 1)
        {
            return 1;
        }

        if (n == 2)
        {
            return 1;
        }

        if (!_cache.TryGetValue(n, out var answer))
        {
            answer = Tribonacci(n - 3) + Tribonacci(n - 2) + Tribonacci(n - 1);
            _cache[n] = answer;
        }

        return answer;
    }
}