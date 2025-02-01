// Definition for a binary tree node.
public class TreeNode
{
    public int val;
    public TreeNode? left;
    public TreeNode? right;
    public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }
}

public class Solution
{
    private int TraverseAndComputeSum(TreeNode? node, int low, int high)
    {
        int sum = 0;

        if (node == null)
        {
            return sum;
        }

        if (node.val >= low)
        {
            sum += TraverseAndComputeSum(node.left, low, high);
        }

        if (node.val >= low && node.val <= high)
        {
            sum += node.val;
        }

        if (node.val <= high)
        {
            sum += TraverseAndComputeSum(node.right, low, high);
        }

        return sum;
    }

    public int RangeSumBST(TreeNode root, int low, int high)
    {
        return TraverseAndComputeSum(root, low, high);
    }
}
