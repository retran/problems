public class Solution
{
    public TreeNode DeleteNode(TreeNode root, int key)
    {
        if (root == null)
        {
            return null;
        }

        if (key < root.val)
        {
            root.left = DeleteNode(root.left, key);
        }
        else if (key > root.val)
        {
            root.right = DeleteNode(root.right, key);
        }
        else
        {
            if (root.left == null)
            {
                return root.right;
            }
            else if (root.right == null)
            {
                return root.left;
            }
            else
            {
                root.val = FindMinKey(root.right);
                root.right = DeleteNode(root.right, root.val);
            }
        }
        return root;
    }

    private int FindMinKey(TreeNode node)
    {
        while (node.left != null)
        {
            node = node.left;
        }
        return node.val;
    }
}