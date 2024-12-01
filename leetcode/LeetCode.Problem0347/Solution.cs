public class Solution 
{
    public int[] TopKFrequent(int[] nums, int k) 
    {
        var frequencies = new Dictionary<int, int>();

        for (int i = 0; i < nums.Length; i++)
        {
            if (!frequencies.TryGetValue(nums[i], out var frequency))
            {
                frequency = 0;
            }

            frequencies[nums[i]] = frequency + 1;
        }

        return frequencies
            .OrderBy(p => -p.Value)
            .Select(p => p.Key)
            .Take(k)
            .ToArray();
    }
}