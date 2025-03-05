package problem0295

import java.util.PriorityQueue

class MedianFinder {
    private val minHeap = PriorityQueue<Int>()
    private val maxHeap = PriorityQueue<Int>(Comparator.reverseOrder())
    private var count = 0

    fun addNum(num: Int) {
        if (count == 0 || maxHeap.peek() >= num) {
            maxHeap.add(num)
        } else {
            minHeap.add(num)
        }

        if (maxHeap.size > minHeap.size + 1) {
            minHeap.add(maxHeap.poll())
        } else if (minHeap.size > maxHeap.size) {
            maxHeap.add(minHeap.poll())
        }

        count++
    }

    fun findMedian(): Double {
        if (count == 0) {
            throw IllegalArgumentException()
        }

        if (count % 2 == 0) {
            return (minHeap.peek().toDouble() + maxHeap.peek().toDouble()) / 2
        } else {
            if (minHeap.size > maxHeap.size) {
                return minHeap.peek().toDouble()
            } else {
                return maxHeap.peek().toDouble()
            }
        }
    }
}

/**
 * Your MedianFinder object will be instantiated and called as such:
 * var obj = MedianFinder()
 * obj.addNum(num)
 * var param_2 = obj.findMedian()
 */