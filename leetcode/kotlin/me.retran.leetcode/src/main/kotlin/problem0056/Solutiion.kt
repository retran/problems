package problem0056

class Solution {
    fun merge(intervals: Array<IntArray>): Array<IntArray> {
        val sortedIntervals = intervals.sortedBy { it[0] }
        val mergedIntervals = mutableListOf<IntArray>()

        for (interval in sortedIntervals) {
            val currentInterval = mergedIntervals.lastOrNull()
            if (currentInterval != null && currentInterval[1] >= interval[0]) {
                currentInterval[1] = maxOf(currentInterval[1], interval[1])
            } else {
                mergedIntervals.add(interval)
            }
        }

        return mergedIntervals.toTypedArray()
    }
}