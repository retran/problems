public class Solution
{
    private Dictionary<(int, int), int> _cache = new Dictionary<(int, int), int>();

    private int MaxValueOfCoins(int[][] prefixSums, int k, int pile)
    {
        if (k == 0 || pile < 0)
        {
            return 0;
        }

        if (_cache.TryGetValue((k, pile), out var cached))
        {
            return cached;
        }

        var prefixSum = prefixSums[pile];

        int max = MaxValueOfCoins(prefixSums, k, pile - 1);
        for (int i = 1; i <= k && i <= prefixSum.Length; i++)
        {
            max = Math.Max(max, MaxValueOfCoins(prefixSums, k - i, pile - 1) + prefixSum[i - 1]);
        }

        _cache[(k, pile)] = max;
        return max;
    }

    public int MaxValueOfCoins(IList<IList<int>> piles, int k)
    {
        int[][] prefixSums = new int[piles.Count][];
        for (int j = 0; j < piles.Count; j++)
        {
            var pile = piles[j];
            var prefixSum = new int[pile.Count];
            if (pile.Count > 0)
            {
                prefixSum[0] = pile[0];
                for (int i = 1; i < pile.Count; i++)
                {
                    prefixSum[i] = pile[i] + prefixSum[i - 1];
                }
            }
            prefixSums[j] = prefixSum;
        }

        return MaxValueOfCoins(prefixSums, k, prefixSums.Length - 1);
    }
}
