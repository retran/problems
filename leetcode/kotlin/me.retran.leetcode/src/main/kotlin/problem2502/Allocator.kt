package problem2502

class Allocator(private val n: Int) {
    data class Interval(val start: Int, val end: Int) {
        fun size() = end - start + 1
    }

    private val startIndex = IntArray(4 * n)
    private val endIndex   = IntArray(4 * n)

    private val prefFree   = IntArray(4 * n)
    private val suffFree   = IntArray(4 * n)
    private val maxFree    = IntArray(4 * n)

    private val lazy       = IntArray(4 * n)

    private val allocated = mutableMapOf<Int, MutableList<Interval>>()

    init {
        build(1, 0, n - 1)
    }

    private fun build(idx: Int, left: Int, right: Int) {
        startIndex[idx] = left
        endIndex[idx] = right
        lazy[idx] = -1

        if (left == right) {
            prefFree[idx] = 1
            suffFree[idx] = 1
            maxFree[idx] = 1
            return
        }
        val mid = (left + right) shr 1
        build(idx * 2, left, mid)
        build(idx * 2 + 1, mid + 1, right)
        pull(idx)
    }

    private fun pull(idx: Int) {
        val leftChild = idx * 2
        val rightChild = idx * 2 + 1

        val leftSize = endIndex[leftChild] - startIndex[leftChild] + 1
        val rightSize = endIndex[rightChild] - startIndex[rightChild] + 1

        prefFree[idx] = prefFree[leftChild]
        if (prefFree[leftChild] == leftSize) {
            prefFree[idx] += prefFree[rightChild]
        }

        suffFree[idx] = suffFree[rightChild]
        if (suffFree[rightChild] == rightSize) {
            suffFree[idx] += suffFree[leftChild]
        }

        maxFree[idx] = maxOf(
            maxFree[leftChild],
            maxFree[rightChild],
            suffFree[leftChild] + prefFree[rightChild]
        )
    }

    private fun applySet(idx: Int, setUsed: Boolean) {
        val size = endIndex[idx] - startIndex[idx] + 1
        if (setUsed) {
            prefFree[idx] = 0
            suffFree[idx] = 0
            maxFree[idx] = 0
            lazy[idx] = 1
        } else {
            prefFree[idx] = size
            suffFree[idx] = size
            maxFree[idx] = size
            lazy[idx] = 0
        }
    }

    private fun pushDown(idx: Int) {
        if (lazy[idx] != -1) {
            val leftChild = idx * 2
            val rightChild = idx * 2 + 1
            applySet(leftChild, lazy[idx] == 1)
            applySet(rightChild, lazy[idx] == 1)
            lazy[idx] = -1
        }
    }

    private fun updateRange(idx: Int, left: Int, right: Int, ql: Int, qr: Int, setUsed: Boolean) {
        if (qr < left || ql > right) return
        if (ql <= left && right <= qr) {
            applySet(idx, setUsed)
            return
        }
        pushDown(idx)
        val mid = (left + right) shr 1
        updateRange(idx * 2, left, mid, ql, qr, setUsed)
        updateRange(idx * 2 + 1, mid + 1, right, ql, qr, setUsed)
        pull(idx)
    }

    private fun findBlock(idx: Int, size: Int): Int {
        if (maxFree[idx] < size) {
            return -1
        }
        val leftSeg = startIndex[idx]
        val rightSeg = endIndex[idx]
        if (leftSeg == rightSeg) {
            return leftSeg
        }

        pushDown(idx)

        val leftChild = idx * 2
        val rightChild = idx * 2 + 1

        if (maxFree[leftChild] >= size) {
            return findBlock(leftChild, size)
        }

        val leftSuffix = suffFree[leftChild]
        val rightPrefix = prefFree[rightChild]
        if (leftSuffix + rightPrefix >= size) {
            return endIndex[leftChild] - leftSuffix + 1
        }

        return findBlock(rightChild, size)
    }

    fun allocate(size: Int, mID: Int): Int {
        if (maxFree[1] < size) return -1

        val startPos = findBlock(1, size)
        if (startPos == -1) {
            return -1
        }
        val endPos = startPos + size - 1

        updateRange(1, 0, n - 1, startPos, endPos, setUsed = true)

        allocated.computeIfAbsent(mID) { mutableListOf() }
            .add(Interval(startPos, endPos))

        return startPos
    }

    fun freeMemory(mID: Int): Int {
        val intervals = allocated[mID] ?: return 0
        var totalFreed = 0
        for (intv in intervals) {
            val sz = intv.size()
            totalFreed += sz
            updateRange(1, 0, n - 1, intv.start, intv.end, setUsed = false)
        }
        allocated.remove(mID)
        return totalFreed
    }
}
