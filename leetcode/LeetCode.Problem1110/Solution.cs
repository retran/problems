public class Solution
{
    public TreeNode DeleteNodes(TreeNode root, HashSet<int> toDelete, IList<TreeNode> roots)
    {
        if (root == null)
        {
            return null;
        }

        root.left = DeleteNodes(root.left, toDelete, roots);
        root.right = DeleteNodes(root.right, toDelete, roots);

        if (toDelete.Contains(root.val))
        {
            if (root.left != null)
            {
                roots.Add(root.left);
            }

            if (root.right != null)
            {
                roots.Add(root.right);
            }

            root = null;
        }

        return root;
    }

    public IList<TreeNode> DelNodes(TreeNode root, int[] to_delete)
    {
        var roots = new List<TreeNode>();
        var toDelete = new HashSet<int>(to_delete);

        if (!toDelete.Contains(root.val))
        {
            roots.Add(root);
        }

        DeleteNodes(root, toDelete, roots);

        return roots;
    }
}