public class Solution
{
    private Dictionary<int, int> _cache = new Dictionary<int, int>();

    private int Rob(int[] nums, int i)
    {
        if (i == 0)
        {
            return nums[0];
        }

        if (i == 1)
        {
            return Math.Max(nums[0], nums[1]);
        }

        if (!_cache.TryGetValue(i, out var answer))
        {
            answer = Math.Max(Rob(nums, i - 1), Rob(nums, i - 2) + nums[i]);
            _cache[i] = answer;
        }

        return answer;
    }

    public int Rob(int[] nums)
    {
        return Rob(nums, nums.Length - 1);
    }
}