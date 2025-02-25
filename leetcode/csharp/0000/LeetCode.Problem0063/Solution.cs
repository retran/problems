public class Solution
{
    private readonly IDictionary<(int, int), int> _cache = new Dictionary<(int, int), int>();

    public int UniquePathsWithObstacles(int[][] obstacleGrid, int m, int n)
    {
        int count = 0;

        if (_cache.TryGetValue((m, n), out count))
        {
            return count;
        }

        if (obstacleGrid[m - 1][n - 1] == 1)
        {
            count = 0;
        }
        else if (m == 1 && n == 1)
        {
            count = 1;
        }
        else if (m == 1)
        {
            count = UniquePathsWithObstacles(obstacleGrid, m, n - 1);
        }
        else if (n == 1)
        {
            count = UniquePathsWithObstacles(obstacleGrid, m - 1, n);
        }
        else
        {
            count = UniquePathsWithObstacles(obstacleGrid, m, n - 1) 
                + UniquePathsWithObstacles(obstacleGrid, m - 1, n);
        }

        _cache[(m, n)] = count;
        
        return count;
    }

    public int UniquePathsWithObstacles(int[][] obstacleGrid)
    {
        return UniquePathsWithObstacles(obstacleGrid, obstacleGrid.Length, obstacleGrid[0].Length);
    }
}