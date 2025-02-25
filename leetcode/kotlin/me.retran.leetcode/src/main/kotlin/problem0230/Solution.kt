package problem0230

class TreeNode(var `val`: Int) {
    var left: TreeNode? = null
    var right: TreeNode? = null
}

class Solution {
    fun kthSmallest(root: TreeNode?, k: Int): Int {
        val stack = ArrayDeque<TreeNode>()
        var currentNode = root
        var count = k

        while (currentNode != null || stack.isNotEmpty()) {
            while (currentNode != null) {
                stack.addFirst(currentNode)
                currentNode = currentNode.left
            }

            val node = stack.removeFirst()
            count--
            if (count == 0) return node.`val`

            currentNode = node.right
        }
        return -1
    }
}
