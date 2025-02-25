public class Solution
{
    public int[] SortArray(int[] nums)
    {
        int min = int.MaxValue;
        int max = int.MinValue;

        var map = new Dictionary<int, int>();

        for (int i = 0; i < nums.Length; i++)
        {
            if (min > nums[i])
            {
                min = nums[i];
            }

            if (max < nums[i])
            {
                max = nums[i];
            }

            if (!map.TryGetValue(nums[i], out var value))
            {
                value = 0;
            }
            map[nums[i]] = value + 1;
        }

        int j = 0;
        for (int i = min; i <= max; i++)
        {
            if (map.TryGetValue(i, out var value))
            {
                while (value > 0)
                {
                    nums[j] = i;
                    j++;
                    value--;
                }
            }
        }

        return nums;
    }
}