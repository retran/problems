package problem3027

class Solution {
    data class Point(val X: Int, val Y: Int)

    fun numberOfPairs(points: Array<IntArray>): Int {
        val orderedPoints = points.map { Point(it[0], it[1]) }
            .sortedWith { a, b ->
                when {
                    a.X == b.X -> b.Y - a.Y
                    else -> a.X - b.X
                }
            }

        var count = 0
        for (i in 0..orderedPoints.size - 2) {
            var maxY: Int = Int.MIN_VALUE
            for (j in i + 1..orderedPoints.size - 1) {

                if (orderedPoints[i].X <= orderedPoints[j].X && orderedPoints[i].Y >= orderedPoints[j].Y) {
                    if (orderedPoints[j].Y > maxY) {
                        count++
                        maxY = orderedPoints[j].Y
                    }
                }
            }
        }

        return count
    }
}