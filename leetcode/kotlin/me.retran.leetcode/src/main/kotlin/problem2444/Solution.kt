package problem2444

import kotlin.math.max
import kotlin.math.min

class Solution {
    fun countSubarrays(nums: IntArray, minK: Int, maxK: Int): Long {
        var answer: Long = 0
        var minIndex = -1
        var maxIndex = -1
        var boundIndex = -1

        for (i in nums.indices) {
            if (nums[i] < minK || nums[i] > maxK) {
                boundIndex = i
            }
            if (nums[i] == maxK) {
                maxIndex = i
            }
            if (nums[i] == minK) {
                minIndex = i
            }
            answer += max(0, min(minIndex, maxIndex) - boundIndex)
        }

        return answer
    }
}

fun test() {
    val solution = Solution()

    // Test Case 1:
    // Input: [1, 3, 5, 2, 7, 5], minK = 1, maxK = 5
    // Expected: 2
    val test1 = solution.countSubarrays(intArrayOf(1, 3, 5, 2, 7, 5), 1, 5)
    println("Test Case 1: Expected = 2, Got = $test1")

    // Test Case 2:
    // Input: [1, 1, 1], minK = 1, maxK = 1
    // All subarrays are valid: 3 + 2 + 1 = 6
    val test2 = solution.countSubarrays(intArrayOf(1, 1, 1), 1, 1)
    println("Test Case 2: Expected = 6, Got = $test2")

    // Test Case 3:
    // Input: [1, 5, 5, 1, 5], minK = 1, maxK = 5
    // Valid subarrays count should be 9.
    val test3 = solution.countSubarrays(intArrayOf(1, 5, 5, 1, 5), 1, 5)
    println("Test Case 3: Expected = 9, Got = $test3")

    // Test Case 4:
    // Input: [1, 2, 3, 4], minK = 2, maxK = 3
    // Only the subarray [2,3] is valid.
    val test4 = solution.countSubarrays(intArrayOf(1, 2, 3, 4), 2, 3)
    println("Test Case 4: Expected = 1, Got = $test4")

    // Test Case 5:
    // Input: [2, 2, 2, 2], minK = 2, maxK = 2
    // All subarrays are valid. For an array of length 4, number of subarrays is 4+3+2+1 = 10.
    val test5 = solution.countSubarrays(intArrayOf(2, 2, 2, 2), 2, 2)
    println("Test Case 5: Expected = 10, Got = $test5")
}