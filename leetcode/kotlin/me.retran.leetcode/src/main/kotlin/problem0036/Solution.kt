package problem0036

class Solution {
    private fun validateRow(board: Array<CharArray>, row: Int): Boolean {
        val seen = mutableSetOf<Char>()
        for (column in 0..board[0].size - 1) {
            if (board[row][column] == '.') {
                continue
            }

            if (!seen.add(board[row][column])) {
                return false
            }
        }
        return true
    }

    private fun validateColumn(board: Array<CharArray>, column: Int): Boolean {
        val seen = mutableSetOf<Char>()
        for (row in 0..board.size - 1) {
            if (board[row][column] == '.') {
                continue
            }

            if (!seen.add(board[row][column])) {
                return false
            }
        }
        return true
    }

    private fun validateSquare(board: Array<CharArray>, startRow: Int, startColumn: Int): Boolean {
        val seen = mutableSetOf<Char>()
        for (row in startRow..startRow + 2) {
            for (column in startColumn..startColumn + 2) {
                if (board[row][column] == '.') {
                    continue
                }

                if (!seen.add(board[row][column])) {
                    return false
                }
            }
        }
        return true
    }

    fun isValidSudoku(board: Array<CharArray>): Boolean {
        for (i in 0..board.size - 1) {
            if (!validateRow(board, i)) {
                return false
            }

            if (!validateColumn(board, i)) {
                return false
            }
        }

        for (i in 0..2) {
            for (j in 0..2) {
                if (!validateSquare(board, i * 3, j * 3)) {
                    return false
                }
            }
        }

        return true
    }
}