public class Solution
{
    private readonly (int, int)[] _directions = new (int, int)[] {
        (-1, 0),
        (1, 0),
        (0, -1),
        (0, 1)
    };

    public void GetMaximumGold(int[][] grid, int i, int j, ISet<(int, int)> visited, int current, ref int max)
    {
        if (current > max)
        {
            max = current;
        }

        foreach (var direction in _directions)
        {
            var next = (i + direction.Item1, j + direction.Item2);

            if (next.Item1 < 0 || next.Item1 > grid.Length - 1
                || next.Item2 < 0 || next.Item2 > grid[next.Item1].Length - 1)
            {
                continue;
            }

            int score = grid[next.Item1][next.Item2];
            if (score == 0)
            {
                continue;
            }

            if (!visited.Add(next))
            {
                continue;
            }

            GetMaximumGold(grid, next.Item1, next.Item2, visited, current + score, ref max);

            visited.Remove(next);
        }
    }

    public int GetMaximumGold(int[][] grid)
    {
        int max = 0;
        for (int i = 0; i < grid.Length; i++)
            for (int j = 0; j < grid[i].Length; j++)
            {
                if (grid[i][j] != 0)
                {
                    GetMaximumGold(grid, i, j, new HashSet<(int, int)>() { (i, j) }, grid[i][j], ref max);
                }
            }
        return max;
    }
}