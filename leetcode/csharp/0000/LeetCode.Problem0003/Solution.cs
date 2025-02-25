public class Solution
{
    public int LengthOfLongestSubstring(string s)
    {
        if (s.Length == 0)
        {
            return 0;
        }

        int i = 0;
        int j = 1;

        int max = 1;

        HashSet<char> set = new HashSet<char>();
        set.Add(s[0]);
        while (j < s.Length)
        {
            if (set.Contains(s[j]))
            {
                set.Remove(s[i]);
                i++;
            }
            else
            {
                set.Add(s[j]);
                j++;

                if (max < j - i)
                {
                    max = j - i;
                }
            }
        }
        return max;
    }
}