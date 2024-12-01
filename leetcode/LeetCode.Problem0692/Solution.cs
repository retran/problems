public class Solution
{
    public IList<string> TopKFrequent(string[] words, int k)
    {
        var frequencies = new Dictionary<string, int>();

        foreach (var word in words)
        {
            if (!frequencies.TryGetValue(word, out var count))
            {
                count = 0;
            }
            frequencies[word] = count + 1;
        }

        return frequencies
            .OrderByDescending(p => p.Value)
            .ThenBy(p => p.Key)
            .Take(k)
            .Select(p => p.Key)
            .ToList();
    }
}