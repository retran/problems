package problem0928

class Solution {
    fun minMalwareSpread(graph: Array<IntArray>, initial: IntArray): Int {
        val n = graph.size
        val initialSet = initial.toHashSet()
        val sortedInitial = initial.sorted()

        var bestCandidate = sortedInitial[0]
        var bestInfected = Int.MAX_VALUE

        for (r in sortedInitial) {
            val visited = BooleanArray(n)
            var totalInfected = 0

            for (i in 0 until n) {
                if (i == r) {
                    continue
                }
                if (!visited[i]) {
                    var compSize = 0
                    var compHasInfected = false
                    val stack = ArrayDeque<Int>()
                    stack.add(i)
                    visited[i] = true
                    while (stack.isNotEmpty()) {
                        val cur = stack.removeLast()

                        compSize++

                        if (cur in initialSet && cur != r) {
                            compHasInfected = true
                        }

                        for (j in 0 until n) {
                            if (j == r) continue
                            if (graph[cur][j] == 1 && !visited[j]) {
                                visited[j] = true
                                stack.add(j)
                            }
                        }
                    }

                    if (compHasInfected) {
                        totalInfected += compSize
                    }
                }
            }

            if (totalInfected < bestInfected) {
                bestInfected = totalInfected
                bestCandidate = r
            }
        }

        return bestCandidate
    }
}
