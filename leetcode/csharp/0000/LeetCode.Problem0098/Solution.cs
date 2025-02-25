public class Solution
{
    private bool IsValidBST(TreeNode root, long min, long max)
    {
        if (root == null)
        {
            return true;
        }

        if (root.val <= min || root.val >= max)
        {
            return false;
        }

        return IsValidBST(root.left, min, root.val) && IsValidBST(root.right, root.val, max);
    }

    public bool IsValidBST(TreeNode root)
    {
        return IsValidBST(root, long.MinValue, long.MaxValue);
    }
}