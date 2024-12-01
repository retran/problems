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
public class Solution
{
    public void Flatten(TreeNode root)
    {

        if (root == null)
        {
            return;
        }

        var stack = new Stack<TreeNode>();
        stack.Push(root);
        TreeNode prev = null;
        while (stack.Count > 0)
        {
            var current = stack.Pop();
            if (prev != null)
            {
                prev.right = current;
                prev.left = null;
            }

            prev = current;
            if (current.right != null)
            {
                stack.Push(current.right);
            }

            if (current.left != null)
            {
                stack.Push(current.left);
            }
        }
    }
}