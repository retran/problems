package problem0136

class Solution {
    fun singleNumber(nums: IntArray): Int {
        var aggregate = 0
        for (number in nums) {
            aggregate = aggregate.xor(number)
        }
        return 0.xor(aggregate)
    }
}