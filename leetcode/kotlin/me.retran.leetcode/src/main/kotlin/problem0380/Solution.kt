package problem0380

import kotlin.random.Random
import java.util.concurrent.locks.ReentrantReadWriteLock

class RandomizedSet {
    private val numLocks = 16
    private val locks = Array(numLocks) { ReentrantReadWriteLock() }

    private val valueToIndex = mutableMapOf<Int, Int>()
    private val values = mutableListOf<Int>()

    private fun getLockForValue(`val`: Int): ReentrantReadWriteLock {
        return locks[(`val`.hashCode() and Int.MAX_VALUE) % numLocks]
    }

    fun insert(`val`: Int): Boolean {
        val lock = getLockForValue(`val`)
        lock.writeLock().lock()
        try {
            if (`val` in valueToIndex) return false
            valueToIndex[`val`] = values.size
            values.add(`val`)
            return true
        } finally {
            lock.writeLock().unlock()
        }
    }

    fun remove(`val`: Int): Boolean {
        val lock = getLockForValue(`val`)
        lock.writeLock().lock()
        try {
            val index = valueToIndex[`val`] ?: return false

            val lastValue = values.last()
            values[index] = lastValue
            valueToIndex[lastValue] = index

            values.removeAt(values.lastIndex)
            valueToIndex.remove(`val`)
            return true
        } finally {
            lock.writeLock().unlock()
        }
    }

    fun getRandom(): Int {
        locks.forEach { it.readLock().lock() }
        try {
            return values[Random.nextInt(values.size)]
        } finally {
            locks.forEach { it.readLock().unlock() }
        }
    }
}
