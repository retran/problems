public class Solution
{
    private readonly Dictionary<(int, int, int), int> _cache = new Dictionary<(int, int, int), int>();

    public int MinCostImpl(int[] houses, int[][] cost, int lastHouse, int color, int neighborhoods)
    {
        if (neighborhoods < 0) 
        {
            return int.MaxValue;
        }

        int currentCost = houses[lastHouse] == 0 
            ? cost[lastHouse][color - 1] 
            : 0;

        if (lastHouse == 0)
        {
            return neighborhoods == 0 
                ? currentCost 
                : int.MaxValue;
        }

        if (_cache.TryGetValue((lastHouse, color, neighborhoods), out var cached))
        {
            return cached;
        }

        int minCost = int.MaxValue;

        if (houses[lastHouse - 1] != 0)
        {
            if (houses[lastHouse - 1] == color)
            {
                minCost = MinCostImpl(houses, cost, lastHouse - 1, color, neighborhoods);
            }
            else
            {
                minCost = MinCostImpl(houses, cost, lastHouse - 1, houses[lastHouse - 1], neighborhoods - 1);
            }
        }
        else
        {
            minCost = MinCostImpl(houses, cost, lastHouse - 1, color, neighborhoods);

            if (neighborhoods > 0)
            {
                for (int i = 1; i <= cost[0].Length; i++)
                {
                    if (i != color)
                    {
                        minCost = Math.Min(minCost, MinCostImpl(houses, cost, lastHouse - 1, i, neighborhoods - 1));
                    }
                }
            }
        }

        if (minCost != int.MaxValue)
        {
            minCost += currentCost;
        }

        _cache[(lastHouse, color, neighborhoods)] = minCost;

        return minCost;
    }

    public int MinCost(int[] houses, int[][] cost, int m, int n, int target)
    {
        int lastHouseIndex = m - 1;
        int minCost = int.MaxValue;

        if (houses[lastHouseIndex] != 0)
        {
            minCost = MinCostImpl(houses, cost, lastHouseIndex, houses[lastHouseIndex], target - 1);
        }
        else
        {
            for (int i = 1; i <= n; i++)
            {
                minCost = Math.Min(minCost, MinCostImpl(houses, cost, lastHouseIndex, i, target - 1));
            }
        }

        return minCost == int.MaxValue 
            ? -1 
            : minCost;
    }
}
