package problem2187

import kotlin.math.min

class Solution {
    fun minimumTime(time: IntArray, totalTrips: Int): Long {
        fun getTripsForTime(currentTime: Long): Long {
            var count: Long = 0
            for (t in time) {
                count += currentTime / t
            }
            return count
        }

        fun binarySearch(from: Long, to: Long, target: Int): Long {
            var left = from
            var right = to
            var answer: Long = to

            while (left <= right) {
                val mid = left + (right - left) / 2
                val trips = getTripsForTime(mid)

                if (trips in 0..<target) {
                    left = mid + 1
                } else {
                    answer = min(answer, mid)
                    right = mid - 1
                }
            }

            return answer
        }

        return binarySearch(0, time.max().toLong() * totalTrips.toLong(), totalTrips)
    }
}