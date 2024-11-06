public class Solution
{
    public int CountNegatives(int[][] grid)
    {
        int i = 0;
        int j = grid[0].Length - 1;
        int count = 0;

        for (; i < grid.Length; i++)
        {
            for (; j >= 0; j--)
            {
                if (grid[i][j] >= 0)
                {
                    count += grid[i].Length - 1 - j;
                    break;
                }
            }

            if (j < 0)
            {
                count += grid[i].Length;
            }
        }

        return count;
    }
}