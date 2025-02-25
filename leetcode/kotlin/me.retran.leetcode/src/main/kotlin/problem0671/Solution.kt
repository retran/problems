package problem0671

import kotlin.math.*

class Solution {
    // Definition for a binary tree node.
    class TreeNode(var `val`: Int) {
        var left: TreeNode? = null
        var right: TreeNode? = null
    }

    fun findSecondMinimumValue(root: TreeNode?): Int {
        if (root!!.left == null && root.right == null) {
            return -1
        }

        var leftValue = -1
        var rightValue = -1

        if (root.left!!.`val` != root.`val`) {
            leftValue = root.left!!.`val`
        } else {
            leftValue = findSecondMinimumValue(root.left!!)
        }

        if (root.right!!.`val` != root.`val`) {
            rightValue = root.right!!.`val`
        } else {
            rightValue = findSecondMinimumValue(root.right!!)
        }

        if (leftValue == -1) {
            return rightValue
        }

        if (rightValue == -1) {
            return leftValue
        }

        return min(leftValue, rightValue)
    }
}