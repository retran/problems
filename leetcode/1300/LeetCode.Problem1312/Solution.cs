public class Solution
{
    private Dictionary<(int, int), int> _cache = new Dictionary<(int, int), int>();

    private int LongestPalindromicSubsequence(string s, int i, int j)
    {
        if (_cache.TryGetValue((i, j), out var cached))
        {
            return cached;
        }

        int max = 0;
        if (i == j)
        {
            max = 1;
        }
        else if (j - i == 1)
        {
            if (s[i] == s[j])
            {
                max = 2;
            }
            else
            {
                max = 1;
            }
        }
        else
        {
            if (s[i] == s[j])
            {
                max = Math.Max(max, LongestPalindromicSubsequence(s, i + 1, j - 1) + 2);
            }

            max = Math.Max(max, LongestPalindromicSubsequence(s, i + 1, j - 1));
            max = Math.Max(max, LongestPalindromicSubsequence(s, i, j - 1));
            max = Math.Max(max, LongestPalindromicSubsequence(s, i + 1, j));
        }

        _cache[(i, j)] = max;
        return max;
    }

    public int MinInsertions(string s)
    {
        var length = LongestPalindromicSubsequence(s, 0, s.Length - 1);
        return (s.Length - length);
    }
}