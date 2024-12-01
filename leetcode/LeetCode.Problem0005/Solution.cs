public class Solution
{
    public string LongestPalindrome(string s)
    {
        int n = s.Length;
        if (n == 0) 
        {
            return string.Empty;
        }

        bool[,] dp = new bool[n, n];
        int maxLength = 1;
        int start = 0;

        for (int i = 0; i < n; i++)
        {
            dp[i, i] = true;

            if (i < n -1 && s[i] == s[i + 1])
            {
                dp[i, i + 1] = true;
                start = i;
                maxLength = 2;
            }
        }

        for (int length = 3; length <= n; length++)
        {
            for (int i = 0; i < n - length + 1; i++)
            {
                int j = i + length - 1;
                if (s[i] == s[j] && dp[i + 1, j - 1])
                {
                    dp[i, j] = true;

                    if (length > maxLength)
                    {
                        start = i;
                        maxLength = length;
                    }
                }
            }
        }

        return s.Substring(start, maxLength);
    }
}
