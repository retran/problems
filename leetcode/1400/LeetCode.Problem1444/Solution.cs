public class Solution
{
    private Dictionary<(int, int, int), int> _cache = new();
    private const int MOD = 1000000007;

    public int Ways(string[] pizza, int k)
    {
        int[,] apples = new int[pizza.Length + 1, pizza[0].Length + 1];

        for (int i = pizza.Length - 1; i >= 0; i--)
        {
            for (int j = pizza[0].Length - 1; j >= 0; j--)
            {
                apples[i, j] = (pizza[i][j] == 'A' ? 1 : 0) + apples[i + 1, j] + apples[i, j + 1] - apples[i + 1, j + 1];
            }
        }

        return CalculateWays(pizza, k - 1, 0, 0, apples);
    }

    private int CalculateWays(string[] pizza, int cuts, int row, int col, int[,] apples)
    {
        if (apples[row, col] == 0) 
        {
            return 0;
        }

        if (cuts == 0)
        {
            return 1;
        }

        if (_cache.TryGetValue((row, col, cuts), out var cached))
        {
            return cached;
        }

        int count = 0;

        for (int i = row + 1; i < pizza.Length; i++)
        {
            if (apples[row, col] > apples[i, col])
            {
                count = (count + CalculateWays(pizza, cuts - 1, i, col, apples)) % MOD;
            }
        }

        for (int j = col + 1; j < pizza[0].Length; j++)
        {
            if (apples[row, col] > apples[row, j])
            {
                count = (count + CalculateWays(pizza, cuts - 1, row, j, apples)) % MOD;
            }
        }

        _cache[(row, col, cuts)] = count;
        return count;
    }
}