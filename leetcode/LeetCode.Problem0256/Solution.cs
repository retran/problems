public class Solution
{
    private readonly IDictionary<(int, int), int> _cache = new Dictionary<(int, int), int>();

    public int MinCost(int[][] costs, int n, int lastUsedPaint)
    {
        if (_cache.TryGetValue((n, lastUsedPaint), out var cached))
        {
            return cached;
        }

        int[] paints = costs[n];

        int min = int.MaxValue;
        if (n == 0)
        {
            for (int i = 0; i < paints.Length; i++)
            {
                if (paints[i] < min && i != lastUsedPaint)
                {
                    min = paints[i];
                }
            }
        }
        else
        {
            for (int i = 0; i < paints.Length; i++)
            {
                if (i == lastUsedPaint)
                {
                    continue;
                }

                int totalCost = MinCost(costs, n - 1, i) + paints[i];
                if (totalCost < min)
                {
                    min = totalCost;
                }
            }
        }

        _cache[(n, lastUsedPaint)] = min;
        return min;
    }


    public int MinCost(int[][] costs)
    {
        return MinCost(costs, costs.Length - 1, -1);
    }
}