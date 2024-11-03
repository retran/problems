public class Solution
{
    public bool IsMonotonic(int[] nums)
    {
        if (nums.Length < 1)
        {
            return false;
        }

        if (nums.Length == 1)
        {
            return true;
        }

        int diff = nums[1] - nums[0];

        for (int i = 1; i < nums.Length - 1; i++)
        {
            int currentDiff = nums[i + 1] - nums[i];

            if (diff == 0)
            {
                diff = currentDiff;
            }
            else if (diff > 0 && currentDiff < 0 || diff < 0 && currentDiff > 0)
            {
                return false;
            }
        }

        return true;
    }
}