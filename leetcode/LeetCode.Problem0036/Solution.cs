// https://leetcode.com/problems/valid-sudoku/

public class Solution
{
    public bool UpdateSet(int i, int j, char[][] board, HashSet<char> set)
    {
        var c = board[i][j];
        if (c == '.')
        {
            return true;
        }

        return set.Add(c);
    }

    public bool IsColumnValid(int j, char[][] board)
    {
        var set = new HashSet<char>();
        for (int i = 0; i < board[0].Length; i++)
        {
            if (!UpdateSet(i, j, board, set))
            {
                return false;
            }
        }
        return true;
    }

    public bool IsRowValid(int i, char[][] board)
    {
        var set = new HashSet<char>();
        for (int j = 0; j < board.Length; j++)
        {
            if (!UpdateSet(i, j, board, set)) 
            {
                return false;
            }
        }
        return true;
    }

    public bool IsSquareValid(int i, int j, char[][] board)
    {
        var set = new HashSet<char>();
        for (int k = i; k < i + 3; k++)
        for (int l = j; l < j + 3; l++)
        {
            if (!UpdateSet(k, l, board, set))
            {
                return false;
            }
        }
        return true;
    }


    public bool IsValidSudoku(char[][] board)
    {
        for (int i = 0; i < board.Length; i++)
        {
            if (!IsRowValid(i, board))
            {
                return false;
            }
        }

        for (int j = 0; j < board.Length; j++)
        {
            if (!IsColumnValid(j, board))
            {
                return false;
            }
        }

        for (int i = 0; i < board.Length / 3; i++)
        for (int j = 0; j < board[0].Length / 3; j++)
        {
            if (!IsSquareValid(3 * i, 3 * j, board))
            {
                return false;
            }
        }

        return true;
    }
}