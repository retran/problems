public class Solution
{
    public bool IsAnagram(string s, string t)
    {
        if (s.Length != t.Length)
        {
            return false;
        }

        var frequencyMap = new int[26];

        for (int i = 0; i < s.Length; i++)
        {
            frequencyMap[s[i] - 'a']++;
            frequencyMap[t[i] - 'a']--;
        }

        for (int i = 0; i < frequencyMap.Length; i++)
        {
            if (frequencyMap[i] != 0)
            {
                return false;
            }
        }

        return true;
    }
}