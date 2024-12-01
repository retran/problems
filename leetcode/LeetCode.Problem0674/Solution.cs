public class Solution 
{
    public int FindLengthOfLCIS(int[] nums) 
    {
        int max = 0;
        int count = 1;
        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i - 1] < nums[i])
            {
                count++;
            }
            else
            {
                max = Math.Max(count, max);
                count = 1;
            }
        }

        max = Math.Max(count, max);

        return max;
    }
}