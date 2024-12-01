public class Solution
{
    private readonly Dictionary<int, int> _cache = new();

    public int CombinationSum4(int[] nums, int target, int currentSum)
    {
        if (currentSum == target)
        {
            return 1;
        }

        if (currentSum > target)
        {
            return 0;
        }

        if (_cache.TryGetValue(currentSum, out var cached))
        {
            return cached;
        }

        int count = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            count += CombinationSum4(nums, target, currentSum + nums[i]);
        }

        _cache[currentSum] = count;
        return count;
    }


    public int CombinationSum4(int[] nums, int target)
    {
        return CombinationSum4(nums, target, 0);
    }
}