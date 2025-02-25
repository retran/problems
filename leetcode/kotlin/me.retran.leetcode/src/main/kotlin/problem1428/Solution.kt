package problem1428

abstract class BinaryMatrix {
    abstract fun get(row: Int, col: Int): Int
    abstract fun dimensions(): List<Int>
}

class Solution {
    fun leftMostColumnWithOne(binaryMatrix: BinaryMatrix): Int {
        val dimensions = binaryMatrix.dimensions()
        val rows = dimensions[0]
        val columns = dimensions[1]

        var row = 0
        var column = columns - 1

        while (row < rows && column >= 0) {
            if (binaryMatrix.get(row, column) == 1) {
                column--
            } else {
                row++
            }
        }

        return if (column == columns - 1) -1 else column + 1
    }
}