public class Solution
{
    private Dictionary<int, int> _cache = new Dictionary<int, int>();

    public int FindLongestChain(int[][] pairs, int i)
    {
        if (_cache.TryGetValue(i, out var cached))
        {
            return cached;
        }

        int answer = 0;
        if (i == 0)
        {
            answer = 1;
        }
        else
        {
            int max = 1;
            for (int j = i - 1; j >= 0; j--)
            {
                if (pairs[j][1] < pairs[i][0])
                {
                    max = Math.Max(max, FindLongestChain(pairs, j) + 1);
                }
            }
            answer = max;
        }

        _cache[i] = answer;
        return answer;
    }

    public int FindLongestChain(int[][] pairs)
    {
        pairs = pairs.OrderBy(p => p[0]).ToArray();

        return FindLongestChain(pairs, pairs.Length - 1);
    }
}