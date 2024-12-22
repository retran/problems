public class Solution
{
    public bool Matches(string s, int index)
    {
        int k = 0;

        for (int i = index + 1; i < s.Length; i++)
        {
            if (s[k] != s[i])
            {
                return false;
            }

            k++;
            if (k > index)
            {
                k = 0;
            }
        }

        return true;
    }

    public bool RepeatedSubstringPattern(string s)
    {
        for (int i = 0; i < s.Length / 2; i++)
        {
            if (s.Length % (i + 1) != 0)
            {
                continue;
            }

            if (Matches(s, i))
            {
                return true;
            }
        }

        return false;
    }
}