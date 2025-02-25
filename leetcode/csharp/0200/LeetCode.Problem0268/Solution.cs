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