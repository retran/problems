// https://leetcode.com/problems/search-insert-position

public class Solution
{
    public int SearchInsert(int[] nums, int target)
    {
        int left = 0;
        int right = nums.Length - 1;

        if (nums[left] == target) 
        {
            return left;
        }

        if (nums[right] == target) 
        {
            return right;
        }

        while (left <= right)
        {
            int mid = (left + right) / 2;
            if (nums[mid] == target)
            {
                return mid;
            }

            if (nums[mid] < target) 
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        if (left == nums.Length || (left < nums.Length && target < nums[left]))
        {
            return left;
        }

        return left + 1;
    }
}