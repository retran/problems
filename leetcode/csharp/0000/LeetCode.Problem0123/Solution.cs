// https://leetcode.com/problems/best-time-to-buy-and-sell-stock-iii

public class Solution
{
    public int MaxProfit(int[] prices)
    {
        int n = prices.Length;
        if (n == 0)
        {
            return 0;
        }

        int[,] dp = new int[n, 5];
        dp[0, 0] = 0;
        dp[0, 1] = -prices[0];
        dp[0, 2] = 0;
        dp[0, 3] = -prices[0];
        dp[0, 4] = 0;

        for (int i = 1; i < n; i++)
        {
            dp[i, 0] = dp[i - 1, 0];
            dp[i, 1] = Math.Max(dp[i - 1, 1], dp[i - 1, 0] - prices[i]);
            dp[i, 2] = Math.Max(dp[i - 1, 2], dp[i - 1, 1] + prices[i]);
            dp[i, 3] = Math.Max(dp[i - 1, 3], dp[i - 1, 2] - prices[i]);
            dp[i, 4] = Math.Max(dp[i - 1, 4], dp[i - 1, 3] + prices[i]);
        }

        return dp[n - 1, 4];
    }
}