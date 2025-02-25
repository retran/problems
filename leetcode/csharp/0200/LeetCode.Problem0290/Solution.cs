public class Solution
{
    public bool WordPattern(string pattern, string s)
    {
        var map = new Dictionary<char, string>();
        var reverseMap = new Dictionary<string, char>();
        var words = s.Split(' ');

        if (pattern.Length != words.Length)
        {
            return false;
        }

        for (int i = 0; i < pattern.Length; i++)
        {
            var c = pattern[i];
            var word = words[i];

            if (map.ContainsKey(c))
            {
                if (map[c] != word)
                {
                    return false;
                }
            }
            else
            {
                if (reverseMap.ContainsKey(word))
                {
                    return false;
                }

                map[c] = word;
                reverseMap[word] = c;
            }
        }

        return true;
    }
}