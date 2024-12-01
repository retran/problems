public class Solution
{
    public int MaxProfit(int[] prices)
    {
        if (prices.Length < 2)
        {
            return 0;
        }

        int buyPrice = prices[0];
        int profit = 0;

        for (int i = 1; i < prices.Length; i++)
        {
            if (prices[i] < buyPrice)
            {
                buyPrice = prices[i];
            }
            else
            {
                int newProfit = prices[i] - buyPrice;
                if (newProfit > profit)
                {
                    profit = newProfit;
                }
            }
        }

        return profit;
    }
}