public class Solution
{
    private Dictionary<int, int> _cache = new();

    public int LongestValidParentheses(string s, int index)
    {
        if (_cache.TryGetValue(index, out var cached))
        {
            return cached;
        }

        int length = 0;
        if (index >= 1 && s[index] == ')')
        {
            var innerLength = LongestValidParentheses(s, index - 1);

            int openingBraceIndex = index - innerLength - 1;
            if (openingBraceIndex >= 0 && s[openingBraceIndex] == '(')
            {
                length = innerLength + 2 + LongestValidParentheses(s, openingBraceIndex - 1);
            }
        }

        _cache[index] = length;
        return length;
    }

    public int LongestValidParentheses(string s)
    {
        int max = 0;

        for (int i = 0; i < s.Length; i++)
        {
            max = Math.Max(max, LongestValidParentheses(s, i));
        }

        return max;
    }
}