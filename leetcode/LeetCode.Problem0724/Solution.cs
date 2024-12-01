public class Solution
{
    public int PivotIndex(int[] nums)
    {
        int rightSum = nums.Sum() - nums[0];
        int leftSum = 0;

        if (leftSum == rightSum)
        {
            return 0;
        }

        for (int i = 1; i < nums.Length; i++)
        {
            leftSum += nums[i - 1];
            rightSum -= nums[i];
            if (leftSum == rightSum)
            {
                return i;
            }
        }

        return -1;
    }
}