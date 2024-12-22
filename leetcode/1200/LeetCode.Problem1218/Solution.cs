public class Solution
{
    private readonly Dictionary<int, int> _cache = new();

    public int LongestSubsequence(int[] arr, int difference, Dictionary<int, List<int>> numberToIndexMap, int i)
    {
        if (_cache.TryGetValue(i, out var cached))
        {
            return cached;
        }

        int answer;
        if (i == 0)
        {
            answer = 1;
        }
        else
        {
            int max = 1;
            int target = arr[i] - difference;

            if (numberToIndexMap.TryGetValue(target, out var indices))
            {
                foreach (var index in indices)
                {
                    if (index < i)
                    {
                        max = Math.Max(max, LongestSubsequence(arr, difference, numberToIndexMap, index) + 1);
                    }
                }
            }

            answer = max;
        }

        _cache[i] = answer;
        return answer;
    }

    public int LongestSubsequence(int[] arr, int difference)
    {
        var numberToIndexMap = new Dictionary<int, List<int>>();

        for (int i = 0; i < arr.Length - 1; i++)
        {
            if (!numberToIndexMap.TryGetValue(arr[i], out var list))
            {
                list = new List<int>() { i };
                numberToIndexMap[arr[i]] = list;
            }
            else
            {
                list.Add(i);
            }
        }

        int max = 0;
        
        for (int i = 0; i <= arr.Length - 1; i++)
        {
            max = Math.Max(max, LongestSubsequence(arr, difference, numberToIndexMap, i));
        }

        return max;
    }
}