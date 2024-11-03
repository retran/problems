using System.Diagnostics;

var solution = new Solution();

Debug.Assert(solution.FindTheDifference("abcd", "abcde") == 'e');
Debug.Assert(solution.FindTheDifference("", "y") == 'y');

public class Solution
{
    public char FindTheDifference(string s, string t)
    {
        var map = new Dictionary<char, int>();

        foreach (var c in s)
        {
            if (!map.TryGetValue(c, out var count))
            {
                count = 0;
            }
            map[c] = count + 1;
        }

        foreach (var c in t)
        {
            if (!map.TryGetValue(c, out var count) || count == 0)
            {
                return c;
            }

            map[c] = count - 1;
        }

        return (char)0;
    }
}