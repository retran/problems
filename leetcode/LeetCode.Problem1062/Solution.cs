public class Solution
{
    public int LongestRepeatingSubstring(string s)
    {
        int[,] dp = new int[s.Length + 1, s.Length + 1];
        int maxLength = 0;

        for (int i = 1; i <= s.Length; i++)
        {
            for (int j = i + 1; j <= s.Length; j++)
            {
                if (s[i - 1] == s[j - 1])
                {
                    dp[i, j] = dp[i - 1, j - 1] + 1;
                    maxLength = Math.Max(maxLength, dp[i, j]);
                }
            }
        }
        return maxLength;
    }
}