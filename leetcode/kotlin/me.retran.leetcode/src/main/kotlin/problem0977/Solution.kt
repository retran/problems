package problem0977

class Solution {
    fun sortedSquares(nums: IntArray): IntArray {
        val n = nums.size
        val result = IntArray(n)

        var index = nums.binarySearch(0)
        if (index < 0) {
            index = index.inv()
        }

        var left = index - 1
        var right = index

        var k = 0
        while (left >= 0 || right < n) {
            val leftSquare = if (left >= 0) nums[left] * nums[left] else Int.MAX_VALUE
            val rightSquare = if (right < n) nums[right] * nums[right] else Int.MAX_VALUE

            if (leftSquare < rightSquare) {
                result[k] = leftSquare
                left--
            } else {
                result[k] = rightSquare
                right++
            }
            k++
        }
        return result
    }
}
