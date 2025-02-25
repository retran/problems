using System;
using System.Linq;

public class Solution
{
    public int SmallestDistancePair(int[] nums, int k)
    {
        Array.Sort(nums);

        int left = 0;
        int right = nums[nums.Length - 1] - nums[0];

        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (CountPairsWithDistanceLessThanOrEqual(nums, mid) >= k)
            {
                right = mid - 1;
            }
            else
            {
                left = mid + 1;
            }
        }

        return left;
    }

    private int CountPairsWithDistanceLessThanOrEqual(int[] nums, int maxDistance)
    {
        int count = 0;
        int start = 0;

        for (int end = 0; end < nums.Length; end++)
        {
            while (nums[end] - nums[start] > maxDistance)
            {
                start++;
            }
            count += end - start;
        }

        return count;
    }
}
