public class Solution
{
    public bool AreSentencesSimilarTwo(string[] sentence1, string[] sentence2, IList<IList<string>> similarPairs)
    {
        if (sentence1.Length != sentence2.Length)
        {
            return false;
        }

        if (sentence1.Length == 0)
        {
            return true;
        }

        var map = new Dictionary<string, HashSet<string>>();

        foreach (var pair in similarPairs)
        {
            if (!map.TryGetValue(pair[0], out var set1))
            {
                set1 = new HashSet<string>();
            }
            set1.Add(pair[1]);
            map[pair[0]] = set1;

            if (!map.TryGetValue(pair[1], out var set2))
            {
                set2 = new HashSet<string>();
            }
            set2.Add(pair[0]);
            map[pair[1]] = set2;
        }

        bool AreSimilar(string word1, string word2)
        {
            var queue = new Queue<string>();
            var visited = new HashSet<string>();

            queue.Enqueue(word1);

            while (queue.Count > 0)
            {
                var word = queue.Dequeue();

                if (visited.Contains(word))
                {
                    continue;
                }

                visited.Add(word);

                if (!map.TryGetValue(word, out var set))
                {
                    continue;
                }

                if (set.Contains(word2))
                {
                    return true;
                }

                foreach (var nextWord in set)
                {
                    queue.Enqueue(nextWord);
                }
            }

            return false;
        }

        for (int i = 0; i < sentence1.Length; i++)
        {
            var similar = false;

            similar = similar || sentence1[i] == sentence2[i];

            similar = similar || AreSimilar(sentence1[i], sentence2[i]);
            similar = similar || AreSimilar(sentence2[i], sentence1[i]);

            if (!similar)
            {
                return false;
            }
        }

        return true;
    }
}