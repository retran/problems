public class Solution
{
    private int MaxZigZag(TreeNode root, bool left, int current)
    {
        return root == null
            ? current
            : left
            ? Math.Max(
                MaxZigZag(root.left, true, 0),
                MaxZigZag(root.right, false, current + 1))
            : Math.Max(
                MaxZigZag(root.left, true, current + 1),
                MaxZigZag(root.right, false, 0));
    }

    public int LongestZigZag(TreeNode root)
    {
        return root == null
            ? 0
            : Math.Max(
            MaxZigZag(root.left, true, 0),
            MaxZigZag(root.right, false, 0));
    }
}