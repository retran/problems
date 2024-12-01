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
    public bool IsSameTree(TreeNode p, TreeNode q)
    {
        int max = 0;
        var stack = new Stack<TreeNode>();

        stack.Push((p));
        stack.Push((q));

        while (stack.Count > 0)
        {
            var pnode = stack.Pop();
            var qnode = stack.Pop();
            if (qnode == null || pnode == null)
            {
                if (qnode != pnode)
                {
                    return false;
                }

                continue;
            }

            if (qnode.val != pnode.val)
            {
                return false;
            }

            stack.Push(pnode.left);
            stack.Push(qnode.left);
            stack.Push(pnode.right);
            stack.Push(qnode.right);
        }
        return true;
    }
}