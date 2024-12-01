public class Solution
{
    public int AppendCharacters(string s, string t)
    {
        int i = 0;
        int j = 0;

        while (i < s.Length && j < t.Length)
        {
            if (s[i] == t[j])
            {
                i++;
                j++;
            }
            else
            {
                i++;
            }
        }

        return t.Length - j;
    }
}