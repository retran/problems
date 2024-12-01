public class Solution
{
    public int MaxPathSum(TreeNode node, ref int max)
    {
        if (node == null)
        {
            return 0;
        }

        var left = MaxPathSum(node.left, ref max);
        var right = MaxPathSum(node.right, ref max);

        left = Math.Max(left, 0);
        right = Math.Max(right, 0);

        var current = left + right + node.val;

        max = Math.Max(max, current);

        return node.val + Math.Max(left, right);
    }

    public int MaxPathSum(TreeNode root)
    {
        int max = int.MinValue;
        MaxPathSum(root, ref max);
        return max;
    }
}
