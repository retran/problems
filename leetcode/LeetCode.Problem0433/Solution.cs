public class Solution
{
    public bool IsMutationPossible(string gene1, string gene2)
    {
        int count = 0;
        for (int i = 0; i < gene1.Length; i++)
        {
            if (gene1[i] != gene2[i])
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

    public int MinMutation(string startGene, string endGene, string[] bank)
    {
        string[] genes = new string[bank.Length + 1];
        genes[0] = startGene;
        for (int i = 0; i < bank.Length; i++)
        {
            genes[i + 1] = bank[i];
        }
        int[,] graph = new int[genes.Length, genes.Length];
        int startIndex = -1;
        int endIndex = -1;
        for (int i = 0; i < genes.Length; i++)
        {
            if (startGene == genes[i])
            {
                startIndex = i;
            }
            if (endGene == genes[i])
            {
                endIndex = i;
            }

            for (int j = 0; j < genes.Length; j++)
            {
                if (i != j && IsMutationPossible(genes[i], genes[j]))
                {
                    graph[i, j] = 1;
                }
            }
        }

        if (startIndex == -1 || endIndex == -1)
        {
            return -1;
        }

        int count = 0;
        var visited = new bool[genes.Length];
        var queue = new Queue<int>();

        queue.Enqueue(startIndex);
        while (queue.Count > 0)
        {
            int size = queue.Count;
            for (int i = 0; i < size; i++)
            {
                int current = queue.Dequeue();
                visited[current] = true;

                if (current == endIndex)
                {
                    return count;
                }

                for (int j = 0; j < genes.Length; j++)
                {
                    if (graph[current, j] == 1 && !visited[j])
                    {
                        queue.Enqueue(j);
                    }
                }
            }
            count++;
        }

        return -1;
    }
}