public class Solution
{
    public long MaximumBooks(int[] books)
    {

        long CalculateSum(int left, int right)
        {
            long count = Math.Min(books[right], right - left + 1);
            return (2 * books[right] - (count - 1)) * count / 2;
        }

        var stack = new Stack<int>();
        
        long[] dp = new long[books.Length];

        for (int i = 0; i < books.Length; i++)
        {
            while (stack.Count > 0 && books[stack.Peek()] - stack.Peek() >= books[i] - i)
            {
                stack.Pop();
            }

            if (stack.Count == 0)
            {
                dp[i] = CalculateSum(0, i);
            }
            else
            {
                int j = stack.Peek();
                dp[i] = dp[j] + CalculateSum(j + 1, i);
            }

            stack.Push(i);
        }

        return dp.Max();
    }
}
