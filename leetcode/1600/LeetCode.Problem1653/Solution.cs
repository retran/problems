public class Solution
{
    public int MinimumDeletions(string s)
    {
        int n = s.Length;
        int count = 'b' == s[0] ? 1 : 0;
        int deletionsCount = 0;

        for (int i = 1; i < n; i++)
        {
            if (s[i] == 'a')
            {
                deletionsCount = Math.Min(deletionsCount + 1, count);
            }
            else
            {
                count++;
            }
        }

        return deletionsCount;
    }
}