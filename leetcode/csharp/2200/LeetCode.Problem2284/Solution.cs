public class Solution
{
    public string LargestWordCount(string[] messages, string[] senders)
    {
        var wordsCount = new Dictionary<string, int>();

        for (int i = 0; i < messages.Length; i++)
        {
            var words = messages[i]
                .Count(x => x == ' ') + 1;
            
            if (!wordsCount.TryGetValue(senders[i], out var count))
            {
                count = 0;
            }

            wordsCount[senders[i]] = count + words;
        }

        int max = 0;
        string name = string.Empty;
        foreach (var kvp in wordsCount) 
        {
            if (kvp.Value == max && string.CompareOrdinal(kvp.Key, name) > 0)
            {
                name = kvp.Key;
            }

            if (kvp.Value > max)
            {
                max = kvp.Value;
                name = kvp.Key;
            }
        }

        return name;
    }
}