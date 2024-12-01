public class Solution
{
    public IList<IList<int>> FindMissingRanges(int[] nums, int lower, int upper)
    {
        var result = new List<IList<int>>();

        if (nums.Length == 0)
        {
            result.Add(new List<int> { lower, upper });
            return result;
        }

        if (nums[0] > lower)
        {
            result.Add(new List<int> { lower, nums[0] - 1 });
        }

        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] == nums[i - 1] + 1)
            {
                continue;
            }
            else
            {
                result.Add(new List<int> { nums[i - 1] + 1, nums[i] - 1 });
            }
        }

        if (nums[nums.Length - 1] < upper)
        {
            result.Add(new List<int> { nums[nums.Length - 1] + 1, upper });
        }

        return result;
    }
}