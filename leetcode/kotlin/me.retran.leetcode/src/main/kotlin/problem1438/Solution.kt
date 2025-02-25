package problem1438

import kotlin.math.max

class Solution {
    fun longestSubarray(nums: IntArray, limit: Int): Int {
        val window = sortedMapOf<Int, Int>()
        var right = 0;
        var left = 0;
        var maxLength = 0

        while (right < nums.size) {
            window[nums[right]] = window.getOrDefault(nums[right], 0) + 1;
            while (window.lastKey() - window.firstKey() > limit) {
                window[nums[left]] = window[nums[left]]!! - 1
                if (window[nums[left]] == 0) {
                    window.remove(nums[left]);
                }
                left++
            }
            maxLength = max(maxLength, right - left + 1)
            right++
        }

        return maxLength
    }
}