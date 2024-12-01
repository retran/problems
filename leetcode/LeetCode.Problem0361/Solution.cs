public class Solution
{
    public int MaxKilledEnemies(char[][] grid)
    {
        int[,] counts = new int[grid.Length, grid[0].Length];
        for (int i = 0; i < grid.Length; i++)
        {
            int count = 0;
            int from = 0;
            for (int j = 0; j < grid[0].Length; j++)
            {
                if (grid[i][j] == 'E')
                {
                    count++;
                }
                if (grid[i][j] == 'W')
                {
                    for (int k = from; k < j; k++)
                    {
                        if (grid[i][k] == '0')
                        {
                            counts[i, k] = count;
                        }
                    }
                    from = j + 1;
                    count = 0;
                }
            }

            for (int k = from; k < grid[0].Length; k++)
            {
                if (grid[i][k] == '0')
                {
                    counts[i, k] = count;
                }
            }
        }

        for (int j = 0; j < grid[0].Length; j++)
        {
            int count = 0;
            int from = 0;
            for (int i = 0; i < grid.Length; i++)
            {
                if (grid[i][j] == 'E')
                {
                    count++;
                }
                if (grid[i][j] == 'W')
                {
                    for (int k = from; k < i; k++)
                    {
                        if (grid[k][j] == '0')
                        {
                            counts[k, j] += count;
                        }
                    }
                    from = i + 1;
                    count = 0;
                }
            }

            for (int k = from; k < grid.Length; k++)
            {
                if (grid[k][j] == '0')
                {
                    counts[k, j] += count;
                }
            }
        }

        int max = 0;
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[0].Length; j++)
            {
                if (grid[i][j] == '0')
                {
                    max = Math.Max(max, counts[i, j]);
                }
            }
        }

        return max;
    }
}