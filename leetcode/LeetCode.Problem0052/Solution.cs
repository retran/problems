public class Solution
{
    public int TotalNQueens(int n)
    {
        var board = new bool[n, n];

        return Backtrack(board, 0);
    }

    private int Backtrack(bool[,] board, int row)
    {
        int n = board.GetLength(0);
        if (row == n)
        {
            return 1;
        }

        int count = 0;
        for (int col = 0; col < n; col++)
        {
            if (IsSafe(board, row, col))
            {
                board[row, col] = true;
                count += Backtrack(board, row + 1);
                board[row, col] = false;
            }
        }

        return count;
    }

    private bool IsSafe(bool[,] board, int row, int col)
    {
        int n = board.GetLength(0);

        for (int i = 0; i < row; i++)
        {
            if (board[i, col])
            {
                return false;
            }
        }

        for (int i = row, j = col; i >= 0 && j >= 0; i--, j--)
        {
            if (board[i, j])
            {
                return false;
            }
        }

        for (int i = row, j = col; i >= 0 && j < n; i--, j++)
        {
            if (board[i, j])
            {
                return false;
            }
        }

        return true;
    }
}
