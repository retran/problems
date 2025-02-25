public class Solution
{
    private class SubarrayEntry
    {
        public int TotalSum { get; set; }
        public int[] Indices { get; }

        public SubarrayEntry(int totalSum)
        {
            TotalSum = totalSum;
            Indices = new int[3];
        }

        public int[] GetIndicesCopy() => (int[])Indices.Clone();
    }

    public int[] MaxSumOfThreeSubarrays(int[] nums, int k)
    {
        int n = nums.Length;
        if (n < 3 * k)
            return Array.Empty<int>();

        int[] prefixSums = ComputePrefixSums(nums);

        SubarrayEntry[,] dp = new SubarrayEntry[3, n];

        for (int stage = 0; stage < 3; stage++)
        {
            if (stage == 0)
            {
                for (int j = 0; j <= n - k; j++)
                {
                    int sum = GetSubarraySum(prefixSums, j, k);
                    dp[stage, j] = new SubarrayEntry(sum);
                    dp[stage, j].Indices[stage] = j;
                }
            }
            else
            {
                SubarrayEntry bestPrevious = null;
                for (int j = stage * k; j <= n - k; j++)
                {
                    int prevIndex = j - k;
                    if (prevIndex >= (stage - 1) * k)
                    {
                        if (bestPrevious == null || dp[stage - 1, prevIndex].TotalSum > bestPrevious.TotalSum)
                        {
                            bestPrevious = dp[stage - 1, prevIndex];
                        }
                    }

                    int currentSum = GetSubarraySum(prefixSums, j, k);
                    dp[stage, j] = new SubarrayEntry(currentSum + bestPrevious!.TotalSum);
                    for (int x = 0; x < stage; x++)
                    {
                        dp[stage, j].Indices[x] = bestPrevious.Indices[x];
                    }
                    dp[stage, j].Indices[stage] = j;
                }
            }
        }

        SubarrayEntry maxEntry = dp[2, 2 * k];
        for (int j = 2 * k + 1; j <= n - k; j++)
        {
            if (dp[2, j].TotalSum > maxEntry.TotalSum)
            {
                maxEntry = dp[2, j];
            }
        }

        return maxEntry.Indices;
    }

    private int[] ComputePrefixSums(int[] nums)
    {
        int n = nums.Length;
        int[] prefixSums = new int[n + 1];
        for (int i = 1; i <= n; i++)
        {
            prefixSums[i] = prefixSums[i - 1] + nums[i - 1];
        }
        return prefixSums;
    }

    private int GetSubarraySum(int[] prefixSums, int start, int k)
    {
        return prefixSums[start + k] - prefixSums[start];
    }
}
