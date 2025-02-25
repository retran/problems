package problem0329

class Solution {
    data class Position(val row: Int, val column: Int)

    fun longestIncreasingPath(matrix: Array<IntArray>): Int {
        val rows = matrix.size
        var columns = matrix[0].size

        val directions = listOf(
            Position(-1, 0),
            Position(1, 0),
            Position(0, 1),
            Position(0, -1),
        )

        val queue = ArrayDeque<Position>()
        val lengths = Array(matrix.size) { IntArray(matrix[0].size) { 0 } }

        for (row in 0..rows - 1) {
            for (column in 0..columns - 1) {
                var isPeak = true
                for (direction in directions) {
                    val adjacentPosition = Position(
                        row + direction.row,
                        column + direction.column
                    )

                    if (adjacentPosition.row in 0..rows - 1 && adjacentPosition.column in 0..columns - 1) {
                        if (matrix[adjacentPosition.row][adjacentPosition.column] > matrix[row][column]) {
                            isPeak = false
                            break
                        }
                    }
                }

                if (isPeak) {
                    queue.addLast(Position(row, column))
                    lengths[row][column] = 1
                }
            }
        }

        var maxLength = 0
        while (queue.isNotEmpty()) {
            val current = queue.removeFirst()
            maxLength = maxOf(maxLength, lengths[current.row][current.column])
            val nextLength = lengths[current.row][current.column] + 1
            for (direction in directions) {
                val adjacentPosition = Position(
                    current.row + direction.row,
                    current.column + direction.column
                )

                if (adjacentPosition.row in 0..rows - 1
                    && adjacentPosition.column in 0..columns - 1
                    && matrix[current.row][current.column] > matrix[adjacentPosition.row][adjacentPosition.column]
                    && nextLength > lengths[adjacentPosition.row][adjacentPosition.column]
                ) {
                    lengths[adjacentPosition.row][adjacentPosition.column] = nextLength
                    queue.addLast(adjacentPosition)
                }
            }
        }

        return maxLength
    }
}