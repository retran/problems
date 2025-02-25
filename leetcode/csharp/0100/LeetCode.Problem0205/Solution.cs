// https://leetcode.com/problems/

public class Solution
{
    public bool IsIsomorphic(string s, string t)
    {
        if (s.Length != t.Length)
        {
            return false;
        }

        var map = new Dictionary<char, char>();

        for (int i = 0; i < s.Length; i++)
        {
            var c1 = s[i];
            var c2 = t[i];

            if (map.ContainsKey(c1))
            {
                if (map[c1] != c2)
                {
                    return false;
                }
            }
            else
            {
                if (map.ContainsValue(c2))
                {
                    return false;
                }
                
                map[c1] = c2;
            }
        }

        return true;
    }
}