package problem0410

import kotlin.math.max

class Solution {
    data class State(val startIndex: Int, val k: Int)

    private val cache = mutableMapOf<State, Int>()

    fun splitArray(nums: IntArray, k: Int): Int {
        if (nums.isEmpty()) {
            return 0
        }

        if (k >= nums.size) {
            return nums.maxOrNull() ?: 0
        }

        val prefixSums = computePrefixSums(nums)
        return splitArrayImpl(nums, prefixSums, 0, k - 1)
    }

    private fun splitArrayImpl(nums: IntArray, prefixSums: IntArray, startIndex: Int, k: Int): Int {
        val state = State(startIndex, k)
        return cache.getOrPut(state) {
            if (k == 0) {
                return@getOrPut prefixSums[nums.size] - prefixSums[startIndex]
            }

            var minBiggestSum = Int.MAX_VALUE
            var leftSum = 0

            for (i in startIndex until nums.size - k) {
                leftSum += nums[i]
                val rightBiggestSum = splitArrayImpl(nums, prefixSums, i + 1, k - 1)
                minBiggestSum = minOf(minBiggestSum, max(leftSum, rightBiggestSum))

                if (leftSum > minBiggestSum) {
                    break
                }
            }

            minBiggestSum
        }
    }

    private fun computePrefixSums(nums: IntArray): IntArray {
        val prefixSums = IntArray(nums.size + 1)
        for (i in nums.indices) {
            prefixSums[i + 1] = prefixSums[i] + nums[i]
        }
        return prefixSums
    }
}

fun test() {
    println("Test Case 1: " + (Solution().splitArray(intArrayOf(7, 2, 5, 10, 8), 2) == 18))
    println("Test Case 2: " + (Solution().splitArray(intArrayOf(1, 2, 3, 4, 5), 2) == 9))
    println("Test Case 3: " + (Solution().splitArray(intArrayOf(1, 4, 4), 3) == 4))
    println("Test Case 4: " + (Solution().splitArray(intArrayOf(5, 5, 5, 5, 5), 2) == 15))
    println("Test Case 5: " + (Solution().splitArray(intArrayOf(10, 1, 1, 1, 10), 3) == 10))
    println("Test Case 6: " + (Solution().splitArray(intArrayOf(1, 2, 3, 4, 5, 6, 7, 8, 9), 3) == 17))
    println("Test Case 7: " + (Solution().splitArray(intArrayOf(1, 2, 3, 4), 1) == 10))
    println("Test Case 8: " + (Solution().splitArray(intArrayOf(1, 2, 3, 4), 4) == 4))
}
