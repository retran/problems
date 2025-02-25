package problem0959

class Solution {
    private class DisjointSetUnion(capacity: Int) {
        val parents = IntArray(capacity) { it }
        val ranks = IntArray(capacity) { 1 }
        var count = capacity

        fun find(key: Int): Int {
            if (key != parents[key]) {
                parents[key] = find(parents[key])
            }
            return parents[key]
        }

        fun union(first: Int, second: Int) {
            val firstParent = find(first)
            val secondParent = find(second)

            if (firstParent == secondParent) {
                return
            }

            if (ranks[firstParent] < ranks[secondParent]) {
                parents[firstParent] = secondParent
            } else if (ranks[firstParent] > ranks[secondParent]) {
                parents[secondParent] = firstParent
            } else {
                parents[secondParent] = firstParent
                ranks[firstParent]++
            }

            count--
        }
    }

    enum class Part(val key: Int) {
        Left(0),
        Top(1),
        Right(2),
        Bottom(3)
    }

    fun regionsBySlashes(grid: Array<String>): Int {
        val rows = grid.size
        val columns = grid[0].length

        fun getKey(row: Int, column: Int, part: Part): Int {
            return 4 * row * columns + 4 * column + part.key
        }

        val dsu = DisjointSetUnion(rows * columns * 4)

        for (row in 0..rows - 1) {
            for (column in 0..columns - 1) {
                val left = getKey(row, column, Part.Left)
                val top = getKey(row, column, Part.Top)
                val right = getKey(row, column, Part.Right)
                val bottom = getKey(row, column, Part.Bottom)
                when (grid[row][column]) {
                    ' ' -> {
                        dsu.union(left, top)
                        dsu.union(top, right)
                        dsu.union(right, bottom)
                    }
                    '/' -> {
                        dsu.union(top, left)
                        dsu.union(right, bottom)
                    }
                    '\\' -> {
                        dsu.union(top, right)
                        dsu.union(left, bottom)
                    }
                }

                if (column < columns - 1) {
                    dsu.union(right, getKey(row, column + 1, Part.Left))
                }

                if (row < rows - 1) {
                    dsu.union(bottom, getKey(row + 1, column, Part.Top))
                }
            }
        }

        return dsu.count
    }
}