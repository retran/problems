public class Solution
{
    public int NumMagicSquaresInside(int[][] grid)
    {
        int count = 0;
        for (int i = 0; i < grid.Length - 2; i++)
        {
            for (int j = 0; j < grid[0].Length - 2; j++)
            {
                if (IsMagic(grid, i, j))
                {
                    count++;
                }
            }
        }
        return count;
    }

    private bool IsMagic(int[][] grid, int row, int col)
    {
        int[] nums = new int[10];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int num = grid[row + i][col + j];
                if (num < 1 || num > 9 || nums[num] == 1)
                {
                    return false;
                }
                nums[num] = 1;
            }
        }

        return grid[row][col] + grid[row][col + 1] + grid[row][col + 2] == 15 &&
                grid[row + 1][col] + grid[row + 1][col + 1] + grid[row + 1][col + 2] == 15 &&
                grid[row + 2][col] + grid[row + 2][col + 1] + grid[row + 2][col + 2] == 15 &&
                grid[row][col] + grid[row + 1][col] + grid[row + 2][col] == 15 &&
                grid[row][col + 1] + grid[row + 1][col + 1] + grid[row + 2][col + 1] == 15 &&
                grid[row][col + 2] + grid[row + 1][col + 2] + grid[row + 2][col + 2] == 15 &&
                grid[row][col] + grid[row + 1][col + 1] + grid[row + 2][col + 2] == 15 &&
                grid[row][col + 2] + grid[row + 1][col + 1] + grid[row + 2][col] == 15;
    }
}