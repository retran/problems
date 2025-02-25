package problem0084

class Solution {
    fun largestRectangleArea(heights: IntArray): Int {
        val stack = mutableListOf<Int>()
        var maxArea = 0
        val n = heights.size

        for (i in 0..n) {
            while (stack.isNotEmpty() && (if (i == n) 0 else heights[i]) <= heights[stack.last()]) {
                val height = heights[stack.removeAt(stack.lastIndex)]
                val width = if (stack.isEmpty()) i else i - stack.last() - 1
                maxArea = maxOf(maxArea, height * width)
            }
            stack.add(i)
        }
        return maxArea
    }
}

fun test() {
    val solution = Solution()
    val testCases = listOf(
        intArrayOf(2, 1, 5, 6, 2, 3) to 10,  // Largest rectangle area is 10.
        intArrayOf(2, 4) to 4,               // Largest rectangle area is 4.
        intArrayOf(2, 2, 2, 2) to 8,         // Largest rectangle area is 8.
        intArrayOf(1) to 1,                 // Largest rectangle area is 1.
        intArrayOf() to 0                   // Empty array returns 0.
    )

    for ((input, expected) in testCases) {
        val result = solution.largestRectangleArea(input)
        println("Input: ${input.joinToString(", ")} | Expected: $expected | Got: $result")
    }
}
