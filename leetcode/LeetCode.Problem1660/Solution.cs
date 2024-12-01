public class Solution
{
    public TreeNode CorrectBinaryTree(TreeNode root)
    {
        var queue = new Queue<(TreeNode node, TreeNode parent)>();

        queue.Enqueue((root, null));

        var visited = new HashSet<TreeNode>();
        visited.Add(root);

        while (queue.Count > 0)
        {
            var (current, parent) = queue.Dequeue();

            if (current.right != null && visited.Contains(current.right))
            {
                if (parent.left == current)
                {
                    parent.left = null;
                }
                else
                {
                    parent.right = null;
                }
                return root;
            }

            if (current.right != null)
            {
                visited.Add(current.right);
                queue.Enqueue((current.right, current));
            }

            if (current.left != null)
            {
                visited.Add(current.left);
                queue.Enqueue((current.left, current));
            }
        }

        return root;
    }
}