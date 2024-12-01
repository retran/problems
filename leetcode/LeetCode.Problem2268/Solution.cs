public class Solution
{
    public int MinimumKeypresses(string s)
    {
        if (s.Length == 0)
        {
            return 0;
        }

        var frequencies = new Dictionary<char, int>();

        for (int i = 0; i < s.Length; i++)
        {
            if (!frequencies.TryGetValue(s[i], out var count))
            {
                count = 0;
            }
            frequencies[s[i]] = count + 1;
        }

        var mapping = new Dictionary<int, List<char>>();
        var keypresses = new Dictionary<char, (int btn, int count)>();

        int counter = 1;
        var characters = frequencies.ToList().OrderByDescending(f => f.Value).ToList();
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
                counter = 1;
            }
        }

        int keypressCount = 0;

        for (int i = 0; i < s.Length; i++)
        {
            keypressCount += keypresses[s[i]].count;
        }

        return keypressCount;
    }
}