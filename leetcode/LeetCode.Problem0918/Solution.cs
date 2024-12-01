public class Solution
{
    public int MaxSubarraySumCircular(int[] nums)
    {
        int maxSum = nums[0];
        int currentSum = nums[0];
        int minSum = nums[0];
        int currentMin = nums[0];
        int totalSum = nums[0];
        for (int i = 1; i < nums.Length; i++)
        {
            currentSum = Math.Max(nums[i], currentSum + nums[i]);
            maxSum = Math.Max(maxSum, currentSum);
            currentMin = Math.Min(nums[i], currentMin + nums[i]);
            minSum = Math.Min(minSum, currentMin);
            totalSum += nums[i];
        }

        return maxSum > 0 ? Math.Max(maxSum, totalSum - minSum) : maxSum;
    }
}