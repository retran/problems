public class Solution
{
    public int GoodNodesImpl(TreeNode root, int max)
    {
        if (root == null)
        {
            return 0;
        }

        int count = root.val >= max ? 1 : 0;

        max = Math.Max(root.val, max);

        count += GoodNodesImpl(root.left, max);
        count += GoodNodesImpl(root.right, max);

        return count;
    }

    public int GoodNodes(TreeNode root)
    {
        return GoodNodesImpl(root, int.MinValue);
    }
}