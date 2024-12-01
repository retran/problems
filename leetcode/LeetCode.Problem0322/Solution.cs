public class Solution
{
    private Dictionary<int, int> _cache = new Dictionary<int, int>();

    public int CoinChange(int[] coins, int amount)
    {
        if (amount == 0)
        {
            return 0;
        }

        if (_cache.TryGetValue(amount, out var cached))
        {
            return cached;
        }

        int min = int.MaxValue;

        foreach (int coin in coins)
        {
            if (amount >= coin)
            {
                int result = CoinChange(coins, amount - coin);
                if (result >= 0)
                {
                    min = Math.Min(min, result + 1);
                }
            }
        }

        _cache[amount] = (min == int.MaxValue) 
            ? -1 
            : min;

        return _cache[amount];
    }
}
