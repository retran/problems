public class Solution
{
    public long SubArrayRanges(int[] nums)
    {
        long sum = 0;
        for (int i = 0; i < nums.Length - 1; i++)
        {
            int max = nums[i];
            int min = nums[i];

            for (int j = i + 1; j < nums.Length; j++)
            {
                max = Math.Max(max, nums[j]);
                min = Math.Min(min, nums[j]);

                sum += max - min;
            }
        }

        return sum;
    }
}