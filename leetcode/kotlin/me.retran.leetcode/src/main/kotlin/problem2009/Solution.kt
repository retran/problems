package problem2009

class Solution {
    fun minOperations(nums: IntArray): Int {
        val n = nums.size
        val sorted = nums.toSet().sorted().toIntArray()
        var minOperations = n
        for (i in sorted.indices) {
            val windowEnd = sorted[i] + n - 1
            var idx = sorted.binarySearch(windowEnd + 1)
            if (idx < 0) {
                idx = -idx - 1
            }
            val count = idx - i
            minOperations = minOf(minOperations, n - count)
        }
        return minOperations
    }
}

fun test() {
    val tests = listOf(
        intArrayOf(1, 2, 3, 5) to 1,    // Change one number to make [1,2,3,4] or [2,3,4,5]
        intArrayOf(1, 2, 3, 4) to 0,    // Already consecutive
        intArrayOf(1, 5, 4, 3, 2) to 0, // Already consecutive after sorting: [1,2,3,4,5]
        intArrayOf(1, 2, 2, 3) to 1,    // After distinct: [1,2,3] => need 1 operation to have 4 consecutive numbers
        intArrayOf(1, 3, 5, 7) to 2     // Best window covers 2 numbers, so 2 operations needed
    )

    for ((nums, expected) in tests) {
        val result = Solution().minOperations(nums)
        println("Input: ${nums.joinToString(", ")} | Expected: $expected, Got: $result")
    }
}
