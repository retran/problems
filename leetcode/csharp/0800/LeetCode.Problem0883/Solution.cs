public class Solution
{
    public int ProjectionArea(int[][] grid)
    {
        int n = grid.Length;
        int m = grid[0].Length;

        int sum = 0;

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (grid[i][j] > 0)
                {
                    sum++;
                }
            }
        }

        for (int i = 0; i < n; i++)
        {
            int max = int.MinValue;
            for (int j = 0; j < m; j++)
            {
                max = Math.Max(max, grid[i][j]);
            }
            sum += max;
        }

        for (int j = 0; j < m; j++)
        {
            int max = int.MinValue;
            for (int i = 0; i < n; i++)
            {
                max = Math.Max(max, grid[i][j]);
            }
            sum += max;
        }

        return sum;
    }
}