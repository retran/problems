public class Solution
{
    public int MaxEnvelopes(int[][] envelopes)
    {
        var array = envelopes
            .OrderBy(envelope => envelope[0])
            .ThenByDescending(envelope => envelope[1])
            .Select(envelopes => envelopes[1])
            .ToArray();

        int[] dp = new int[array.Length];
        int length = 0;

        foreach (var value in array)
        {
            int i = Array.BinarySearch(dp, 0, length, value);

            if (i < 0)
            {
                i = -(i + 1);
            }

            dp[i] = value;
            if (i == length)
            {
                length++;
            }
        }

        return length;
    }
}