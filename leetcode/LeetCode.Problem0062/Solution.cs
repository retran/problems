public class Solution
{
    private readonly IDictionary<(int, int), int> _cache = new Dictionary<(int, int), int>();

    public int UniquePaths(int m, int n)
    {
        int answer = 1;

        if (_cache.TryGetValue((m, n), out answer))
        {
            return answer;
        }

        if (m == 1 && n == 1)
        {
            answer = 1;
        }
        else if (m == 1)
        {
            answer = UniquePaths(m, n - 1);
        }
        else if (n == 1)
        {
            answer = UniquePaths(m - 1, n);
        }
        else
        {
            answer = UniquePaths(m - 1, n) + UniquePaths(m, n - 1);
        }

        _cache[(m, n)] = answer;
        
        return answer;
    }
}