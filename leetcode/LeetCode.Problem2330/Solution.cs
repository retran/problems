public class Solution
{
    public bool MakePalindrome(string s)
    {
        int singles = 0;

        for (int i = 0; i < s.Length / 2; i++)
        {
            int j = s.Length - 1 - i;

            if (s[i] != s[j])
            {
                singles++;
            }
        }

        return singles < 3;
    }
}