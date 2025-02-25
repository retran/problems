package problem0079

class Solution {
    data class Position(val row: Int, val column: Int)
    data class State(val position: Position, val index: Int, val visited: Set<Position>)

    fun exist(board: Array<CharArray>, word: String): Boolean
    {
        val rows = board.size
        val columns = board[0].size

        val directions = arrayOf(
            Position(-1, 0),
            Position(1, 0),
            Position(0, -1),
            Position(0, 1)
        )

        val stack: ArrayDeque<State> = ArrayDeque()

        for (i in 0..rows - 1) {
            for (j in 0..columns - 1) {
                if (board[i][j] == word[0]) {
                    val position = Position(i, j)
                    stack.addLast(State(position, 0, setOf(position)))
                }
            }
        }

        while (stack.isNotEmpty()) {
            val currentState = stack.removeLast()

            if (currentState.index == word.length - 1) {
                return true
            }

            for (direction in directions) {
                val nextPosition = Position(
                    currentState.position.row + direction.row,
                    currentState.position.column + direction.column)

                if (nextPosition.row !in 0..rows - 1 || nextPosition.column !in 0..columns - 1) {
                    continue
                }

                if (currentState.visited.contains(nextPosition)) {
                    continue
                }

                if (board[nextPosition.row][nextPosition.column] == word[currentState.index + 1]) {
                    val newState = State(
                        nextPosition,
                        currentState.index + 1,
                        currentState.visited.plus(nextPosition))
                    stack.addLast(newState)
                }
            }
        }
        return false
    }
}