public class Solution
{
    public int ShortestSubarray(int[] nums, int targetSum)
    {
        int n = nums.Length;
        long[] prefixSums = new long[n + 1];
        for (int i = 1; i <= n; i++)
        {
            prefixSums[i] = prefixSums[i - 1] + nums[i - 1];
        }

        var candidateIndices = new LinkedList<int>();
        int shortestSubarrayLength = int.MaxValue;

        for (int i = 0; i <= n; i++)
        {
            while (candidateIndices.Count > 0 && 
                   prefixSums[i] - prefixSums[candidateIndices.First!.Value] >= targetSum)
            {
                shortestSubarrayLength = Math.Min(shortestSubarrayLength, i - candidateIndices.First.Value);
                candidateIndices.RemoveFirst();
            }

            while (candidateIndices.Count > 0 &&
                   prefixSums[i] <= prefixSums[candidateIndices.Last!.Value])
            {
                candidateIndices.RemoveLast();
            }

            candidateIndices.AddLast(i);
        }

        return shortestSubarrayLength == int.MaxValue ? -1 : shortestSubarrayLength;
    }
}
