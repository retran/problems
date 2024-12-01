public class Solution
{
    public void MoveZeroes(int[] nums)
    {
        if (nums.Length < 2)
        {
            return;
        }

        int left = 0;
        int right = 1;

        while (right < nums.Length)
        {
            if (nums[left] != 0 && left < right - 1) 
            {
                left++;
                continue;
            }

            if (nums[left] == 0 && nums[right] != 0)
            {
                nums[left] = nums[right];
                nums[right] = 0;
                left++;
            }
            right++;
        }
    }
}