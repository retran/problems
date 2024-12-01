public class Solution {
    public void SolveSudoku(char[][] board) {
        Solve(board);
    }

    private bool Solve(char[][] board) {
        for (int row = 0; row < 9; row++) {
            for (int col = 0; col < 9; col++) {
                // Find an empty cell
                if (board[row][col] == '.') {
                    // Try each number from 1 to 9
                    for (char num = '1'; num <= '9'; num++) {
                        if (IsValid(board, row, col, num)) {
                            board[row][col] = num; // Place the number

                            // Recur to solve the rest of the board
                            if (Solve(board)) {
                                return true; // If solved, return true
                            }

                            // Backtrack: reset the cell
                            board[row][col] = '.';
                        }
                    }
                    // If no number can be placed, return false
                    return false;
                }
            }
        }
        // If no empty cells are found, the board is solved
        return true;
    }

    private bool IsValid(char[][] board, int row, int col, char num) {
        // Check the row
        for (int j = 0; j < 9; j++) {
            if (board[row][j] == num) {
                return false;
            }
        }
        
        // Check the column
        for (int i = 0; i < 9; i++) {
            if (board[i][col] == num) {
                return false;
            }
        }
        
        // Check the 3x3 subgrid
        int startRow = row / 3 * 3;
        int startCol = col / 3 * 3;
        for (int i = startRow; i < startRow + 3; i++) {
            for (int j = startCol; j < startCol + 3; j++) {
                if (board[i][j] == num) {
                    return false;
                }
            }
        }
        
        // If no conflict found, it's valid
        return true;
    }
}
