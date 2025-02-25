public class Solution
{
    public int MinOperations(int[] nums)
    {
        int n = nums.Length;
        
        int[] sorted = nums.Distinct().OrderBy(x => x).ToArray();
        
        int minOperations = n;
        for (int i = 0; i < sorted.Length; i++)
        {
            int windowEnd = sorted[i] + n - 1;
            
            int idx = Array.BinarySearch(sorted, windowEnd + 1);
            if (idx < 0)
            {
                idx = ~idx;
            }
            
            int count = idx - i;
            
            minOperations = Math.Min(minOperations, n - count);
        }
        
        return minOperations;
    }
}
