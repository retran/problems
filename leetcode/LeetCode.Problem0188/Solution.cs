public class Solution
{
    private Dictionary<(int, int, bool), int> _cache = new Dictionary<(int, int, bool), int>();

    public int MaxProfit(int[] prices, int currentDay, int numberOfSells, bool hasStocks)
    {
        if (currentDay == 0)
        {
            if (hasStocks)
            {
                throw new InvalidOperationException();
            }
            return 0;
        }

        if (_cache.TryGetValue((currentDay, numberOfSells, hasStocks), out var cached))
        {
            return cached;
        }

        int max = int.MinValue;
        if (hasStocks)
        {
            if (currentDay - 1 == 0)
            {
                max = MaxProfit(prices, currentDay - 1, numberOfSells, false) - prices[currentDay - 1];
            }
            else
            {
                // купил на прошлом ходу
                // ничего не делал на прошлом ходу
                max = Math.Max(
                    MaxProfit(prices, currentDay - 1, numberOfSells, false) - prices[currentDay - 1],
                    MaxProfit(prices, currentDay - 1, numberOfSells, true)
                );
            }
        }
        else
        {
            if (numberOfSells > 0)
            {
                if (currentDay - 1 == 0)
                {
                    max = MaxProfit(prices, currentDay - 1, numberOfSells, false);
                }
                else
                {

                    // продал на прошлом ходу
                    // ничего не делал на прошлом ходу
                    max = Math.Max(
                        MaxProfit(prices, currentDay - 1, numberOfSells - 1, true) + prices[currentDay - 1],
                        MaxProfit(prices, currentDay - 1, numberOfSells, false)
                    );
                }
            }
            else
            {
                max = MaxProfit(prices, currentDay - 1, numberOfSells, false);
            }
        }

        _cache[(currentDay, numberOfSells, hasStocks)] = max;

        return max;
    }

    public int MaxProfit(int k, int[] prices)
    {
        return MaxProfit(prices, prices.Length, k, false);
    }
}