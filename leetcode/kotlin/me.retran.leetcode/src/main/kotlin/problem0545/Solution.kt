package problem0545

class TreeNode(var `val`: Int) {
    var left: TreeNode? = null
    var right: TreeNode? = null
}

class Solution {
    fun boundaryOfBinaryTree(root: TreeNode?): List<Int> {
        if (root == null) return emptyList()

        val result = mutableListOf<Int>()

        fun isLeaf(node: TreeNode?) = node?.left == null && node?.right == null

        fun addLeftBoundary(node: TreeNode?) {
            var cur = node
            while (cur != null) {
                if (!isLeaf(cur)) {
                    result.add(cur.`val`)
                }
                cur = cur.left ?: cur.right
            }
        }

        fun addLeaves(node: TreeNode?) {
            if (node == null) {
                return
            }
            if (isLeaf(node)) {
                result.add(node.`val`)
            } else {
                addLeaves(node.left)
                addLeaves(node.right)
            }
        }

        fun addRightBoundary(node: TreeNode?) {
            var cur: TreeNode? = node
            val temp = mutableListOf<Int>()
            while (cur != null) {
                if (!isLeaf(cur)) {
                    temp.add(cur!!.`val`)
                }
                cur = cur!!.right ?: cur!!.left
            }
            for (i in temp.size - 1 downTo 0) {
                result.add(temp[i])
            }
        }

        if (!isLeaf(root)) {
            result.add(root.`val`)
        }

        addLeftBoundary(root.left)
        addLeaves(root)
        addRightBoundary(root.right)

        return result
    }
}
