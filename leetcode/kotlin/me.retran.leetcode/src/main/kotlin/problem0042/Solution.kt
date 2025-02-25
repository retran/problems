package problem0042

class Solution {
    fun trap(height: IntArray): Int {
        if (height.isEmpty()) return 0

        val left = IntArray(height.size)
        val right = IntArray(height.size)

        var currentLevel = 0
        for (i in height.indices) {
            currentLevel = maxOf(currentLevel, height[i])
            left[i] = currentLevel
        }

        currentLevel = 0
        for (i in height.indices.reversed()) {
            currentLevel = maxOf(currentLevel, height[i])
            right[i] = currentLevel
        }

        var sum = 0
        for (i in height.indices) {
            sum += maxOf(0, minOf(left[i], right[i]) - height[i])
        }

        return sum
    }
}

fun test() {
    val solution = Solution()

    // Test Case 1: Typical case
    println("Test Case 1: " + (
            solution.trap(intArrayOf(0,1,0,2,1,0,1,3,2,1,2,1)) == 6
            ))

    // Test Case 2: No water trapped
    println("Test Case 2: " + (
            solution.trap(intArrayOf(0,0,0,0)) == 0
            ))

    // Test Case 3: Complex case with varying heights
    println("Test Case 3: " + (
            solution.trap(intArrayOf(4,2,0,3,2,5)) == 9
            ))

    // Test Case 4: Empty input
    println("Test Case 4: " + (
            solution.trap(intArrayOf()) == 0
            ))

    // Test Case 5: Single element (no water possible)
    println("Test Case 5: " + (
            solution.trap(intArrayOf(3)) == 0
            ))

    // Test Case 6: Flat surface (no valleys)
    println("Test Case 6: " + (
            solution.trap(intArrayOf(3,3,3)) == 0
            ))

    // Test Case 7: Small valley
    println("Test Case 7: " + (
            solution.trap(intArrayOf(3,2,3)) == 1
            ))

    // Test Case 8: Large input (Performance test)
    val largeInput = IntArray(10000) { if (it % 2 == 0) 0 else 1 } // Alternating peaks and valleys
    println("Test Case 8: " + (
            solution.trap(largeInput) >= 0 // Should run efficiently
            ))

    // Test Case 9: Two walls, no valley
    println("Test Case 9: " + (
            solution.trap(intArrayOf(2,2)) == 0
            ))

    // Test Case 10: Large wall in between
    println("Test Case 10: " + (
            solution.trap(intArrayOf(5,0,0,0,5)) == 15
            ))

    // Test Case 11: Multiple water trapping sections
    println("Test Case 11: " + (
            solution.trap(intArrayOf(0,3,1,2,1,4,1,2,1,5)) == 13
            ))

    // Test Case 12: Gradual increase and decrease (no water)
    println("Test Case 12: " + (
            solution.trap(intArrayOf(1,2,3,4,3,2,1)) == 0
            ))
}
