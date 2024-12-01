public class Solution
{
    public void WallsAndGates(int[][] rooms)
    {
        int n = rooms.Length;
        int m = rooms[0].Length;

        var queue = new Queue<(int i, int j)>();

        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
            {
                if (rooms[i][j] == 0)
                {
                    queue.Enqueue((i, j));
                }
            }

        var directions = new (int i, int j)[] { (-1, 0), (1, 0), (0, -1), (0, 1) };

        int currentStep = 0;
        while (queue.Count > 0)
        {
            currentStep++;
            var size = queue.Count;
            for (int i = 0; i < size; i++)
            {
                var current = queue.Dequeue();

                foreach (var direction in directions)
                {
                    (int i, int j) next = (current.i + direction.i, current.j + direction.j);
                    if (next.i < 0 || next.i > n - 1 || next.j < 0 || next.j > m - 1)
                    {
                        continue;
                    }

                    if (rooms[next.i][next.j] <= currentStep)
                    {
                        continue;
                    }

                    rooms[next.i][next.j] = currentStep;
                    queue.Enqueue(next);
                }
            }
        }
    }
}