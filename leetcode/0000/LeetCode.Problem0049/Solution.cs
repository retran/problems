// https://leetcode.com/problems/group-anagrams

public class Solution
{
    public string CreateKey(string text) 
    {
            var count = new int[26];

            foreach (var ch in text)
            {
                count[ch - 'a']++;
            }

            return string.Join('#', count); 
    }

    public IList<IList<string>> GroupAnagrams(string[] strs)
    {
        Dictionary<string, IList<string>> anagrams = new Dictionary<string, IList<string>>();

        foreach (var str in strs)
        {
            var key = CreateKey(str);
            if (!anagrams.ContainsKey(key))
            {
                anagrams[key] = new List<string>();
            }

            anagrams[key].Add(str);
        }

        return anagrams.Values.ToList();
    }
}