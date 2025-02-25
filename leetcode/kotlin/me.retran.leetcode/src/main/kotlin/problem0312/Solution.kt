package problem0312

class Solution {
    private fun maxCoins(
        cache: MutableMap<Pair<Int, Int>, Int>,
        nums: IntArray,
        left: Int,
        right: Int
    ): Int {
        if (right - left < 0)
            return 0

        val key = Pair(left, right)
        if (cache.containsKey(key))
            return cache[key]!!

        val leftScore = if (left > 0)
            nums[left - 1]
        else 1

        val rightScore = if (right < nums.size - 1)
            nums[right + 1]
        else 1

        var maxCoins = 0
        if (right == left) {
            maxCoins = leftScore * nums[left] * rightScore
        } else {
            for (i in left..right) {
                val coins = leftScore * nums[i] * rightScore +
                        maxCoins(cache, nums, left, i - 1) +
                        maxCoins(cache, nums, i + 1, right)
                maxCoins = maxOf(maxCoins, coins)
            }
        }
        cache[key] = maxCoins
        return maxCoins
    }

    fun maxCoins(nums: IntArray): Int {
        return maxCoins(mutableMapOf(), nums, 0, nums.size - 1)
    }
}
