using System.Reflection;

public class Solution {
    public bool CloseStrings(string word1, string word2) 
    {
        if (word1.Length != word2.Length)
        {
            return false;
        }
        
        var map1 = new Dictionary<char, int>();
        var map2 = new Dictionary<char, int>();

        for (int i = 0; i < word1.Length; i++)
        {
            if (!map1.TryGetValue(word1[i], out var count1))
            {
                count1 = 0;
            }
            map1[word1[i]] = count1 + 1;

            if (!map2.TryGetValue(word2[i], out var count2))
            {
                count2 = 0;
            }
            map2[word2[i]] = count2 + 1;
        }

        var set1 = new HashSet<char>(map1.Keys);
        var set2 = new HashSet<char>(map2.Keys);

        if (!set1.SetEquals(set2))
        {
            return false;
        }

        var frequencies1 = map1.Values.OrderBy(v => v).ToArray();
        var frequencies2 = map2.Values.OrderBy(v => v).ToArray();

        for (int i = 0; i < frequencies1.Length; i++)
        {
            if (frequencies1[i] != frequencies2[i])
            {
                return false;
            }
        }

        return true;
    }
}