package problem0399

typealias Graph = MutableMap<String, MutableMap<String, Double>>

class Solution {
    fun calcEquation(equations: List<List<String>>, values: DoubleArray, queries: List<List<String>>): DoubleArray {
        val equationGraph = buildGraph(equations, values)
        floydWarshall(equationGraph)

        return queries.map { query ->
            val (dividend, divisor) = query
            equationGraph[dividend]?.get(divisor) ?: -1.0
        }.toDoubleArray()
    }

    private fun buildGraph(equations: List<List<String>>, values: DoubleArray): Graph {
        val graph: Graph = mutableMapOf()

        for (i in equations.indices) {
            val (dividend, divisor) = equations[i]
            val value = values[i]

            graph.computeIfAbsent(dividend) { mutableMapOf() }[divisor] = value
            graph.computeIfAbsent(divisor) { mutableMapOf() }[dividend] = 1.0 / value

            graph[dividend]!![dividend] = 1.0
            graph[divisor]!![divisor] = 1.0
        }

        return graph
    }

    private fun floydWarshall(graph: Graph) {
        val nodes = graph.keys.toList()

        for (mid in nodes) {
            for (start in nodes) {
                for (end in nodes) {
                    if (graph[start]?.containsKey(mid) == true && graph[mid]?.containsKey(end) == true) {
                        val newDistance = graph[start]!![mid]!! * graph[mid]!![end]!!
                        graph[start]!![end] = newDistance
                        graph[end]!![start] = 1.0 / newDistance
                    }
                }
            }
        }
    }
}
