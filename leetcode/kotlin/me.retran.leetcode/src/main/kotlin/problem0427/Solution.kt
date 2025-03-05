package problem0427

class Node(var `val`: Boolean, var isLeaf: Boolean) {
    var topLeft: Node? = null
    var topRight: Node? = null
    var bottomLeft: Node? = null
    var bottomRight: Node? = null
}

class Solution {
    fun constructImpl(grid:Array<IntArray>, rowStart: Int, rowEnd: Int, columnStart: Int, columnEnd:Int): Node {
        if (rowStart == rowEnd && columnStart == columnEnd) {
            return Node(grid[rowStart][columnStart] == 1, true)
        }

        val rowMid = rowStart + (rowEnd - rowStart) / 2
        val columnMid = columnStart + (columnEnd - columnStart) / 2

        val topLeft = constructImpl(grid, rowStart, rowMid, columnStart, columnMid)
        val topRight = constructImpl(grid, rowStart, rowMid, columnMid + 1, columnEnd)
        val bottomLeft = constructImpl(grid, rowMid + 1, rowEnd, columnStart, columnMid)
        val bottomRight = constructImpl(grid, rowMid + 1, rowEnd, columnMid + 1, columnEnd)

        if (topLeft.isLeaf
            && topRight.isLeaf
            && bottomLeft.isLeaf
            && bottomRight.isLeaf
            && topLeft.`val` == topRight.`val`
            && bottomLeft.`val` == bottomRight.`val`
            && topLeft.`val` == bottomLeft.`val`) {
            return Node(topLeft.`val`, true)
        }

        val node = Node(topRight.`val`, false)
        node.topLeft = topLeft
        node.topRight = topRight
        node.bottomLeft = bottomLeft
        node.bottomRight = bottomRight

        return node
    }

    fun construct(grid: Array<IntArray>): Node {
        val rows = grid.size
        val columns = grid[0].size

        return constructImpl(grid, 0, rows - 1, 0, columns - 1)
    }
}