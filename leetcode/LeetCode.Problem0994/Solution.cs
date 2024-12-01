public class Solution
{
    public int OrangesRotting(int[][] grid)
    {
        var queue = new Queue<(int row, int column)>();

        int orangesCount = 0;
        for (int i = 0; i < grid.Length; i++)
            for (int j = 0; j < grid[0].Length; j++)
            {
                if (grid[i][j] == 2)
                {
                    queue.Enqueue((i, j));
                }

                if (grid[i][j] == 1)
                {
                    orangesCount++;
                }
            }

        if (orangesCount == 0)
        {
            return 0;
        }

        if (queue.Count == 0)
        {
            return -1;
        }

        var directions = new List<(int drow, int dcolumn)>() { (-1, 0), (1, 0), (0, -1), (0, 1) };

        int minutes = 0;
        while (queue.Count > 0)
        {
            minutes++;
            int size = queue.Count;
            while (size > 0)
            {
                var orange = queue.Dequeue();
                size--;
                foreach (var direction in directions)
                {
                    (int row, int column) nextOrange = (orange.row + direction.drow, orange.column + direction.dcolumn);
                    if (nextOrange.row >= 0 && nextOrange.row < grid.Length && nextOrange.column >= 0 && nextOrange.column < grid[0].Length)
                    {
                        if (grid[nextOrange.row][nextOrange.column] == 1)
                        {
                            grid[nextOrange.row][nextOrange.column] = 2;
                            orangesCount--;
                            queue.Enqueue(nextOrange);
                        }
                    }
                }
            }
        }

        return orangesCount == 0 ? minutes - 1 : -1;
    }
}