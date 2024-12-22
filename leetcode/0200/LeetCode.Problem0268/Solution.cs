new Solution().MissingNumber([9, 6, 4, 2, 3, 5, 7, 0, 1]);

public class Solution
{
    public int MissingNumber(int[] nums)
    {
        int n = nums.Length;
        int expectedSum = n * (n + 1) / 2;

        int actualSum = 0;
        foreach (int num in nums)
        {
            actualSum += num;
        }

        return expectedSum - actualSum;
    }
}