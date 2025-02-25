// Input: s1 = "aabcc", s2 = "dbbca", s3 = "aadbbcbcac"
// Output: true
// Explanation: One way to obtain s3 is:
// Split s1 into s1 = "aa" + "bc" + "c", and s2 into s2 = "dbbc" + "a".
// Interleaving the two splits, we get "aa" + "dbbc" + "bc" + "a" + "c" = "aadbbcbcac".
// Since s3 can be obtained by interleaving s1 and s2, we return true.
// Example 2:

// Input: s1 = "aabcc", s2 = "dbbca", s3 = "aadbbbaccc"
// Output: false
// Explanation: Notice how it is impossible to interleave s2 with any other string to obtain s3.

using System.Net.Http.Headers;

public class Solution
{
    private readonly Dictionary<(int, int), bool> _cache = new Dictionary<(int, int), bool>();

    public bool IsInterleave(string s1, string s2, string s3, int i, int j)
    {
        if (i == 0 && j == 0)
        {
            return true;
        }

        if (_cache.TryGetValue((i, j), out var cached))
        {
            return cached;
        }

        bool isInterleave = false;

        int index = s3.Length - ((s1.Length - i) + (s2.Length - j)) - 1;

        if (i > 0 && s1[i - 1] == s3[index])
        {
            isInterleave = isInterleave || IsInterleave(s1, s2, s3, i - 1, j);
        }

        if (j > 0 && s2[j - 1] == s3[index])
        {
            isInterleave = isInterleave || IsInterleave(s1, s2, s3, i, j - 1);
        }

        _cache[(i, j)] = isInterleave;
        return isInterleave;
    }

    public bool IsInterleave(string s1, string s2, string s3)
    {
        if (s3.Length != s1.Length + s2.Length)
        {
            return false;
        }

        return IsInterleave(s1, s2, s3, s1.Length, s2.Length);
    }
}