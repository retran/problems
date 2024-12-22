/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
public class Solution {
    public TreeNode InsertIntoBST(TreeNode root, int val) {
        // Case 1: If the tree is empty, return a new node
        if (root == null) {
            return new TreeNode(val);
        }

        // Case 2: Otherwise, traverse the tree
        TreeNode current = root;
        while (true) {
            if (val < current.val) {
                // Move to the left
                if (current.left == null) {
                    // If left child is null, insert the new node here
                    current.left = new TreeNode(val);
                    break;
                } else {
                    current = current.left; // Move to left child
                }
            } else {
                // Move to the right
                if (current.right == null) {
                    // If right child is null, insert the new node here
                    current.right = new TreeNode(val);
                    break;
                } else {
                    current = current.right; // Move to right child
                }
            }
        }

        return root; // Return the unchanged root pointer
    }
}
