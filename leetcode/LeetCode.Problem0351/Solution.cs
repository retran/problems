public class Solution
{
    private readonly int[] _digits = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

    private bool IsValidConnection(int i, int j, ISet<int> used)
    {
        (int row, int column) btn1 = ((i - 1) / 3, (i - 1) % 3);
        (int row, int column) btn2 = ((j - 1) / 3, (j - 1) % 3);

        if (Math.Abs(btn1.row - btn2.row) <= 1 && Math.Abs(btn1.column - btn2.column) <= 1)
        {
            return true;
        }

        int sum = i + j;

        if (sum % 2 == 1)
        {
            return true;
        }

        if (used.Contains(sum / 2))
        {
            return true;
        }

        return false;
    }

    public int NumberOfPatterns(int m, int n, List<int> pattern)
    {
        if (pattern.Count == n)
        {
            return 1;
        }

        int count = 0;

        if (pattern.Count >= m)
        {
            count++;
        }

        if (pattern.Count == 0)
        {
            foreach (var digit in _digits)
            {
                pattern.Add(digit);
                count += NumberOfPatterns(m, n, pattern);
                pattern.Remove(digit);
            }
        }
        else
        {
            int lastDigit = pattern.Last();
            var set = new HashSet<int>(pattern);
            foreach (var digit in _digits)
            {
                if (set.Contains(digit) || !IsValidConnection(lastDigit, digit, set))
                {
                    continue;
                }

                pattern.Add(digit);
                count += NumberOfPatterns(m, n, pattern);
                pattern.Remove(digit);
            }
        }

        return count;
    }

    public int NumberOfPatterns(int m, int n)
    {
        return NumberOfPatterns(m, n, new List<int>());
    }
}
