public class Solution
{
    public int MinSubArrayLen(int target, int[] nums)
    {
        int i = 0;
        int j = 0;

        int min = nums.Length;

        int sum = nums[0];

        while (j < nums.Length)
        {
            if (sum >= target)
            {
                if (min > j - i + 1)
                {
                    min = j - i + 1;
                }

                sum -= nums[i];
                i++;
            }
            else
            {
                j++;
                if (j < nums.Length)
                {
                    sum += nums[j];
                }
            }
        }

        if (i == 0 && sum < target)
        {
            return 0;
        }

        return min;
    }
}