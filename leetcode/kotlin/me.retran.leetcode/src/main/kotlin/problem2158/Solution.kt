package problem2158

class Solution {
    fun amountPainted(paint: Array<IntArray>): IntArray {
        val n = paint.size
        val result = IntArray(n)
        val maxIndex = 50000 + 1
        val uf = IntArray(maxIndex) { it }

        fun find(x: Int): Int {
            if (uf[x] != x) {
                uf[x] = find(uf[x])
            }
            return uf[x]
        }

        for (i in 0 until n) {
            val start = paint[i][0]
            val end = paint[i][1]
            var j = find(start)
            while (j < end) {
                result[i]++
                uf[j] = find(j + 1)
                j = find(j)
            }
        }

        return result
    }
}
