public class Solution
{
    private const int UnknownDifference = 1000;
    private readonly Dictionary<(int, int), int> _cache = new();

    public int LongestSubsequence(int[] nums, int difference, Dictionary<int, List<int>> numberToIndexMap, int i)
    {
        if (_cache.TryGetValue((i, difference), out var cached))
        {
            return cached;
        }

        int answer;
        if (i == 0)
        {
            answer = 1;
        }
        else
        {
            int max = 1;

            if (difference == UnknownDifference)
            {
                for (int index = i - 1; index >= 0; index--)
                {
                    int targetDifference = nums[i] - nums[index];
                    max = Math.Max(max, LongestSubsequence(nums, targetDifference, numberToIndexMap, index) + 1);
                }
            }
            else
            {
                int target = nums[i] - difference;

                if (numberToIndexMap.TryGetValue(target, out var indices))
                {
                    foreach (var index in indices)
                    {
                        if (index < i)
                        {
                            max = Math.Max(max, LongestSubsequence(nums, difference, numberToIndexMap, index) + 1);
                        }
                    }
                }
            }

            answer = max;
        }

        _cache[(i, difference)] = answer;
        return answer;
    }

    public int LongestArithSeqLength(int[] nums)
    {
        var numberToIndexMap = new Dictionary<int, List<int>>();

        for (int i = 0; i < nums.Length - 1; i++)
        {
            if (!numberToIndexMap.TryGetValue(nums[i], out var list))
            {
                list = new List<int>() { i };
                numberToIndexMap[nums[i]] = list;
            }
            else
            {
                list.Add(i);
            }
        }

        int max = 0;
        
        for (int i = 0; i <= nums.Length - 1; i++)
        {
            max = Math.Max(max, LongestSubsequence(nums, UnknownDifference, numberToIndexMap, i));
        }

        return max;
    }
}