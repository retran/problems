public class Solution
{
    public IList<int> LongToSubset(long value, int[] nums)
    {
        var subset = new List<int>();
        for (int i = 0; i < nums.Length && value > 0; i++)
        {
            long bit = value % 2;
            value = value / 2;

            if (bit == 1)
            {
                subset.Add(nums[i]);
            }
        }

        return subset;
    }

    public IList<IList<int>> Subsets(int[] nums)
    {
        var answer = new List<IList<int>>();
        long max = 1 << nums.Length;

        for (long i = 0; i < max; i++)
        {
            answer.Add(LongToSubset(i, nums));
        }

        return answer;
    }
}