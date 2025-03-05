package problem3387

class Solution {
    fun maxAmount(
        initialCurrency: String,
        pairs1: List<List<String>>,
        rates1: DoubleArray,
        pairs2: List<List<String>>,
        rates2: DoubleArray
    ): Double {
        fun addEdge(
            graph: MutableMap<String, MutableList<Pair<String, Double>>>,
            from: String,
            to: String,
            rate: Double
        ) {
            graph.computeIfAbsent(from) { mutableListOf() }.add(Pair(to, rate))
        }

        val graph1 = mutableMapOf<String, MutableList<Pair<String, Double>>>()
        for (i in pairs1.indices) {
            val from = pairs1[i][0]
            val to = pairs1[i][1]
            val rate = rates1[i]
            addEdge(graph1, from, to, rate)
            addEdge(graph1, to, from, 1.0 / rate)
        }

        val factor1 = mutableMapOf<String, Double>()
        fun dfs1(currency: String, currentFactor: Double) {
            if (currency in factor1) return
            factor1[currency] = currentFactor
            for ((next, rate) in graph1.getOrDefault(currency, emptyList())) {
                dfs1(next, currentFactor * rate)
            }
        }
        dfs1(initialCurrency, 1.0)

        val graph2 = mutableMapOf<String, MutableList<Pair<String, Double>>>()
        for (i in pairs2.indices) {
            val from = pairs2[i][0]
            val to = pairs2[i][1]
            val rate = rates2[i]
            addEdge(graph2, from, to, rate)
            addEdge(graph2, to, from, 1.0 / rate)
        }

        val factor2 = mutableMapOf<String, Double>()
        fun dfs2(currency: String, currentFactor: Double) {
            if (currency in factor2) return
            factor2[currency] = currentFactor
            for ((next, rate) in graph2.getOrDefault(currency, emptyList())) {
                dfs2(next, currentFactor * rate)
            }
        }
        dfs2(initialCurrency, 1.0)

        var answer = 1.0
        for ((currency, f1) in factor1) {
            if (currency in factor2) {
                val g2 = factor2[currency]!!
                val candidate = f1 / g2
                if (candidate > answer) answer = candidate
            }
        }
        return answer
    }
}
