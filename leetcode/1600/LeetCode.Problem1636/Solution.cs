public class Solution
{
    public int[] FrequencySort(int[] nums)
    {
        var map = new Dictionary<int, int>();
        foreach (var num in nums)
        {
            if (map.ContainsKey(num))
            {
                map[num]++;
            }
            else
            {
                map[num] = 1;
            }
        }

        return map
            .OrderBy(kvp => kvp.Value)
            .ThenByDescending(kvp => kvp.Key)
            .SelectMany(kvp => Enumerable.Repeat(kvp.Key, kvp.Value))
            .ToArray();
    }
}