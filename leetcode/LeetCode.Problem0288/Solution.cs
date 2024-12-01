public class ValidWordAbbr
{
    private Dictionary<string, string> _dictionary = new Dictionary<string, string>();

    private string GetWordAbbr(string word)
    {
        if (word.Length < 3)
        {
            return word;
        }

        var sb = new StringBuilder();
        sb.Append(word[0]);
        sb.Append(word.Length - 2);
        sb.Append(word[word.Length - 1]);

        return sb.ToString();
    }

    public ValidWordAbbr(string[] dictionary)
    {
        for (int i = 0; i < dictionary.Length; i++)
        {
            var abbr = GetWordAbbr(dictionary[i]);
            if (_dictionary.TryGetValue(abbr, out var stored) && stored != dictionary[i])
            {
                _dictionary[abbr] = null;
            }
            else
            {
                _dictionary[abbr] = dictionary[i];
            }
        }
    }

    public bool IsUnique(string word)
    {
        var abbr = GetWordAbbr(word);
        if (_dictionary.TryGetValue(abbr, out var stored))
        {
            return stored == word;
        }

        return true;
    }
}
