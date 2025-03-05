package problem0365

import kotlin.math.*

class Solution {
    data class State(val x: Int, val y: Int)

    fun canMeasureWater(x: Int, y: Int, target: Int): Boolean {
        if (target > x + y) {
            return false
        }

        val queue = ArrayDeque<State>()
        val visited = mutableSetOf<State>()

        queue.addLast(State(0, 0))
        while (queue.isNotEmpty()) {
            val current = queue.removeFirst()

            if (current.x + current.y == target) {
                return true
            }

            if (current in visited) {
                continue
            }

            visited.add(current)

            if (current.x < x) {
                queue.addLast(State(x, current.y))
            }

            if (current.x > 0) {
                queue.addLast(State(0, current.y))
            }

            if (current.y < y) {
                queue.addLast(State(current.x, y))
            }

            if (current.y > 0) {
                queue.addLast(State(current.x, 0))
            }

            if (current.x > 0 && current.y < y) {
                val amount = min(y - current.y, current.x)
                queue.addLast(State(current.x - amount, current.y + amount))
            }

            if (current.x < x && current.y > 0) {
                val amount = min(x - current.x, current.y)
                queue.addLast(State(current.x + amount, current.y - amount))
            }
        }

        return false
    }
}