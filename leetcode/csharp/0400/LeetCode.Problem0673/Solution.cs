public class Solution
{
    private Dictionary<int, (int, int)> _cache = new();

    public (int, int) FindNumberOfLIS(int[] nums, int i)
    {
        if (i == 0)
        {
            return (1, 1);
        }

        if (_cache.TryGetValue(i, out var cached))
        {
            return cached;
        }

        int maxLength = 1;
        int maxLengthCount = 1;
        for (int j = i - 1; j >= 0; j--)
        {
            var (length, count) = FindNumberOfLIS(nums, j);
            if (nums[j] < nums[i])
            {
                length += 1;
                if (length > maxLength)
                {
                    maxLength = length;
                    maxLengthCount = count;
                }
                else if (length == maxLength)
                {
                    maxLengthCount += count;
                }
            }
        }

        _cache[i] = (maxLength, maxLengthCount);
        return (maxLength, maxLengthCount);
    }

    public int FindNumberOfLIS(int[] nums)
    {
        int maxLength = 0;
        int maxLengthCount = 0;
        for (int i = nums.Length - 1; i >= 0; i--)
        {
            var (length, count) = FindNumberOfLIS(nums, i);
            if (length > maxLength)
            {
                maxLength = length;
                maxLengthCount = count;
            }
            else if (length == maxLength)
            {
                maxLengthCount += count;
            }
        }
        return maxLengthCount;
    }
}