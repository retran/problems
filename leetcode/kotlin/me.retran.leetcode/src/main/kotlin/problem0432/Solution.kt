package problem0432

import java.util.LinkedList

class AllOne {
    private class Bucket(val count: Int) {
        val keys = mutableSetOf<String>()
    }

    private val buckets = LinkedList<Bucket>()

    private val keyToBucket = HashMap<String, Bucket>()

    fun inc(key: String) {
        val curBucket = keyToBucket[key]
        if (curBucket == null) {
            if (buckets.isNotEmpty() && buckets.first.count == 1) {
                buckets.first.keys.add(key)
                keyToBucket[key] = buckets.first
            } else {
                val newBucket = Bucket(1)
                newBucket.keys.add(key)
                buckets.addFirst(newBucket)
                keyToBucket[key] = newBucket
            }
        } else {
            val curIndex = buckets.indexOf(curBucket)
            curBucket.keys.remove(key)
            val newCount = curBucket.count + 1
            var nextBucket: Bucket? = null
            if (curIndex + 1 < buckets.size && buckets[curIndex + 1].count == newCount) {
                nextBucket = buckets[curIndex + 1]
            }
            if (nextBucket != null) {
                nextBucket.keys.add(key)
                keyToBucket[key] = nextBucket
            } else {
                val newBucket = Bucket(newCount)
                newBucket.keys.add(key)
                buckets.add(curIndex + 1, newBucket)
                keyToBucket[key] = newBucket
            }
            if (curBucket.keys.isEmpty()) {
                buckets.removeAt(curIndex)
            }
        }
    }

    fun dec(key: String) {
        val curBucket = keyToBucket[key] ?: return
        val curIndex = buckets.indexOf(curBucket)
        curBucket.keys.remove(key)
        if (curBucket.count == 1) {
            keyToBucket.remove(key)
        } else {
            val newCount = curBucket.count - 1
            var prevBucket: Bucket? = null
            if (curIndex - 1 >= 0 && buckets[curIndex - 1].count == newCount) {
                prevBucket = buckets[curIndex - 1]
            }
            if (prevBucket != null) {
                prevBucket.keys.add(key)
                keyToBucket[key] = prevBucket
            } else {
                val newBucket = Bucket(newCount)
                newBucket.keys.add(key)
                buckets.add(curIndex, newBucket)
                keyToBucket[key] = newBucket
            }
        }
        if (curBucket.keys.isEmpty()) {
            buckets.remove(curBucket)
        }
    }

    fun getMaxKey(): String {
        if (buckets.isEmpty()) return ""
        val lastBucket = buckets.last
        return if (lastBucket.keys.isNotEmpty()) lastBucket.keys.first() else ""
    }

    fun getMinKey(): String {
        if (buckets.isEmpty()) return ""
        val firstBucket = buckets.first
        return if (firstBucket.keys.isNotEmpty()) firstBucket.keys.first() else ""
    }
}

fun test() {
    val allOne = AllOne()

    println("Initial: max='${allOne.getMaxKey()}', min='${allOne.getMinKey()}'")

    allOne.inc("hello")
    println("After inc('hello'): max='${allOne.getMaxKey()}', min='${allOne.getMinKey()}'")

    allOne.inc("world")
    println("After inc('world'): max='${allOne.getMaxKey()}', min='${allOne.getMinKey()}'")

    allOne.inc("hello")
    println("After inc('hello') again: max='${allOne.getMaxKey()}', min='${allOne.getMinKey()}'")

    allOne.dec("hello")
    println("After dec('hello'): max='${allOne.getMaxKey()}', min='${allOne.getMinKey()}'")

    allOne.dec("hello")
    println("After dec('hello') again: max='${allOne.getMaxKey()}', min='${allOne.getMinKey()}'")

    allOne.dec("world")
    println("After dec('world'): max='${allOne.getMaxKey()}', min='${allOne.getMinKey()}'")

    allOne.inc("a")
    allOne.inc("b")
    allOne.inc("c")
    allOne.inc("a")
    allOne.inc("b")
    println("After inc(a,b,c,a,b): max='${allOne.getMaxKey()}', min='${allOne.getMinKey()}'")

    allOne.inc("c")
    allOne.inc("c")
    println("After inc('c') twice: max='${allOne.getMaxKey()}', min='${allOne.getMinKey()}'")

    allOne.dec("c")
    println("After dec('c'): max='${allOne.getMaxKey()}', min='${allOne.getMinKey()}'")
}
