public class Solution
{
    public long MaximumSubarraySum(int[] nums, int k)
    {
        var seen = new Dictionary<int, int>();
        long sum = 0;
        long maxSum = 0;

        void AddToSet(int val)
        {
            if (!seen.TryGetValue(val, out var count))
            {
                count = 0;
            }
            seen[val] = count + 1;
        }

        void RemoveFromSet(int val)
        {
            if (seen[val] == 1)
            {
                seen.Remove(val);
            }
            else
            {
                seen[val]--;
            }
        }

        int CountOfDistinctElements()
        {
            return seen.Count;
        }

        for (int j = 0; j < k; j++)
        {
            sum += nums[j];
            AddToSet(nums[j]);
        }

        if (CountOfDistinctElements() == k)
        {
            maxSum = sum;
        }

        for (int i = k; i < nums.Length; i++)
        {
            sum -= nums[i - k];
            RemoveFromSet(nums[i - k]);

            sum += nums[i];
            AddToSet(nums[i]);

            if (CountOfDistinctElements() == k)
            {
                maxSum = Math.Max(maxSum, sum);
            }
        }

        return maxSum;
    }
}
