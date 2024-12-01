public class Solution
{
    public int MaximumScore(int[] nums, int[] multipliers)
    {
        int[,] dp = new int[multipliers.Length + 1, multipliers.Length + 1];

        for (int i = multipliers.Length - 1; i >= 0; i--)
        {
            for (int left = i; left >= 0; left--)
            {
                int right = nums.Length - 1 - (i - left);
                
                dp[i, left] = Math.Max(multipliers[i] * nums[left] + dp[i + 1, left + 1],
                                       multipliers[i] * nums[right] + dp[i + 1, left]);
            }
        }

        return dp[0, 0];
    }
}
