public class Solution
{
    private readonly Dictionary<(int, int, int, int), bool> _cached = new();

    public bool IsThereAPath(int[][] grid, int i, int j, int zeroes, int ones)
    {
        if (i < 0 || j < 0)
        {
            return false;
        }

        if (grid[i][j] == 0)
        {
            zeroes++;
        }
        else
        {
            ones++;
        }

        if (i == 0 && j == 0)
        {
            return zeroes == ones;
        }

        if (_cached.TryGetValue((i, j, zeroes, ones), out var cached))
        {
            return cached;
        }

        bool hasPath = false;


        hasPath = hasPath || IsThereAPath(grid, i - 1, j, zeroes, ones);
        hasPath = hasPath || IsThereAPath(grid, i, j - 1, zeroes, ones);

        _cached[(i, j, zeroes, ones)] = hasPath;
        return hasPath;
    }


    public bool IsThereAPath(int[][] grid)
    {
        return IsThereAPath(grid, grid.Length - 1, grid[0].Length - 1, 0, 0);
    }
}