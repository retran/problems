public class Solution
{
    private (int, int) ToRowColumn(int n, int label) // 6, 6
    {
        int row = (label - 1) / n; // 0
        row = n - 1 - row; // 5

        int column = (label - 1) % n; // 5
        
        if (n % 2 == 0 && (row % 2 == 0 || row == 0))
        {
            column = n - 1 - column;
        }

        if (n % 2 == 1 && (row % 2 == 1 || row == 1))
        {
            column = n - 1 - column;
        }

        return (row, column);
    }

    public int SnakesAndLadders(int[][] board)
    {
        int n = board.Length;
        var visited = new int[n * n];
        var queue = new Queue<int>();
        queue.Enqueue(1);
        visited[0] = 0;
        int steps = 0;

        while (queue.Count > 0)
        {
            int count = queue.Count;
            for (int i = 0; i < count; i++)
            {
                int label = queue.Dequeue();

                if (label == n * n) 
                {
                    return steps;
                }

                for (int next = label + 1; next <= Math.Min(n * n, label + 6); next++)
                {
                    var (row, column) = ToRowColumn(n, next);

                    int value = board[row][column];
                    if (value == -1)
                    {
                        value = next;
                    }

                    if (visited[value - 1] == 0 || visited[value - 1] > steps + 1) 
                    {
                        visited[value - 1] = steps + 1;
                        queue.Enqueue(value);
                    }
                }
            }
            steps++;
        }

        return -1;
    }
}
