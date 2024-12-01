public class Solution
{
    private readonly ISet<string> _validStrings = new HashSet<string>()
    {
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "10",
        "11", "12", "13", "14", "15", "16", "17", "18", "19", "20",
        "21", "22", "23", "24", "25", "26"
    };

    private readonly IDictionary<string, int> _cache = new Dictionary<string, int>();

    public int NumDecodings(string s)
    {
        if (s.Length == 0)
        {
            return 1;
        }

        if (_cache.TryGetValue(s, out var cached))
        {
            return cached;
        }

        int count = 0;
        var lastChar = s.Substring(s.Length - 1);

        bool valid = false;

        if (s.Length > 0)
        {
            if (_validStrings.Contains(lastChar))
            {
                count += NumDecodings(s.Substring(0, s.Length - 1));
                valid = true;
            }
        }

        if (s.Length > 1)
        {
            var lastTwoChars = s.Substring(s.Length - 2);
            if (_validStrings.Contains(lastTwoChars))
            {
                count += NumDecodings(s.Substring(0, s.Length - 2));
                valid = true;
            }
        }

        if (!valid)
        {
            return 0;
        }

        _cache[s] = count;

        return count;
    }
}