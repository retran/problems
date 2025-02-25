package problem0330

class Solution {
    fun minPatches(nums: IntArray, n: Int): Int {
        var miss: Long = 1
        var patches = 0
        var i = 0
        while (miss <= n) {
            if (i < nums.size && nums[i] <= miss) {
                miss += nums[i]
                i++
            } else {
                miss += miss
                patches++
            }
        }
        return patches
    }
}