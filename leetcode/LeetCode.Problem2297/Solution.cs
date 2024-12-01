public class Solution
{
    public long MinCost(int[] nums, int[] costs)
    {
        var stack1 = new Stack<int>();
        var stack2 = new Stack<int>();
        long[] dp = new long[nums.Length];
        Array.Fill(dp, long.MaxValue);
        dp[0] = 0;

        for (int i = 0; i < nums.Length; i++)
        {
            while (stack1.Count > 0 && nums[i] >= nums[stack1.Peek()])
            {
                dp[i] = Math.Min(dp[i], dp[stack1.Pop()] + costs[i]); // condition 1
            }

            while (stack2.Count > 0 && nums[i] < nums[stack2.Peek()])
            {
                dp[i] = Math.Min(dp[i], dp[stack2.Pop()] + costs[i]); // condition 2
            }

            stack1.Push(i);
            stack2.Push(i);
        }

        return dp[nums.Length - 1];
    }
}
