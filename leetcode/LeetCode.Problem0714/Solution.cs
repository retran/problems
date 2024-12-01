public class Solution
{
    private readonly IDictionary<(int, bool), int> _cache = new Dictionary<(int, bool), int>();

    public int MaxProfit(int[] prices, int fee, int day, bool hasStock)
    {
        if (day == 0)
        {
            return 0;
        }

        if (_cache.TryGetValue((day, hasStock), out var cached))
        {
            return cached;
        }

        int profit;
        if (hasStock)
        {
            if (day == 1)
            {
                profit = -prices[0] - fee;
            }
            else
            {
                profit = Math.Max(
                    MaxProfit(prices, fee, day - 1, false) - prices[day - 1] - fee,
                    MaxProfit(prices, fee, day - 1, true));
            }
        }
        else
        {
            if (day == 1)
            {
                return 0;
            }
            else
            {
                profit = Math.Max(
                    MaxProfit(prices, fee, day - 1, true) + prices[day - 1],
                    MaxProfit(prices, fee, day - 1, false)
                );
            }
        }

        _cache[(day, hasStock)] = profit;
        return profit;
    }


    public int MaxProfit(int[] prices, int fee)
    {
        return MaxProfit(prices, fee, prices.Length, false);
    }
}