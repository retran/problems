package problem0218

class Solution {
    fun getSkyline(buildings: Array<IntArray>): List<List<Int>> {
        if (buildings.isEmpty()) return emptyList()
        return divideAndConquer(buildings, 0, buildings.size - 1)
    }

    private fun divideAndConquer(buildings: Array<IntArray>, left: Int, right: Int): List<List<Int>> {
        if (left == right) {
            // For a single building [L, R, H], the skyline is:
            // [L, H] and [R, 0]
            return listOf(
                listOf(buildings[left][0], buildings[left][2]),
                listOf(buildings[left][1], 0)
            )
        }

        val mid = (right - left) / 2 + left
        val leftSkyline = divideAndConquer(buildings, left, mid)
        val rightSkyline = divideAndConquer(buildings, mid + 1, right)
        return mergeSkylines(leftSkyline, rightSkyline)
    }

    private fun mergeSkylines(
        leftSkyline: List<List<Int>>,
        rightSkyline: List<List<Int>>
    ): List<List<Int>> {
        var leftPos = 0
        var rightPos = 0
        var leftPrevHeight = 0
        var rightPrevHeight = 0
        val answer = mutableListOf<List<Int>>()

        while (leftPos < leftSkyline.size && rightPos < rightSkyline.size) {
            val nextLeftX = leftSkyline[leftPos][0]
            val nextRightX = rightSkyline[rightPos][0]
            val curX: Int
            val curY: Int

            when {
                nextLeftX < nextRightX -> {
                    leftPrevHeight = leftSkyline[leftPos][1]
                    curX = nextLeftX
                    curY = maxOf(leftPrevHeight, rightPrevHeight)
                    leftPos++
                }
                nextLeftX > nextRightX -> {
                    rightPrevHeight = rightSkyline[rightPos][1]
                    curX = nextRightX
                    curY = maxOf(leftPrevHeight, rightPrevHeight)
                    rightPos++
                }
                else -> { // both x are equal
                    leftPrevHeight = leftSkyline[leftPos][1]
                    rightPrevHeight = rightSkyline[rightPos][1]
                    curX = nextLeftX
                    curY = maxOf(leftPrevHeight, rightPrevHeight)
                    leftPos++
                    rightPos++
                }
            }

            // Only add a new key point if the height changes.
            if (answer.isEmpty() || answer.last()[1] != curY) {
                answer.add(listOf(curX, curY))
            }
        }

        // Append any remaining points from the left skyline.
        while (leftPos < leftSkyline.size) {
            answer.add(leftSkyline[leftPos])
            leftPos++
        }

        // Append any remaining points from the right skyline.
        while (rightPos < rightSkyline.size) {
            answer.add(rightSkyline[rightPos])
            rightPos++
        }

        return answer
    }
}

// Test cases
fun test() {
    val sol = Solution()
    val testCases = listOf(
        // Test 1: Provided example
        arrayOf(
            intArrayOf(2, 9, 10),
            intArrayOf(3, 7, 15),
            intArrayOf(5, 12, 12),
            intArrayOf(15, 20, 10),
            intArrayOf(19, 24, 8)
        ) to listOf(
            listOf(2, 10),
            listOf(3, 15),
            listOf(7, 12),
            listOf(12, 0),
            listOf(15, 10),
            listOf(20, 8),
            listOf(24, 0)
        ),
        // Test 2: Single building.
        arrayOf(
            intArrayOf(1, 3, 3)
        ) to listOf(
            listOf(1, 3),
            listOf(3, 0)
        ),
        // Test 3: No buildings.
        arrayOf<IntArray>() to emptyList<List<Int>>()
    )

    for ((buildings, expected) in testCases) {
        val result = sol.getSkyline(buildings)
        println("Input: ${buildings.joinToString(separator = "; ") { it.joinToString(", ") }}")
        println("Output: $result")
        println("Expected: $expected")
        println("-----")
    }
}

fun main() {
    test()
}
