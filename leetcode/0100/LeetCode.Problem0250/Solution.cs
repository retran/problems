public class Solution
{
    public (int Count, bool IsUnival) CountUnivalSubtreesImpl(TreeNode root)
    {
        if (root == null)
        {
            return (0, true);
        }

        if (root.left == null && root.right == null)
        {
            return (1, true);
        }

        var left = CountUnivalSubtreesImpl(root.left);
        var right = CountUnivalSubtreesImpl(root.right);

        int count = left.Count + right.Count;
        bool isUnival = left.IsUnival && right.IsUnival;

        if (root.left != null)
        {
            isUnival = isUnival && root.val == root.left.val;
        }

        if (root.right != null)
        {
            isUnival = isUnival && root.val == root.right.val;
        }

        if (isUnival)
        {
            count++;
        }

        return (count, isUnival);
    }

    public int CountUnivalSubtrees(TreeNode root)
    {
        return CountUnivalSubtreesImpl(root).Count;
    }
}