public class Solution
{
    private Dictionary<string, bool> _cache = new Dictionary<string, bool>();

    public bool WordBreak(string s, IList<string> wordDict)
    {
        if (string.IsNullOrEmpty(s))
        {
            return true;
        }

        if (_cache.TryGetValue(s, out var answer))
        {
            return answer;
        }

        _cache[s] = false;

        foreach (var word in wordDict)
        {
            if (s.StartsWith(word))
            {
                _cache[s] |= WordBreak(s.Remove(0, word.Length), wordDict);
            }
        }

        return _cache[s];
    }
}