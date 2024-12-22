public class Solution
{
    public int FindPeakElement(int[] nums)
    {
        if (nums.Length == 1)
        {
            return 0;
        }

        int left = 0;
        int right = nums.Length - 1;
        while (left < right)
        {
            int mid = left + (right - left) / 2;

            if (mid == nums.Length - 1 || nums[mid] < nums[mid + 1])
            {
                left = mid + 1;
            }
            else
            {
                right = mid;
            }
        }

        return left;
    }
}