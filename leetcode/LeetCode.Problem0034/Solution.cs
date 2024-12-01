public class Solution
{
    public int[] SearchRange(int[] nums, int target)
    {
        int leftIndex = FindBound(nums, target, true);
        if (leftIndex == -1)
        {
            return [-1, -1];
        }

        int rightIndex = FindBound(nums, target, false);

        return [leftIndex, rightIndex];
    }

    private int FindBound(int[] nums, int target, bool isLeft)
    {
        int left = 0, right = nums.Length - 1;
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (nums[mid] == target)
            {
                if (isLeft)
                {
                    if (mid == left || nums[mid - 1] != target)
                    {
                        return mid;
                    }
                    right = mid - 1;
                }
                else
                {
                    if (mid == right || nums[mid + 1] != target)
                    {
                        return mid;
                    }
                    left = mid + 1;
                }
            }
            else if (nums[mid] < target)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }
        return -1;
    }
}
