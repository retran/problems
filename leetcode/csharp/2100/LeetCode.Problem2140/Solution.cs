public class Solution
{
    public long MostPoints(int[][] questions)
    {
        long[,] dp = new long[questions.Length, 2];

        dp[questions.Length - 1, 0] = 0;
        dp[questions.Length - 1, 1] = questions[questions.Length - 1][0];

        for (int i = questions.Length - 2; i >= 0; i--)
        {
            dp[i, 0] = Math.Max(dp[i + 1, 0], dp[i + 1, 1]);

            int skipTo = i + questions[i][1] + 1;
            if (skipTo < questions.Length)
            {
                dp[i, 1] = Math.Max(dp[skipTo, 0], dp[skipTo, 1]) + questions[i][0];
            }
            else
            {
                dp[i, 1] = questions[i][0];
            }
        }

        return Math.Max(dp[0, 0], dp[0, 1]);
    }
}