public class Solution
{
    public int MinDays(int[][] grid)
    {

        if (IsDisconnected(grid))
        {
            return 0;
        }

        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[0].Length; j++)
            {
                if (grid[i][j] == 1)
                {
                    grid[i][j] = 0;
                    if (IsDisconnected(grid))
                    {
                        return 1;
                    }
                    grid[i][j] = 1;
                }
            }
        }

        return 2;
    }

    private bool IsDisconnected(int[][] grid)
    {
        bool[,] visited = new bool[grid.Length, grid[0].Length];
        int islands = 0;

        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[0].Length; j++)
            {
                if (grid[i][j] == 1 && !visited[i, j])
                {
                    islands++;
                    if (islands > 1) 
                    {
                        return true;
                    }
                    Traverse(grid, visited, i, j);
                }
            }
        }

        return islands != 1;
    }

    private void Traverse(int[][] grid, bool[,] visited, int i, int j)
    {
        var directions = new int[][] {
            [1, 0],
            [-1, 0],
            [0, 1],
            [0, -1]
        };

        var stack = new Stack<int[]>();
        stack.Push([i, j]);

        while (stack.Count > 0)
        {
            int[] current = stack.Pop();
            int x = current[0];
            int y = current[1];

            if (x < 0 || y < 0 || x >= grid.Length || y >= grid[0].Length || grid[x][y] == 0 || visited[x, y])
            {
                continue;
            }

            visited[x, y] = true;

            foreach (int[] direction in directions)
            {
                int newX = x + direction[0];
                int newY = y + direction[1];
                stack.Push([newX, newY]);
            }
        }
    }
}