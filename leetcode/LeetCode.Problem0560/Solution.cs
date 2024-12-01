public class Solution
{
    public int SubarraySum(int[] nums, int k)
    {
        var frequencies = new Dictionary<int, int>();
        
        int sum = nums[0];
        frequencies[sum] = 1;
        int count = sum == k ? 1 : 0;

        for (int i = 1; i < nums.Length; i++)
        {
            sum += nums[i];

            if (sum == k)
            {
                count++;
            }

            int complement = sum - k;
            if (frequencies.TryGetValue(complement, out var complementCount))
            {
                count += complementCount;
            }

            if (!frequencies.TryGetValue(sum, out var frequency))
            {
                frequency = 0;
            }

            frequencies[sum] = frequency + 1;
        }

        return count;
    }
}