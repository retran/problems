public class Solution
{
    public TreeNode LCA(TreeNode root, ISet<TreeNode> nodes)
    {
        if (root == null)
        {
            return null;
        }

        if (nodes.Contains(root))
        {
            return root;
        }

        var left = LCA(root.left, nodes);
        var right = LCA(root.right, nodes);

        if (left != null && right != null)
        {
            return root;
        }

        if (left != null)
        {
            return left;
        }

        if (right != null)
        {
            return right;
        }

        return null;
    }

    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode[] nodes)
    {
        var set = new HashSet<TreeNode>(nodes);
        return LCA(root, set);
    }
}