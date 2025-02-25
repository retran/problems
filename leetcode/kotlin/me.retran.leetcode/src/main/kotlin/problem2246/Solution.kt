package problem2246

import kotlin.math.max

class Solution {
    fun longestPath(parent: IntArray, s: String): Int {
        val inDegrees = IntArray(parent.size) { 0 }
        val paths = Array(parent.size) { mutableListOf(0) }
        var longestPath = 1

        for (node in parent) {
            if (node != -1)
            {
                inDegrees[node]++
            }
        }

        val queue = ArrayDeque<Int>()
        for (i in 0..parent.lastIndex) {
            if (inDegrees[i] == 0) {
                queue.addLast(i)
            }
        }

        while (queue.isNotEmpty()) {
            val currentNode = queue.removeFirst()
            val currentParent = parent[currentNode]

            if (paths[currentNode].size == 1) {
                longestPath = max(longestPath, paths[currentNode][0] + 1)
            } else {
                val sum = paths[currentNode]
                    .sortedByDescending { it }
                    .take(2)
                    .sum() + 1
                longestPath = max(longestPath, sum)
            }

            if (currentParent != -1) {
                if (s[currentNode] != s[currentParent])
                    paths[currentParent].add(paths[currentNode].max() + 1)

                inDegrees[currentParent]--

                if (inDegrees[currentParent] == 0)
                    queue.addLast(currentParent)
            }
        }

        return longestPath
    }
}