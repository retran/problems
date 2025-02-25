package problem2493

class Solution {
    fun magnificentSets(n: Int, edges: Array<IntArray>): Int {
        val graph = buildGraph(n, edges)
        var answer = 0
        val colors = IntArray(n + 1) { -1 }
        for (i in 1..n) {
            if (colors[i] == -1) {
                val (isBipartite, component) = getBipartiteComponent(graph, colors, i)
                if (!isBipartite) {
                    return -1
                }
                answer += computeMaxPartitionOfComponent(n, graph, component)
            }
        }
        return answer
    }

    private fun buildGraph(n: Int, edges: Array<IntArray>): Array<MutableList<Int>> {
        val graph = Array(n + 1) { mutableListOf<Int>() }
        for (edge in edges) {
            val u = edge[0]
            val v = edge[1]
            graph[u].add(v)
            graph[v].add(u)
        }
        return graph
    }

    private fun getBipartiteComponent(
        graph: Array<MutableList<Int>>,
        colors: IntArray,
        start: Int
    ): Pair<Boolean, List<Int>> {
        val component = mutableListOf<Int>()
        val queue = ArrayDeque<Int>()
        queue.add(start)
        colors[start] = 0
        component.add(start)
        while (queue.isNotEmpty()) {
            val cur = queue.removeFirst()
            for (neighbour in graph[cur]) {
                if (colors[neighbour] == -1) {
                    colors[neighbour] = 1 - colors[cur]
                    queue.add(neighbour)
                    component.add(neighbour)
                } else if (colors[neighbour] == colors[cur]) {
                    return Pair(false, emptyList())
                }
            }
        }
        return Pair(true, component)
    }

    private fun computeMaxPartitionOfComponent(
        n: Int,
        graph: Array<MutableList<Int>>,
        component: List<Int>
    ): Int {
        var maxGroups = 0
        for (start in component) {
            val groups = getMaxDistance(start, graph, n) + 1
            maxGroups = maxOf(maxGroups, groups)
        }
        return maxGroups
    }

    private fun getMaxDistance(
        start: Int,
        graph: Array<MutableList<Int>>,
        n: Int
    ): Int {
        val distances = IntArray(n + 1) { -1 }
        val queue = ArrayDeque<Int>()
        queue.add(start)
        distances[start] = 0
        var maxDistance = 0
        while (queue.isNotEmpty()) {
            val current = queue.removeFirst()
            maxDistance = maxOf(maxDistance, distances[current])
            for (neighbour in graph[current]) {
                if (distances[neighbour] == -1) {
                    distances[neighbour] = distances[current] + 1
                    queue.add(neighbour)
                }
            }
        }
        return maxDistance
    }
}

// ---------- Testing ----------

fun test() {
    val solution = Solution()

    // Test Case 1: Simple path (bipartite)
    // Graph: 1 - 2 - 3 - 4
    // For a path of 4 nodes, maximum groups = 4 (e.g. from one end)
    val edges1 = arrayOf(
        intArrayOf(1, 2),
        intArrayOf(2, 3),
        intArrayOf(3, 4)
    )
    val result1 = solution.magnificentSets(4, edges1)
    println("Test Case 1: Expected 4, Got $result1")

    // Test Case 2: Not bipartite (triangle)
    // Graph: 1 - 2, 2 - 3, 3 - 1
    val edges2 = arrayOf(
        intArrayOf(1, 2),
        intArrayOf(2, 3),
        intArrayOf(3, 1)
    )
    val result2 = solution.magnificentSets(3, edges2)
    println("Test Case 2: Expected -1, Got $result2")

    // Test Case 3: Two disconnected components (both bipartite)
    // Component 1: 1 - 2 - 3  => max groups = 3
    // Component 2: 4 - 5 - 6  => max groups = 3
    // Total expected = 3 + 3 = 6
    val edges3 = arrayOf(
        intArrayOf(1, 2),
        intArrayOf(2, 3),
        intArrayOf(4, 5),
        intArrayOf(5, 6)
    )
    val result3 = solution.magnificentSets(6, edges3)
    println("Test Case 3: Expected 6, Got $result3")

    // Test Case 4: Single node (no edges)
    // Only one node means one group.
    val edges4 = emptyArray<IntArray>()
    val result4 = solution.magnificentSets(1, edges4)
    println("Test Case 4: Expected 1, Got $result4")

    // Test Case 5: Star graph (bipartite)
    // Graph: 1 connected to 2, 3, 4, 5.
    // For component, starting from the center 1: max distance = 1 so groups = 2.
    // Expected = 2.
    val edges5 = arrayOf(
        intArrayOf(1, 2),
        intArrayOf(1, 3),
        intArrayOf(1, 4),
        intArrayOf(1, 5)
    )
    val result5 = solution.magnificentSets(5, edges5)
    println("Test Case 5: Expected 2, Got $result5")
}