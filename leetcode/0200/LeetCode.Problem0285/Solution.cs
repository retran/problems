/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution {
    public TreeNode InorderSuccessor(TreeNode root, TreeNode p) {
        TreeNode successor = null;
        // Case 1: If p has a right child
        if (p.right != null) {
            // Move to the right child
            successor = p.right;
            // Find the leftmost node in the right subtree
            while (successor.left != null) {
                successor = successor.left;
            }
            return successor;
        }

        // Case 2: If p does not have a right child
        while (root != null) {
            if (p.val < root.val) {
                successor = root;  // Potential successor
                root = root.left;  // Go left to find a smaller value
            } else {
                root = root.right; // Go right
            }
        }
        
        return successor;
    }
}
