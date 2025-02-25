public class Solution
{
    public bool IsValidTransformation(string wordA, string wordB)
    {
        int count = 0;
        for (int i = 0; i < wordA.Length; i++)
        {
            if (wordA[i] != wordB[i])
            {
                count++;
                if (count > 1)
                {
                    return false;
                }
            }
        }
        return count == 1;
    }

    public int LadderLength(string beginWord, string endWord, IList<string> wordList)
    {
        var words = new string[wordList.Count + 1];
        words[0] = beginWord;
        for (int i = 0; i < wordList.Count; i++)
        {
            words[i + 1] = wordList[i];
        }
        int startIndex = 0;
        int endIndex = -1;

        var graph = new int[words.Length, words.Length];
        for (int i = 0; i < words.Length; i++)
        {
            if (endWord == words[i])
            {
                endIndex = i;
            }

            for (int j = 0; j < words.Length; j++)
            {
                if (i != j && IsValidTransformation(words[i], words[j]))
                {
                    graph[i, j] = 1;
                }
            }
        }

        if (endIndex == -1)
        {
            return 0;
        }

        var visited = new bool[words.Length];
        var queue = new Queue<int>();
        queue.Enqueue(startIndex);
        int steps = 0;

        while (queue.Count > 0)
        {
            int count = queue.Count;
            for (int i = 0; i < count; i++)
            {
                int index = queue.Dequeue();
                if (index == endIndex)
                {
                    return steps + 1;
                }

                for (int j = 0; j < words.Length; j++)
                {
                    if (graph[index, j] == 1 && !visited[j])
                    {
                        visited[j] = true;
                        queue.Enqueue(j);
                    }
                }
            }
            steps++;
        }

        return 0;
    }
}