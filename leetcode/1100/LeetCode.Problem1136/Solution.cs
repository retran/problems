public class Solution
{
    public int MinimumSemesters(int n, int[][] relations)
    {
        bool[,] graph = new bool[n + 1, n + 1];
        int[] inDegree = new int[n + 1];

        for (int i = 0; i < relations.Length; i++)
        {
            graph[relations[i][0], relations[i][1]] = true;
            inDegree[relations[i][1]]++;
        }

        Queue<int> queue = new Queue<int>();

        for (int i = 1; i <= n; i++)
        {
            if (inDegree[i] == 0)
            {
                queue.Enqueue(i);
            }
        }

        int semesters = 0;

        while (queue.Count > 0)
        {
            int size = queue.Count;
            semesters++;

            for (int i = 0; i < size; i++)
            {
                int course = queue.Dequeue();

                for (int j = 1; j <= n; j++)
                {
                    if (graph[course, j])
                    {
                        inDegree[j]--;

                        if (inDegree[j] == 0)
                        {
                            queue.Enqueue(j);
                        }
                    }
                }
            }
        }

        for (int i = 1; i <= n; i++)
        {
            if (inDegree[i] > 0)
            {
                return -1;
            }
        }

        return semesters;
    }
}