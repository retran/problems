public class Solution
{
    public int CreateKey(string word) 
    {
        int key = 0;
        foreach (char c in word)
        {
            int bit = 1 << (c - 'a');
            key |= bit;
        }
        return key;
    }

    public int SimilarPairs(string[] words)
    {
        int n = words.Length;
        int[] keys = new int[n];
        
        for (int i = 0; i < n; i++)
        {
            keys[i] = CreateKey(words[i]);
        }

        int result = 0;
        var count = new Dictionary<int, int>();
        
        foreach (int key in keys)
        {
            if (count.TryGetValue(key, out int currentCount))
            {
                result += currentCount;
                count[key] = currentCount + 1;
            }
            else
            {
                count[key] = 1;
            }
        }
        
        return result;
    }
}
