public class Solution
{
    public double ProbabilityOfHeads(double[] prob, int target)
    {
        var dp = new double[prob.Length + 1, target + 1];
        dp[0, 0] = 1;

        for (int i = 1; i <= prob.Length; i++)
        {
            dp[i, 0] = dp[i - 1, 0] * (1 - prob[i - 1]);
        }

        for (int i = 1; i <= prob.Length; i++)
        {
            for (int j = 1; j <= target; j++)
            {
                dp[i, j] = dp[i - 1, j] * (1 - prob[i - 1]) + dp[i - 1, j - 1] * prob[i - 1];
            }
        }

        return dp[prob.Length, target];
    }
}