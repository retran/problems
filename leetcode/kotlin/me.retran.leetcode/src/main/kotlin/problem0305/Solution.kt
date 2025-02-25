package problem0305

class Solution {
    class DisjointSetUnion(capacity: Int) {
        val parents = IntArray(capacity)
        val ranks = IntArray(capacity) { 1 }
        var count = capacity

        init {
            for (i in 0 until capacity) {
                parents[i] = i
            }
        }

        fun find(value: Int): Int {
            if (parents[value] == value) {
                return value
            }

            parents[value] = find(parents[value])
            return parents[value]
        }

        fun union(a: Int, b: Int): Boolean {
            val parentA = find(a)
            val parentB = find(b)

            if (parentA == parentB) {
                return false
            }

            if (ranks[parentA] < ranks[parentB]) {
                parents[parentA] = parentB
            } else if (ranks[parentA] > ranks[parentB]) {
                parents[parentB] = parentA
            } else {
                parents[parentB] = parentA
                ranks[parentA]++
            }

            count--
            return true
        }
    }

    fun numIslands2(m: Int, n: Int, positions: Array<IntArray>): List<Int> {
        val directions = listOf(-1 to 0, 1 to 0, 0 to -1, 0 to 1)
        val set = DisjointSetUnion(m * n)
        val map = Array(m) { IntArray(n) { 0 } }
        var count = 0

        fun getKey(row: Int, column: Int): Int = row * n + column

        fun put(position: IntArray): Int {
            val row = position[0]
            val column = position[1]

            if (row !in 0..m - 1
                || column !in 0..n - 1
                || map[row][column] != 0
            )
                return count

            map[row][column] = 1

            var merges = 0
            for (direction in directions) {
                val adjRow = row + direction.first
                val adjColumn = column + direction.second
                if (adjRow in 0..m - 1
                    && adjColumn in 0..n - 1
                    && map[adjRow][adjColumn] > 0
                ) {
                    if (set.union(getKey(row, column), getKey(adjRow, adjColumn))) {
                        merges++
                    }
                }
            }

            if (merges > 0) {
                count = count - merges + 1
                return count
            }

            count++
            return count
        }

        return positions.map { put(it) }.toList()
    }
}