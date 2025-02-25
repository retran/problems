public class Solution
{
    private Dictionary<int, int> _cache = new Dictionary<int, int>();

    private int LengthOfLIS(int[] nums, int i)
    {
        if (i == 0)
        {
            return 1;
        }

        if (_cache.TryGetValue(i, out var cached))
        {
            return cached;
        }

        int max = 1;
        for (int j = 0; j < i; j++)
        {
            if (nums[i] > nums[j])
            {
                max = Math.Max(max, LengthOfLIS(nums, j) + 1);
            }
        }

        _cache[i] = max;
        return max;
    }


    public int LengthOfLIS(int[] nums)
    {
        if (nums == null || nums.Length == 0)
        {
            return 0;
        }

        int maxLIS = 1;
        for (int i = 0; i < nums.Length; i++)
        {
            maxLIS = Math.Max(maxLIS, LengthOfLIS(nums, i));
        }

        return maxLIS;
    }
}