public class Solution
{
    private readonly Dictionary<(int, int), int> _cache = new();

    private int MinimumDeleteSum(string s1, string s2, int i, int j)
    {
        if (_cache.TryGetValue((i, j), out var cached))
        {
            return cached;
        }

        int sum = 0;
        if (i == -1 && j == -1)
        {
            sum = 0;
        }
        else if (i == -1)
        {
            for (int k = 0; k <= j; k++)
            {
                sum += s2[k];
            }
        }
        else if (j == -1)
        {
            for (int k = 0; k <= i; k++)
            {
                sum += s1[k];
            }
        }
        else
        {
            sum = int.MaxValue;

            if (s1[i] == s2[j])
            {
                sum = MinimumDeleteSum(s1, s2, i - 1, j - 1);
            }

            sum = Math.Min(sum, MinimumDeleteSum(s1, s2, i - 1, j - 1) + s1[i] + s2[j]);
            sum = Math.Min(sum, MinimumDeleteSum(s1, s2, i - 1, j) + s1[i]);
            sum = Math.Min(sum, MinimumDeleteSum(s1, s2, i, j - 1) + s2[j]);
        }

        _cache[(i, j)] = sum;
        return sum;
    }

    public int MinimumDeleteSum(string s1, string s2)
    {
        return MinimumDeleteSum(s1, s2, s1.Length - 1, s2.Length - 1);
    }
}