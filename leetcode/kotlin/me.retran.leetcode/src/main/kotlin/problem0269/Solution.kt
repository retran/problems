package problem0269

class Solution {
    fun alienOrder(words: Array<String>): String {
        if (words.isEmpty())
            return ""

        val adjacencyList = Array(26) { mutableSetOf<Int>() }
        val inDegree = Array(26) { 0 }
        val uniqueLetters = mutableSetOf<Int>()

        for (word in words) {
            for (character in word) {
                uniqueLetters.add(character - 'a')
            }
        }

        for (i in 0 until words.size - 1) {
            val first = words[i]

            val second = words[i + 1]

            if (first.length > second.length && first.startsWith(second)) {
                return ""
            }

            for (j in 0 until minOf(first.length, second.length)) {
                val parent = first[j] - 'a'
                val child = second[j] - 'a'

                if (parent != child) {
                    if (adjacencyList[parent].add(child)) {
                        inDegree[child]++
                    }
                    break
                }
            }
        }

        val queue = ArrayDeque<Int>().apply {
            for (i in 0..inDegree.lastIndex) {
                if (inDegree[i] == 0 && i in uniqueLetters) {
                    add(i)
                }
            }
        }

        val result = StringBuilder()
        while (queue.isNotEmpty()) {
            val current = queue.removeFirst()
            result.append('a' + current)
            adjacencyList[current].forEach { neighbor ->
                inDegree[neighbor] = inDegree[neighbor] - 1
                if (inDegree[neighbor] == 0) {
                    queue.add(neighbor)
                }
            }
        }

        return if (result.length == uniqueLetters.size)
            result.toString()
        else ""
    }
}
