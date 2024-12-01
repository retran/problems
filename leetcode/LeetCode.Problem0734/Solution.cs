public class Solution
{
    public bool AreSentencesSimilar(string[] sentence1, string[] sentence2, IList<IList<string>> similarPairs)
    {
        if (sentence1.Length != sentence2.Length)
        {
            return false;
        }

        if (sentence1.Length == 0)
        {
            return true;
        }

        var map = new Dictionary<string, HashSet<string>>();

        foreach (var pair in similarPairs)
        {
            if (!map.TryGetValue(pair[0], out var set1))
            {
                set1 = new HashSet<string>();
            }
            set1.Add(pair[1]);
            map[pair[0]] = set1;

            if (!map.TryGetValue(pair[1], out var set2))
            {
                set2 = new HashSet<string>();
            }
            set2.Add(pair[0]);
            map[pair[1]] = set2;
        }

        for (int i = 0; i < sentence1.Length; i++)
        {
            var similar = false;

            similar = similar || sentence1[i] == sentence2[i];

            HashSet<string> mapped1 = null;
            HashSet<string> mapped2 = null;

            similar = similar || (map.TryGetValue(sentence1[i], out mapped1) && mapped1.Contains(sentence2[i]));
            similar = similar || (map.TryGetValue(sentence2[i], out mapped2) && mapped2.Contains(sentence1[i]));

            if (!similar)
            {
                return false;
            }
        }

        return true;
    }
}