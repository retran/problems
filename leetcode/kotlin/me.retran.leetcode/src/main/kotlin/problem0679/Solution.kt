package problem0679

import kotlin.math.*

class Solution {
    fun backtrack(cards: MutableList<Double>): Boolean {
        if (cards.size == 1) {
            return abs(cards[0] - 24.0) < 1e-6
        }

        for (i in cards.indices) {
            for (j in i + 1 until cards.size) {
                val possibleResults = mutableListOf<Double>()

                possibleResults.add(cards[i] + cards[j])
                possibleResults.add(cards[i] - cards[j])
                possibleResults.add(cards[j] - cards[i])
                possibleResults.add(cards[i] * cards[j])

                if (cards[j] != 0.0) possibleResults.add(cards[i] / cards[j])
                if (cards[i] != 0.0) possibleResults.add(cards[j] / cards[i])

                val num1 = cards[i]
                val num2 = cards[j]
                cards.removeAt(j)
                cards.removeAt(i)

                for (result in possibleResults) {
                    cards.add(result)
                    if (backtrack(cards)) return true
                    cards.removeAt(cards.size - 1)
                }

                cards.add(i, num1)
                cards.add(j, num2)
            }
        }
        return false
    }

    fun judgePoint24(cards: IntArray): Boolean {
        return backtrack(cards.map { it.toDouble() }.toMutableList())
    }
}