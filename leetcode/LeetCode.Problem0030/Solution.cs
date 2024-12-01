// https://leetcode.com/problems/substring-with-concatenation-of-all-words

// alternate approach - frequency map of words in sliding window

public class Solution
{
    private bool IsConcantenatedString(string text, int index, string[] words, string pattern)
    {
        if (text.Substring(index, pattern.Length) == pattern) return true;

        var wordLength = words[0].Length;
        var wordsCount = words.Length;
        var wordCounts = new Dictionary<string, int>();
        foreach (var word in words)
            if (wordCounts.ContainsKey(word))
                wordCounts[word]++;
            else
                wordCounts[word] = 1;

        for (var i = 0; i < wordsCount; i++)
        {
            var word = text.Substring(index + i * wordLength, wordLength);
            if (wordCounts.ContainsKey(word))
            {
                if (wordCounts[word] == 0) return false;
                wordCounts[word]--;
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    public IList<int> FindSubstring(string s, string[] words)
    {
        var result = new List<int>();
        if (words.Length == 0 || s.Length == 0) return result;

        var pattern = string.Join("", words);

        if (s.Length < pattern.Length) return result;

        var patternHash = new RollingHash(pattern, words.Length, words[0].Length);
        var rollingHash = new RollingHash(s, words.Length, words[0].Length);

        while (rollingHash.Current < s.Length - pattern.Length + 1)
        {
            if (rollingHash.Hash == patternHash.Hash)
                if (IsConcantenatedString(s, rollingHash.Current, words, pattern))
                    result.Add(rollingHash.Current);
            rollingHash.Slide();
        }

        return result;
    }

    private class RollingHash
    {
        private readonly long _base;
        private readonly long _modulus;
        private readonly string _text;
        private readonly int _wordLength;
        private readonly int _words;
        private readonly long _multiplier;
        private readonly long[] _wordHashes;

        public RollingHash(string text,
            int words,
            int wordLength,
            long baseValue = 101,
            long modulus = 1000000007)
        {
            _text = text;
            _words = words;
            _wordLength = wordLength;
            _base = baseValue;
            _modulus = modulus;
            Current = 0;
            _multiplier = 1;
            _wordHashes = new long[words];

            for (var i = 0; i < wordLength - 1; i++)
            {
                for (var j = 0; j < words; j++)
                {
                    var c = text[i + j * wordLength];
                    _wordHashes[j] = (_wordHashes[j] * _base + c) % _modulus;
                }

                _multiplier = _multiplier * _base % _modulus;
            }

            UpdateHash();
        }

        public long Hash { get; private set; }

        public int Current { get; private set; }

        public void Slide()
        {
            for (var j = 0; j < _words; j++)
            {
                var removedChar = _text[Current + j * _wordLength];
                var addedChar = _text[Current + (j + 1) * _wordLength - 1];

                _wordHashes[j] = (_wordHashes[j] * _base - removedChar * _multiplier % _modulus + _modulus) % _modulus;
                _wordHashes[j] = (_wordHashes[j] + addedChar) % _modulus;
            }

            Current++;

            UpdateHash();
        }

        private void UpdateHash()
        {
            Hash = 0;
            for (var j = 0; j < _words; j++) Hash = (Hash + _wordHashes[j]) % _modulus;
        }
    }
}