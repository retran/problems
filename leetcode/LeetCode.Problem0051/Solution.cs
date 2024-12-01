public class Solution {
    public IList<IList<string>> SolveNQueens(int n) {
        var results = new List<IList<string>>();
        var board = new char[n][];
        for (int i = 0; i < n; i++) {
            board[i] = new string('.', n).ToCharArray();
        }
        Solve(results, board, 0, n);
        return results;
    }

    private void Solve(List<IList<string>> results, char[][] board, int row, int n) {
        if (row == n) {
            results.Add(Construct(board));
            return;
        }

        for (int col = 0; col < n; col++) {
            if (IsSafe(board, row, col, n)) {
                board[row][col] = 'Q';
                Solve(results, board, row + 1, n);
                board[row][col] = '.';
            }
        }
    }

    private bool IsSafe(char[][] board, int row, int col, int n) {
        // Check column
        for (int i = 0; i < row; i++) {
            if (board[i][col] == 'Q') {
                return false;
            }
        }

        // Check upper-left diagonal
        for (int i = row, j = col; i >= 0 && j >= 0; i--, j--) {
            if (board[i][j] == 'Q') {
                return false;
            }
        }

        // Check upper-right diagonal
        for (int i = row, j = col; i >= 0 && j < n; i--, j++) {
            if (board[i][j] == 'Q') {
                return false;
            }
        }

        return true;
    }

    private IList<string> Construct(char[][] board) {
        var result = new List<string>();
        for (int i = 0; i < board.Length; i++) {
            result.Add(new string(board[i]));
        }
        return result;
    }
}
