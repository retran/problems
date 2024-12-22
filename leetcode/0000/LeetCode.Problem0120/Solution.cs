public class Solution
{
    private Dictionary<(int, int), int> _cache = new();

    private int MinimumPathToTop(IList<IList<int>> triangle, int i, int j)
    {
        if (_cache.TryGetValue((i, j), out var cached))
        {
            return cached;
        }

        if (i == 0 && j == 0)
        {
            _cache[(i, j)] = triangle[i][j];
            return triangle[i][j];
        }

        var min = int.MaxValue;

        if (j < triangle[i].Count - 1)
        {
            min = Math.Min(min, MinimumPathToTop(triangle, i - 1, j));
        }

        if (j > 0)
        {
            min = Math.Min(min, MinimumPathToTop(triangle, i - 1, j - 1));
        }

        min += triangle[i][j];

        _cache[(i, j)] = min;
        return min;
    }

    public int MinimumTotal(IList<IList<int>> triangle)
    {
        var min = int.MaxValue;

        for (int j = 0; j < triangle[triangle.Count - 1].Count; j++)
        {
            min = Math.Min(min, MinimumPathToTop(triangle, triangle.Count - 1, j));
        }

        return min;
    }
}