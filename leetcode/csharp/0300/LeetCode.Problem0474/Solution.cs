public class Solution
{
    private readonly Dictionary<(int, int, int), int> _cache = new();

    private (int, int) CountZeroesAndOnes(string str)
    {
        int zeroes = 0;
        int ones = 0;
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == '0')
            {
                zeroes++;
            }
            else
            {
                ones++;
            }
        }

        return (zeroes, ones);
    }

    private IDictionary<int, (int zeroes, int ones)> BuildMap(string[] strs)
    {
        var map = new Dictionary<int, (int, int)>();
        for (int i = 0; i < strs.Length; i++)
        {
            map[i] = CountZeroesAndOnes(strs[i]);
        }

        return map;
    }

    public int FindMaxForm(IDictionary<int, (int zeroes, int ones)> map, int m, int n, int totalZeroes, int totalOnes, int k)
    {
        if (k < 0)
        {
            return 0;
        }

        if (_cache.TryGetValue((totalZeroes, totalOnes, k), out var cached))
        {
            return cached;
        }

        var (zeroes, ones) = map[k];
        int length = FindMaxForm(map, m, n, totalZeroes, totalOnes, k - 1);

        if (totalZeroes + zeroes <= m && totalOnes + ones <= n)
        {
            length = Math.Max(
                length,
                FindMaxForm(map, m, n, totalZeroes + zeroes, totalOnes + ones, k - 1) + 1
            );
        }

        _cache[(totalZeroes, totalOnes, k)] = length;
        return length;
    }


    public int FindMaxForm(string[] strs, int m, int n)
    {
        var map = BuildMap(strs);

        return FindMaxForm(map, m, n, 0, 0, strs.Length - 1);
    }
}