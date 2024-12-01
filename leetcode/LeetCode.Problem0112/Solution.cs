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
    public bool HasPathSum(TreeNode root, int targetSum)
    {
        if (root == null)
        {
            return false;
        }

        var stack = new Stack<(TreeNode, int)>();
        stack.Push((root, 0));
        while (stack.Count > 0)
        {
            var (node, sum) = stack.Pop();
            if (node == null)
            {
                continue;
            }

            if (sum + node.val == targetSum && node.left == null && node.right == null)
            {
                return true;
            }

            stack.Push((node.left, node.val + sum));
            stack.Push((node.right, node.val + sum));
        }
        return false;
    }
}