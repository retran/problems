public class Solution
{
    public string Tictactoe(int[][] moves)
    {
        int[,] board = new int[3, 3];

        bool isCrossTurn = true;
        foreach (var turn in moves)
        {
            board[turn[0], turn[1]] = isCrossTurn ? 1 : 2;
            isCrossTurn = !isCrossTurn;
        }

        int lastTurn = isCrossTurn ? 2 : 1;

        int freeCells = 9;

        int rows = 0;
        int columns = 0;
        for (int i = 0; i < 3; i++)
        {
            rows = 0;
            columns = 0;
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == lastTurn)
                {
                    rows++;
                }

                if (board[j, i] == lastTurn)
                {
                    columns++;
                }

                if (board[i, j] != 0)
                {
                    freeCells--;
                }
            }

            if (rows == 3 || columns == 3)
            {
                return lastTurn == 1 ? "A" : "B";
            }
        }

        int left = 0;
        int right = 0;
        for (int i = 0; i  < 3; i++)
        {
            if (board[i, i] == lastTurn)
            {
                left++;
            }

            if (board[i, 2 - i] == lastTurn)
            {
                right++;
            }
        }

        if (left == 3 || right == 3)
        {
            return lastTurn == 1 ? "A" : "B";
        }
        
        return freeCells > 0 
            ? "Pending" 
            : "Draw";
    }
}