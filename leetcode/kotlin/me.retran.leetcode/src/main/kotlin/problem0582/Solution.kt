package problem0582

import kotlin.random.Random

class Solution(w: IntArray) {
    private val prefixSums = IntArray(w.size + 1)

    init {
        for (i in w.indices) {
            prefixSums[i + 1] = prefixSums[i] + w[i]
        }
    }

    fun pickIndex(): Int {
        val totalSum = prefixSums.last()
        val randomValue = Random.nextInt(totalSum) + 1
        var index = prefixSums.binarySearch(randomValue)
        if (index < 0) {
            index = -index - 1
        }
        return index - 1
    }
}
