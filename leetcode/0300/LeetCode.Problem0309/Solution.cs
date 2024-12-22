public class Solution 
{
    private Dictionary<(int, bool, bool), int> _cache = new Dictionary<(int, bool, bool), int>();

    public int MaxProfit(int[] prices, int currentDay, bool hasStock, bool isCooldown)
    {
        if (currentDay == 0)
        {
            if (hasStock)
            {
                throw new InvalidOperationException();
            }

            return 0;
        }

        if (_cache.TryGetValue((currentDay, hasStock, isCooldown), out var cached))
        {
            return cached;
        }

        int profit = int.MinValue;
        if (hasStock)
        {
            if (currentDay - 1 == 0)
            {
                profit = MaxProfit(prices, currentDay - 1, false, false) - prices[currentDay - 1];
            }
            else
            {
                if (!isCooldown)
                {
                    profit = Math.Max(MaxProfit(prices, currentDay - 1, true, false), MaxProfit(prices, currentDay - 1, true, true));
                }

                profit = Math.Max(
                    profit,
                    MaxProfit(prices, currentDay - 1, false, false) - prices[currentDay - 1]);
            }
        }
        else
        {
            if (currentDay - 1 == 0)
            {
                profit = MaxProfit(prices, currentDay - 1, false, false);
            }
            else
            {
                if (isCooldown)
                {
                    profit = MaxProfit(prices, currentDay - 1, true, false) + prices[currentDay - 1];
                }
                else
                {
                    profit = Math.Max(
                        MaxProfit(prices, currentDay - 1, false, true),
                        MaxProfit(prices, currentDay - 1, false, false));
                }
            }
        }

        _cache[(currentDay, hasStock, isCooldown)] = profit;
        return profit;
    }

    public int MaxProfit(int[] prices) 
    {
        return Math.Max(
            MaxProfit(prices, prices.Length, false, false),
            MaxProfit(prices, prices.Length, false, true));
    }
}