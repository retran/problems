package problem0200

class Solution {
    class DisjointSetUnion(val capacity: Int) {
        val parents: IntArray
        val ranks: IntArray
        var count: Int = capacity

        init {
            parents = IntArray(capacity)
            ranks = IntArray(capacity) { 1 }

            for (i in 0..<capacity) {
                parents[i] = i
            }
        }

        fun find(key: Int): Int {
            var parent = parents[key]
            while (parent != parents[parent]) {
                parent = find(parent)
                parents[key] = parent
                return parent
            }

            return parent
        }

        fun union(first: Int, second: Int) {
            val firstRoot = find(first)
            val secondRoot = find(second)

            if (firstRoot == secondRoot) {
                return
            }

            if (ranks[firstRoot] < ranks[secondRoot]) {
                parents[firstRoot] = secondRoot
            } else if (ranks[firstRoot] > ranks[secondRoot]) {
                parents[secondRoot] = firstRoot
            } else {
                parents[secondRoot] = firstRoot
                ranks[firstRoot]++
            }

            count--
        }
    }

    fun numIslands(grid: Array<CharArray>): Int {
        val rows = grid.size
        val columns = grid[0].size

        fun getKey(row: Int, column: Int): Int =
            row * columns + column + 1

        val waterKey = 0;

        val dsu = DisjointSetUnion(rows * columns + 1)

        for (row in 0 until rows) {
            for (column in 0 until columns) {
                val key = getKey(row, column)
                when (grid[row][column]) {
                    '0' -> dsu.union(waterKey, key)
                    '1' -> {
                        if (row < rows - 1 && grid[row + 1][column] == '1') {
                            dsu.union(getKey(row + 1, column), key)
                        }

                        if (column < columns - 1 && grid[row][column + 1] == '1') {
                            dsu.union(getKey(row, column + 1), key)
                        }
                    }
                }
            }
        }

        return dsu.count - 1;
    }
}

// TODO tests