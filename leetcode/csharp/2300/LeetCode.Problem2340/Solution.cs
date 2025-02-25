public class Solution
{
    public int MinimumSwaps(int[] nums)
    {
        int minIndex = 0;
        int maxIndex = 0;

        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[minIndex] > nums[i])
            {
                minIndex = i;
            }

            if (nums[maxIndex] <= nums[i])
            {
                maxIndex = i;
            }
        }

        int swaps = 0;
        if (minIndex > maxIndex)
        {
            swaps--;
        }
        
        swaps += minIndex;
        swaps += nums.Length - 1 - maxIndex;

        return swaps;
    }
}