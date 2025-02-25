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
public bool IsSymmetric(TreeNode root)
    {
        int max = 0;
        var stack = new Stack<TreeNode>();

        stack.Push((root.left));
        stack.Push((root.right));

        while (stack.Count > 0)
        {
            var pnode = stack.Pop();
            var qnode = stack.Pop();
            if (qnode == null || pnode == null)
            {
                if (qnode != pnode)
                    return false;
                else
                    continue;
            }

            if (qnode.val != pnode.val)
            {
                return false;
            }

            stack.Push(pnode.right);
            stack.Push(qnode.left);
            stack.Push(pnode.left);
            stack.Push(qnode.right);
        }
        return true;
    }
}