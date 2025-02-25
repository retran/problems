package problem3161

import kotlin.math.*

class Solution {
    private class TreeNode(val start: Int, val end: Int) {
        var maxFree: Int = end - start
        var left: TreeNode? = null
        var right: TreeNode? = null
    }

    private fun putObstacle(node: TreeNode, obstacle: Int) {
        if (node.start == obstacle || node.end == obstacle) {
            return
        }

        if (node.left == null && node.right == null) {
            node.left = TreeNode(node.start, obstacle)
            node.right = TreeNode(obstacle, node.end)
        } else {
            if (node.left!!.start < obstacle && obstacle < node.left!!.end) {
                putObstacle(node.left!!, obstacle)
            } else if (node.right!!.start < obstacle && obstacle < node.right!!.end) {
                putObstacle(node.right!!, obstacle)
            }
        }

        node.maxFree = max(node.left?.maxFree ?: 0, node.right?.maxFree ?: 0)
    }

    private fun canPlace(node: TreeNode, before: Int, size: Int): Boolean {
        if (node.maxFree < size) {
            return false
        }

        if (node.start > before || min(before, node.end) - node.start < size) {
            return false
        }

        if (node.left == null && node.right == null) {
            return true
        }

        return canPlace(node.left!!, before, size) || canPlace(node.right!!, before, size)
    }

    fun getResults(queries: Array<IntArray>): List<Boolean> {
        val root = TreeNode(0, min(50000, 3 * queries.size))
        var results = mutableListOf<Boolean>()
        for (query in queries) {
            when (query[0]) {
                1 -> putObstacle(root, query[1])
                2 -> results.add(canPlace(root, query[1], query[2]))
            }
        }
        return results
    }
}

fun test() {
    // Test Case 1: Basic obstacle placement and block placement check
    println("Test Case 1: " + (
            Solution().getResults(
                arrayOf(
                    intArrayOf(1, 1),    // Place obstacle at 1
                    intArrayOf(1, 11),   // Place obstacle at 11
                    intArrayOf(1, 4),    // Place obstacle at 4
                    intArrayOf(1, 8),    // Place obstacle at 8
                    intArrayOf(2, 13, 7)  // Query: Can we place a block of size 7 before 13?
                )
            ) == listOf(false)
            ))

    // Test Case 2: Checking available space after obstacles
    println("Test Case 2: " + (
            Solution().getResults(
                arrayOf(
                    intArrayOf(1, 7),   // Place obstacle at 7
                    intArrayOf(2, 7, 6), // Query: Can we place a block of size 6 before 7? -> true
                    intArrayOf(1, 2),   // Place obstacle at 2
                    intArrayOf(2, 7, 5), // Query: Can we place a block of size 5 before 7? -> true
                    intArrayOf(2, 7, 6)  // Query: Can we place a block of size 6 before 7? -> false
                )
            ) == listOf(true, true, false)
            ))

    // Test Case 3: Full obstruction test
    println("Test Case 3: " + (
            Solution().getResults(
                arrayOf(
                    intArrayOf(1, 3),
                    intArrayOf(1, 6),
                    intArrayOf(1, 9),
                    intArrayOf(2, 10, 4)
                )
            ) == listOf(false)
            ))

    // Test Case 4: No obstacles, but tree range is limited
    println("Test Case 4: " + (
            Solution().getResults(
                arrayOf(
                    intArrayOf(2, 10, 5) // Query: Should return false
                )
            ) == listOf(false)
            ))

    // Test Case 5: Border case (placing obstacles at limits)
    println("Test Case 5: " + (
            Solution().getResults(
                arrayOf(
                    intArrayOf(1, 0),
                    intArrayOf(1, 50),
                    intArrayOf(2, 50, 10)
                )
            ) == listOf(false)
            ))

    // Test Case 6: Large input scenario
    val largeTest = mutableListOf<IntArray>()
    for (i in 1..10000 step 2) largeTest.add(intArrayOf(1, i)) // Place obstacles at every odd number
    largeTest.add(intArrayOf(2, 10000, 2))
    println("Test Case 6: " + (Solution().getResults(largeTest.toTypedArray()) == listOf(true)))
}
