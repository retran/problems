public class Solution
{
    private readonly (int dr, int dc)[] _moves = [
            (-2, -1),
            (-2, 1),
            (-1, -2),
            (-1, 2),
            (1, -2),
            (1, 2),
            (2, -1),
            (2, 1)
        ];

    private Dictionary<(int, int, int, int), double> _memoize = new Dictionary<(int, int, int, int), double>();

    public double KnightProbability(int n, int k, int row, int column)
    {
        if (k == 0)
        {
            return 1;
        }

        if (_memoize.TryGetValue((n, k, row, column), out var value))
        {
            return value;
        }

        double probability = 0;

        foreach (var (dr, dc) in _moves)
        {
            int newRow = row + dr;
            int newColumn = column + dc;

            if (newRow >= 0 && newRow < n && newColumn >= 0 && newColumn < n)
            {
                probability += 0.125 * KnightProbability(n, k - 1, newRow, newColumn);
            }
        }

        _memoize[(n, k, row, column)] = probability;

        return probability;
    }
}