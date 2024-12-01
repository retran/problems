public class Solution
{
    public int MaximalSquare(char[][] matrix)
    {
        int n = matrix.Length;
        int m = matrix[0].Length;

        int[,] dp = new int[n + 1, m + 1];

        int max = 0;
        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= m; j++)
            {
                if (matrix[i - 1][j - 1] == '1')
                {
                    dp[i, j] = Math.Min(Math.Min(dp[i - 1, j], dp[i, j - 1]), dp[i - 1, j - 1]) + 1;
                    if (dp[i, j] > max)
                    {
                        max = dp[i, j];
                    }
                }
            }
        }

        return max * max;
    }
}