public class Solution
{
    public bool IncreasingTriplet(int[] nums)
    {
        if (nums.Length < 3)
        {
            return false;
        }

        int low = int.MaxValue;
        int mid = int.MaxValue;
        int i = 0;

        while (i < nums.Length)
        {
            if (nums[i] < low)
            {
                low = nums[i];
            }
            else if (nums[i] > low && nums[i] < mid)
            {
                mid = nums[i];
            }
            else if (nums[i] > mid)
            {
                return true;
            }
            i++;
        }

        return false;
    }
}