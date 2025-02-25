public class Solution
{
    private IDictionary<(int, int), int> _cache = new Dictionary<(int, int), int>();

    public int MinFallingPathSum(int[][] matrix, int m, int n)
    {
        if (m == 0)
        {
            return matrix[m][n];
        }

        if (_cache.TryGetValue((m, n), out var cached))
        {
            return cached;
        }

        int min = int.MaxValue; 
        
        min = Math.Min(min, MinFallingPathSum(matrix, m - 1, n) + matrix[m][n]);

        if (n > 0)
        {
            min = Math.Min(min, MinFallingPathSum(matrix, m - 1, n - 1) + matrix[m][n]);
        }

        if (n < matrix[0].Length - 1)
        {
            min = Math.Min(min, MinFallingPathSum(matrix, m - 1, n + 1) + matrix[m][n]);
        }

        _cache[(m, n)] = min;

        return min;
    }


    public int MinFallingPathSum(int[][] matrix)
    {
        int min = int.MaxValue;
        for (int i = 0; i < matrix[0].Length; i++)
        {
            min = Math.Min(min, MinFallingPathSum(matrix, matrix.Length - 1, i));
        }
        return min;
    }
}