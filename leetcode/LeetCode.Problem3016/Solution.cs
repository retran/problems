public class Solution
{
    public int MinimumPushes(string word)
    {
        if (word.Length == 0)
        {
            return 0;
        }

        var frequencies = new Dictionary<char, int>();

        for (int i = 0; i < word.Length; i++)
        {
            if (!frequencies.TryGetValue(word[i], out var count))
            {
                count = 0;
            }
            frequencies[word[i]] = count + 1;
        }

        var mapping = new Dictionary<int, List<char>>();
        var keypresses = new Dictionary<char, (int btn, int count)>();

        int counter = 2;
        var characters = frequencies
            .ToList()
            .OrderByDescending(f => f.Value)
            .ToList();

        foreach (var (character, _) in characters)
        {
            if (!mapping.TryGetValue(counter, out var list))
            {
                list = new List<char>();
                mapping[counter] = list;
            }
            list.Add(character);
            keypresses[character] = (counter, list.Count);
            counter++;
            if (counter > 9)
            {
                counter = 2;
            }
        }

        int keypressCount = 0;

        for (int i = 0; i < word.Length; i++)
        {
            keypressCount += keypresses[word[i]].count;
        }

        return keypressCount;

    }
}