class Solution
{
    private int CalculateMatch(string word1, string word2, Dictionary<string, int> similarityMap)
    {
        if (similarityMap.TryGetValue(word1 + word2, out var value))
        {
            return value;
        }

        int match = 0;
        for (int i = 0; i < 6; i++)
        {
            if (word1[i] == word2[i]) match++;
        }

        similarityMap[word1 + word2] = match;
        similarityMap[word2 + word1] = match;

        return match;
    }

    public string[] Reduce(string[] words, string word, int match, Dictionary<string, int> similarityMap)
    {
        var result = new List<string>();
        foreach (var w in words)
        {
            if (w == word) 
            {
                continue;
            }

            int count = CalculateMatch(w, word, similarityMap);

            if (count == match)
            {
                result.Add(w);
            }
        }

        return result.ToArray();
    }

    private string GetWordWithMaxExpectedElimination(string[] words, Dictionary<string, int> similarityMap)
    {
        double maxExpectedElimination = double.MinValue;
        string bestGuess = "";

        foreach (string guess in words)
        {
            double expectedElimination = 0;
            for (int matchCount = 0; matchCount <= 6; matchCount++)
            {
                int wordsWithMatchCount = words.Count(w => CalculateMatch(guess, w, similarityMap) == matchCount);
                double probability = wordsWithMatchCount / (double)words.Length;
                expectedElimination += probability * (words.Length - wordsWithMatchCount);
            }

            if (expectedElimination > maxExpectedElimination)
            {
                maxExpectedElimination = expectedElimination;
                bestGuess = guess;
            }
        }

        return bestGuess;
    }

    public void FindSecretWord(string[] words, Master master)
    {
        Dictionary<string, int> similarityMap = new Dictionary<string, int>();
        int count = 30;
        var random = new Random();
        for (int i = 0; i < count; i++)
        {
            var guess = GetWordWithMaxExpectedElimination(words, similarityMap);
            int match = master.Guess(guess);
            if (match == 6)
            {
                return;
            }
            words = Reduce(words, guess, match, similarityMap);
        }
    }
}