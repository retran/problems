public class Solution
{
    public void Mark(char[][] grid, int i, int j, int count)
    {
        if (i < 0 || j < 0 || i >= grid.Length || j >= grid[0].Length || grid[i][j] != '1')
        {
            return;
        }

        Queue<(int, int)> queue = new Queue<(int, int)>();
        queue.Enqueue((i, j));
        while (queue.Count > 0)
        {
            var (x, y) = queue.Dequeue();
            if (x < 0 || y < 0 || x >= grid.Length || y >= grid[0].Length || grid[x][y] != '1')
            {
                continue;
            }

            grid[x][y] = '0';
            queue.Enqueue((x - 1, y));
            queue.Enqueue((x + 1, y));
            queue.Enqueue((x, y - 1));
            queue.Enqueue((x, y + 1));
        }
    }

    public int NumIslands(char[][] grid)
    {
        var count = 0;
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[0].Length; j++)
            {
                if (grid[i][j] == '1')
                {
                    count++;
                    Mark(grid, i, j, count);
                }
            }
        }

        return count;
    }
}