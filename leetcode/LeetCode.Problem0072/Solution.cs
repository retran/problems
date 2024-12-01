public class Solution
{
    private readonly Dictionary<(int, int), int> _cache = new();

    private int MinDistance(string word1, string word2, int i, int j)
    {
        if (_cache.TryGetValue((i, j), out var cached))
        {
            return cached;
        }

        int distance;
        if (i == -1 && j == -1)
        {
            distance = 0;
        }
        else if (i == -1)
        {
            distance = j + 1;
        }
        else if (j == -1)
        {
            distance = i + 1;
        }
        else
        {
            distance = int.MaxValue;

            if (word1[i] == word2[j])
            {
                distance = MinDistance(word1, word2, i - 1, j - 1);
            }

            distance = Math.Min(
                distance,
                MinDistance(word1, word2, i - 1, j) + 1
            );

            distance = Math.Min(
                distance,
                MinDistance(word1, word2, i, j - 1) + 1
            );

            distance = Math.Min(
                distance,
                MinDistance(word1, word2, i - 1, j - 1) + 1
            );
        }

        _cache[(i, j)] = distance;
        return distance;
    }


    public int MinDistance(string word1, string word2)
    {
        return MinDistance(word1, word2, word1.Length - 1, word2.Length - 1);
    }
}