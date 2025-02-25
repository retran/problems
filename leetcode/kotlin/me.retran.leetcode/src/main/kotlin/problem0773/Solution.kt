package problem0773

class Solution {
    data class Position(var row: Int, var column: Int)

    data class State(val board: Array<IntArray>, val emptyPosition: Position, val turns: Int)

    fun swap(board: Array<IntArray>, from: Position, to: Position): Array<IntArray> {

        val newBoard = Array(board.size) { IntArray(board[0].size) { 0 } }

        for (row in board.indices) {
            for (column in board[0].indices) {
                newBoard[row][column] = board[row][column]
            }
        }

        val temp = newBoard[from.row][from.column]
        newBoard[from.row][from.column] = newBoard[to.row][to.column]
        newBoard[to.row][to.column] = temp

        return newBoard
    }

    fun isSolved(board: Array<IntArray>): Boolean {
        return board[0][0] == 1
            && board[0][1] == 2
            && board[0][2] == 3
            && board[1][0] == 4
            && board[1][1] == 5
            && board[1][2] == 0
    }

    fun getKey(board: Array<IntArray>): String {
        return board.joinToString { it.joinToString { it.toString() } }
    }

    fun slidingPuzzle(board: Array<IntArray>): Int {
        val directions = arrayOf(
            Position(-1, 0),
            Position(1, 0),
            Position(0, 1),
            Position(0, -1)
        )

        val rows = board.size
        var columns = board[0].size

        var emptyPosition: Position? = null
        for (row in 0 until rows) {
            for (column in 0 until columns) {
                if (board[row][column] == 0) {
                    emptyPosition = Position(row, column)
                    break
                }
            }

            if (emptyPosition != null) {
                break
            }
        }

        val queue = ArrayDeque<State>()
        queue.add(State(board, emptyPosition!!, 0))

        val visited = mutableSetOf<String>()

        while (queue.isNotEmpty()) {
            val state = queue.removeFirst()
            if (isSolved(state.board)) {
                return state.turns
            }

            val key = getKey(state.board)
            if (visited.contains(key)) {
                continue
            }

            visited.add(key)

            for (direction in directions) {
                val adjacentPosition = Position(
                    state.emptyPosition.row + direction.row,
                    state.emptyPosition.column + direction.column)

                if (adjacentPosition.row !in 0..rows - 1 || adjacentPosition.column !in 0..columns - 1) {
                    continue
                }

                val newBoard = swap(state.board, state.emptyPosition, adjacentPosition)
                queue.addLast(State(newBoard, adjacentPosition, state.turns + 1))
            }
        }

        return -1
    }
}