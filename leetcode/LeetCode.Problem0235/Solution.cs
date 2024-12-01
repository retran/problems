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
    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q) {
        // Loop until we find the LCA
        while (root != null) {
            // If both p and q are smaller than root, LCA must be in the left subtree
            if (p.val < root.val && q.val < root.val) {
                root = root.left;
            }
            // If both p and q are greater than root, LCA must be in the right subtree
            else if (p.val > root.val && q.val > root.val) {
                root = root.right;
            }
            // We have found the split point, i.e., the LCA
            else {
                return root;
            }
        }
        
        return null; // This line will never be reached if p and q are guaranteed to be in the tree
    }
}
