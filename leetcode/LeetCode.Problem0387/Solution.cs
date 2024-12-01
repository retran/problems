public class Solution
{
    public int FirstUniqChar(string s)
    {
        int[] frequencies = new int[26];
        int[] indices = new int[26];

        for (int i = 0; i < s.Length; i++)
        {
            frequencies[s[i] - 'a']++;
            indices[s[i] - 'a'] = i;
        }

        int min = s.Length;
        for (int i = 0; i < 26; i++)
        {
            if (frequencies[i] == 1)
            {
                min = Math.Min(min, indices[i]);
            }
        }

        if (min == s.Length)
        {
            min = -1;
        }

        return min;
    }
}