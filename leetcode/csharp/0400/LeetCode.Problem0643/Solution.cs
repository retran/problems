public class Solution
{
    public double FindMaxAverage(int[] nums, int k)
    {
        double currentSum = 0;
        for (int i = 0; i < k; i++)
        {
            currentSum += nums[i];
        }
        double maxAverage = currentSum / k;
        for (int i = k; i < nums.Length; i++)
        {
            currentSum = currentSum + nums[i] - nums[i - k];
            maxAverage = Math.Max(maxAverage, currentSum / k);
        }

        return maxAverage;
    }
}