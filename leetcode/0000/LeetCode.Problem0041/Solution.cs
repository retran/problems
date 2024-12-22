public class Solution
{
    public int FirstMissingPositive(int[] nums)
    {
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] <= 0 || nums[i] > nums.Length)
            {
                nums[i] = nums.Length + 1;
            }
        }

        for (int i = 0; i < nums.Length; i++)
        {
            int num = Math.Abs(nums[i]);
            if (num <= nums.Length)
            {
                nums[num - 1] = -Math.Abs(nums[num - 1]);
            }
        }

        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] > 0)
            {
                return i + 1;
            }
        }

        return nums.Length + 1;
    }
}
