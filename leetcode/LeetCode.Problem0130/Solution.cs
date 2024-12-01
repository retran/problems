public class Solution
{
    private void TryCapture(char[][] board, int i, int j)
    {
        if (i < 0 || j < 0 || i >= board.Length || j >= board[0].Length || board[i][j] != 'O')
        {
            return;
        }

        List<(int, int)> captured = new List<(int, int)>();

        var queue = new Queue<(int, int)>();
        queue.Enqueue((i, j));
        while (queue.Count > 0)
        {
            var (x, y) = queue.Dequeue();
            if (x < 0 || y < 0 || x >= board.Length || y >= board[0].Length || board[x][y] != 'O')
            {
                continue;
            }

            board[x][y] = '1';
            captured.Add((x, y));
            queue.Enqueue((x - 1, y));
            queue.Enqueue((x + 1, y));
            queue.Enqueue((x, y - 1));
            queue.Enqueue((x, y + 1));
        }

        foreach (var (x, y) in captured)
        {
            if (x == 0 || x == board.Length - 1 || y == 0 || y == board[0].Length - 1)
            {
                return;
            }
        }

        foreach (var (x, y) in captured)
        {
            board[x][y] = 'X';
        }
    }

    public void Solve(char[][] board)
    {
        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[0].Length; j++)
            {
                if (board[i][j] == 'O')
                {
                    TryCapture(board, i, j);
                }
            }
        }

        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[0].Length; j++)
            {
                if (board[i][j] == '1')
                {
                    board[i][j] = 'O';
                }
            }
        }
    }
}