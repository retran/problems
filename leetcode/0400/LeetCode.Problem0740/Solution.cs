public class Solution
{
    public int DeleteAndEarn(int[] nums)
    {
        int max = nums.Max();

        int[,] dp = new int[max + 1, 2];

        dp[0, 0] = 0;
        dp[0, 1] = 0;

        int maxScore = 0;
        for (int i = 1; i <= max; i++)
        {
            dp[i, 0] = maxScore;
            dp[i, 1] = i * nums.Count(v => v == i) + dp[i - 1, 0];

            if (dp[i, 1] > maxScore)
            {
                maxScore = dp[i, 1];
            }
        }

        return maxScore;
    }
}