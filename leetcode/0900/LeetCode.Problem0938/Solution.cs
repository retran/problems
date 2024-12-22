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
    public void TraverseAndComputeSum(TreeNode node, int low, int high, ref int sum)
    {
        if (node == null)
        {
            return;
        }

        if (node.val >= low)
        {
            TraverseAndComputeSum(node.left, low, high, ref sum);
        }

        if (node.val >= low && node.val <= high)
        {
            sum += node.val;
        }

        if (node.val <= high)
        {
            TraverseAndComputeSum(node.right, low, high, ref sum);
        }
    }

    public int RangeSumBST(TreeNode root, int low, int high)
    {
        int sum = 0;
        TraverseAndComputeSum(root, low, high, ref sum);
        return sum;
    }
}