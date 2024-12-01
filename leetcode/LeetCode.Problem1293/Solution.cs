public class Solution
{
    public int ShortestPath(int[][] grid, int k)
    {
        int n = grid.Length;
        int m = grid[0].Length;

        var queue = new Queue<(int i, int j, int k)>();
        var directions = new (int i, int j)[] { (-1, 0), (1, 0), (0, -1), (0, 1) };
        var visited = new HashSet<(int i, int j, int k)>();

        queue.Enqueue((0, 0, k));
        visited.Add((0, 0, k));

        int currentStep = 0;
        while (queue.Count > 0)
        {
            var size = queue.Count;
            for (int i = 0; i < size; i++)
            {
                var current = queue.Dequeue();

                if (current.i == n - 1 && current.j == m - 1)
                {
                    return currentStep;
                }

                foreach (var direction in directions)
                {
                    (int i, int j, int k) next = (current.i + direction.i, current.j + direction.j, current.k);
                    if (next.i < 0 || next.i > n - 1 || next.j < 0 || next.j > m - 1)
                    {
                        continue;
                    }

                    if (grid[next.i][next.j] == 1)
                    {
                        if (next.k < 1)
                        {
                            continue;
                        }

                        next.k--;
                    }

                    if (visited.Contains(next))
                    {
                        continue;
                    }

                    visited.Add(next);
                    queue.Enqueue(next);
                }
            }

            currentStep++;
        }

        return -1;
    }
}