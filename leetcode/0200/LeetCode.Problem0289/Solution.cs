// https://leetcode.com/problems/game-of-life

public class Solution
{
    private int CountNeighbours(int[][] board, int i, int j)
    {
        int count = 0;
        for (int x = i - 1; x <= i + 1; x++)
        {
            for (int y = j - 1; y <= j + 1; y++)
            {
                if (x >= 0 && x < board.Length && y >= 0 && y < board[0].Length && (x != i || y != j))
                {
                    if (board[x][y] == 1 || board[x][y] == 2)
                    {
                        count++;
                    }
                }
            }
        }
        return count;
    }

    public void GameOfLife(int[][] board)
    {
        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[0].Length; j++)
            {
                int count = CountNeighbours(board, i, j);
                if (board[i][j] == 1)
                {
                    if (count < 2 || count > 3)
                    {
                        board[i][j] = 2;
                    }
                }
                else
                {
                    if (count == 3)
                    {
                        board[i][j] = 3;
                    }
                }
            }
        }

        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[0].Length; j++)
            {
                if (board[i][j] == 2)
                {
                    board[i][j] = 0;
                }
                else if (board[i][j] == 3)
                {
                    board[i][j] = 1;
                }
            }
        }
    }
}