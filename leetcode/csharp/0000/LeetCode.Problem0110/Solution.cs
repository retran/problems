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
    public bool IsBalanced(TreeNode root) {
        // Helper function to check height and balance
        return CheckBalance(root) != -1;
    }
    
    private int CheckBalance(TreeNode node) {
        // Base case: an empty tree is balanced and has height -1
        if (node == null) {
            return 0; // Height of an empty tree is considered 0
        }
        
        // Recursively check the left subtree
        int leftHeight = CheckBalance(node.left);
        if (leftHeight == -1) return -1; // Not balanced
        
        // Recursively check the right subtree
        int rightHeight = CheckBalance(node.right);
        if (rightHeight == -1) return -1; // Not balanced
        
        // Check the balance condition for the current node
        if (Math.Abs(leftHeight - rightHeight) > 1) {
            return -1; // Current subtree is not balanced
        }
        
        // Return the height of the current node
        return Math.Max(leftHeight, rightHeight) + 1;
    }
}
