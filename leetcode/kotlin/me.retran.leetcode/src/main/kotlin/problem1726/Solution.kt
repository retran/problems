package problem1726

class Solution {
    fun tupleSameProduct(nums: IntArray): Int {
        val productionsToSetsOfPairs = mutableMapOf<Int, MutableSet<Pair<Int, Int>>>()

        for (i in 0..nums.size - 1) {
            for (j in i + 1..nums.size - 1) {
                if (i == j || nums[j] == nums[i]) {
                    continue
                }

                val product = nums[i] * nums[j]
                productionsToSetsOfPairs[product] = productionsToSetsOfPairs.getOrPut(product) { mutableSetOf() }

                val pair = when {
                    nums[i] < nums[j] -> Pair(nums[i], nums[j])
                    else -> Pair(nums[j], nums[i])
                }

                productionsToSetsOfPairs[product]!!.add(pair)
            }
        }

        var count = 0
        for (setOfPairs in productionsToSetsOfPairs) {
            if (setOfPairs.value.size > 1) {
                count += setOfPairs.value.size * (setOfPairs.value.size - 1) * 4
            }
        }

        return count
    }
}