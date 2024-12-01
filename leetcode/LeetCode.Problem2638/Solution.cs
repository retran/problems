public class Solution
{
    public long CountTheNumOfKFreeSubsets(int[] nums, int k)
    {
        Array.Sort(nums);

        Dictionary<int, int> sequenceLengths = new();
        int maxSequenceLength = 0;

        foreach (int num in nums)
        {
            if (sequenceLengths.TryGetValue(num - k, out int length))
            {
                sequenceLengths[num] = length + 1;
                sequenceLengths.Remove(num - k);
            }
            else
            {
                sequenceLengths[num] = 1;
            }

            maxSequenceLength = Math.Max(maxSequenceLength, sequenceLengths[num]);
        }

        long[] fibonacci = GenerateFibonacci(maxSequenceLength + 2);

        long result = 1;
        foreach (var length in sequenceLengths.Values)
        {
            result *= fibonacci[length + 1];
        }

        return result;
    }

    private long[] GenerateFibonacci(int n)
    {
        long[] fibonacci = new long[n];
        fibonacci[0] = 1;
        fibonacci[1] = 1;

        for (int i = 2; i < n; i++)
        {
            fibonacci[i] = fibonacci[i - 1] + fibonacci[i - 2];
        }

        return fibonacci;
    }
}
