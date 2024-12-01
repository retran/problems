public class Solution
{
    public int ThirdMax(int[] nums)
    {
        int count = 0;
        long[] max = new long[3]
        {
            long.MinValue,
            long.MinValue,
            long.MinValue
        };

        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] == max[0] || nums[i] == max[1] || nums[i] == max[2])
            {
                continue;
            }

            if (nums[i] > max[0])
            {
                max[2] = max[1];
                max[1] = max[0];
                max[0] = nums[i];
                count++;
                continue;
            }

            if (nums[i] > max[1])
            {
                max[2] = max[1];
                max[1] = nums[i];
                count++;
                continue;
            }

            if (nums[i] > max[2])
            {
                count++;
                max[2] = nums[i];
            }
        }

        return (int)(count > 2 ? max[2] : max[0]);
    }
}