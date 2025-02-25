package problem0124

// Definition for a binary tree node.
class TreeNode(var `val`: Int) {
    var left: TreeNode? = null
    var right: TreeNode? = null
}

class Solution {
    fun maxPathSum(root: TreeNode?): Int {
        var maxSum = Int.MIN_VALUE

        fun maxPathSumImpl(node: TreeNode?): Int {
            if (node == null) return 0

            val left = maxOf(maxPathSumImpl(node.left), 0)
            val right = maxOf(maxPathSumImpl(node.right), 0)

            maxSum = maxOf(maxSum, left + right + node.`val`)

            return node.`val` + maxOf(left, right)
        }

        maxPathSumImpl(root)
        return maxSum
    }
}

// Test cases
fun test() {
    val sol = Solution()

    // Test 1: Single node tree.
    val tree1 = TreeNode(1)
    println("Test 1: Expected 1, got ${sol.maxPathSum(tree1)}")

    // Test 2: Tree: [-10,9,20,null,null,15,7]
    //         -10
    //         /  \
    //        9    20
    //            /  \
    //           15   7
    val tree2 = TreeNode(-10).apply {
        left = TreeNode(9)
        right = TreeNode(20).apply {
            left = TreeNode(15)
            right = TreeNode(7)
        }
    }
    println("Test 2: Expected 42, got ${sol.maxPathSum(tree2)}")

    // Test 3: Tree: [2,-1]
    //         2
    //        /
    //      -1
    val tree3 = TreeNode(2).apply {
        left = TreeNode(-1)
    }
    println("Test 3: Expected 2, got ${sol.maxPathSum(tree3)}")

    // Test 4: Single negative node.
    val tree4 = TreeNode(-3)
    println("Test 4: Expected -3, got ${sol.maxPathSum(tree4)}")

    // Test 5: Tree: [1,2,3]
    //         1
    //        / \
    //       2   3
    // Expected max path sum = 2 + 1 + 3 = 6
    val tree5 = TreeNode(1).apply {
        left = TreeNode(2)
        right = TreeNode(3)
    }
    println("Test 5: Expected 6, got ${sol.maxPathSum(tree5)}")
}
