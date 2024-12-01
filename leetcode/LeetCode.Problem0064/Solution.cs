public class Solution
{
    private readonly IDictionary<(int, int), int> _cache = new Dictionary<(int, int), int>();

    public int MinPathSum(int[][] grid, int m, int n)
    {
        if (m == 1 && n == 1)
        {
            return grid[m - 1][n - 1];
        }

        if (_cache.TryGetValue((m, n), out var cached))
        {
            return cached;
        }

        int sum = 0;
        if (m == 1)
        {
            sum = MinPathSum(grid, m, n - 1) + grid[m - 1][n - 1];
        }
        else if (n == 1)
        {
            sum = MinPathSum(grid, m - 1, n) + grid[m - 1][n - 1];
        }
        else
        {
            sum = Math.Min(MinPathSum(grid, m - 1, n), MinPathSum(grid, m, n - 1)) + grid[m - 1][n - 1];
        }
        
        _cache[(m, n)] = sum;
        return sum;
    }


    public int MinPathSum(int[][] grid)
    {
        return MinPathSum(grid, grid.Length, grid[0].Length);
    }
}