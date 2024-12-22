public class Solution
{
    private Dictionary<(int, int), int> _cache = new();

    public int LongestPalindromeSubseq(string s, int i, int j)
    {
        if (_cache.TryGetValue((i, j), out var cached))
        {
            return cached;
        }

        int length;
        if (j == i)
        {
            length = 1;
        }
        else if (j - i == 1)
        {
            if (s[i] == s[j])
            {
                length = 2;
            }
            else
            {
                length = 1;
            }
        }
        else
        {
            if (s[i] == s[j])
            {
                length = 2 + LongestPalindromeSubseq(s, i + 1, j - 1);
            }
            else
            {
                length = Math.Max(
                    LongestPalindromeSubseq(s, i + 1, j),
                    LongestPalindromeSubseq(s, i, j - 1)
                );
            }
        }

        _cache[(i, j)] = length;
        return length;
    }

    public int LongestPalindromeSubseq(string s)
    {
        return LongestPalindromeSubseq(s, 0, s.Length - 1);
    }
}