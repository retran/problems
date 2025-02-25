public class Solution
{
    public IList<int> FindAnagrams(string s, string p)
    {
        var answer = new List<int>();

        if (p.Length > s.Length)
        {
            return answer;
        }

        var targetFrequencies = new Dictionary<char, int>();
        var windowFrequencies = new Dictionary<char, int>();

        foreach (char c in p)
        {
            if (!targetFrequencies.TryGetValue(c, out var count))
            {
                count = 0;
            }
            targetFrequencies[c] = count + 1;
        }

        int pLength = p.Length;
        for (int i = 0; i < pLength; i++)
        {
            char c = s[i];
            if (!windowFrequencies.TryGetValue(c, out var count))
            {
                count = 0;
            }
            windowFrequencies[c] = count + 1;
        }

        if (CompareDictionaries(targetFrequencies, windowFrequencies))
        {
            answer.Add(0);
        }

        for (int i = pLength; i < s.Length; i++)
        {
            char charToRemove = s[i - pLength];
            char charToAdd = s[i];

            windowFrequencies[charToRemove]--;
            if (windowFrequencies[charToRemove] == 0)
            {
                windowFrequencies.Remove(charToRemove);
            }

            if (!windowFrequencies.TryGetValue(charToAdd, out var count))
            {
                count = 0;
            }
            windowFrequencies[charToAdd] = count + 1;

            if (CompareDictionaries(targetFrequencies, windowFrequencies))
            {
                answer.Add(i - pLength + 1);
            }
        }

        return answer;
    }

    private bool CompareDictionaries(Dictionary<char, int> dict1, Dictionary<char, int> dict2)
    {
        if (dict1.Count != dict2.Count)
        {
            return false;
        }

        foreach (var kvp in dict1)
        {
            if (!dict2.TryGetValue(kvp.Key, out var count) || count != kvp.Value)
            {
                return false;
            }
        }

        return true;
    }
}
