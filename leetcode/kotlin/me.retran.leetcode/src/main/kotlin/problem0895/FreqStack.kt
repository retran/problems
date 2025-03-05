package problem0895

class FreqStack {
    private val freqMap = mutableMapOf<Int, Int>()
    private val group = mutableMapOf<Int, MutableList<Int>>()
    private var maxFreq = 0

    fun push(`val`: Int) {
        val f = freqMap.getOrDefault(`val`, 0) + 1
        freqMap[`val`] = f
        group.computeIfAbsent(f) { mutableListOf() }.add(`val`)
        if (f > maxFreq) {
            maxFreq = f
        }
    }

    fun pop(): Int {
        val list = group[maxFreq]!!
        val x = list.removeAt(list.size - 1)
        freqMap[x] = freqMap[x]!! - 1
        if (list.isEmpty()) {
            group.remove(maxFreq)
            maxFreq--
        }
        return x
    }
}
