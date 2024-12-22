public class Solution
{
    private Dictionary<(int, int), int> _cache = new Dictionary<(int, int), int>();

    public int Change(int amount, int[] coins, int biggestCoint)
    {
        if (amount == 0)
        {
            return 1;
        }

        if (_cache.TryGetValue((amount, biggestCoint), out var cached))
        {
            return cached;
        }

        int count = 0;
        for (int i = 0; i < coins.Length; i++)
        {
            if (amount >= coins[i] && coins[i] <= biggestCoint)
            {
                var result = Change(amount - coins[i], coins, coins[i]);
                count += result;
            }
        }

        _cache[(amount, biggestCoint)] = count;

        return count;
    }

    public int Change(int amount, int[] coins)
    {
        return Change(amount, coins, int.MaxValue);
    }
}