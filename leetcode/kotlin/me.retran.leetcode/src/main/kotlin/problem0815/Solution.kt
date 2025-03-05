package problem0815

import java.util.*

class Solution {
    private val sourceId = 0
    private val targetId = 1

    fun numBusesToDestination(routes: Array<IntArray>, source: Int, target: Int): Int {
        if (source == target) {
            return 0
        }

        val graph = buildGraph(routes, source, target)
        return getDistance(graph)
    }

    private fun getDistance(graph: List<Set<Int>>): Int {
        val distances = IntArray(graph.size) { Int.MAX_VALUE }
        distances[sourceId] = 0

        val queue = PriorityQueue<Pair<Int, Int>> { p1, p2 -> p1.second - p2.second }
        queue.add(sourceId to 0)

        val visited = mutableSetOf<Int>()
        while (queue.isNotEmpty()) {
            val current = queue.poll()

            if (current.first in visited) {
                continue
            }

            visited.add(current.first)

            val neighbors = graph[current.first]
            for (neighbor in neighbors) {
                val distance = distances[current.first] + 1
                if (distances[neighbor] > distance) {
                    distances[neighbor] = distance
                    queue.add(neighbor to distance)
                }
            }
        }

        if (distances[targetId] == Int.MAX_VALUE) {
            return -1
        }

        return distances[targetId] - 1
    }

    private fun buildGraph(routes: Array<IntArray>, source: Int, target: Int): List<Set<Int>> {
        val routeToStops = routes
            .map { it.toSet() }

        val graph = Array<MutableSet<Int>>(routes.size + 2) { mutableSetOf() }
        for (i in 0..routeToStops.lastIndex) {
            if (routeToStops[i].contains(source)) {
                graph[i + 2].add(sourceId)
                graph[sourceId].add(i + 2)
            }

            if (routeToStops[i].contains(target)) {
                graph[i + 2].add(targetId)
                graph[targetId].add(i + 2)
            }

            for (j in 0..routeToStops.lastIndex) {
                if (i == j) {
                    continue
                }

                if (routeToStops[j].intersect(routeToStops[i]).isNotEmpty()) {
                    graph[i + 2].add(j + 2)
                    graph[j + 2].add(i + 2)
                }
            }
        }

        return graph
            .map { it.toSet() }
    }
}